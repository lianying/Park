using Park.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Park.Behaviors
{
    public class MoveToViewboxBehavior:Behavior<Grid>
    {
        public bool IsClipped { get { return (bool)GetValue(IsClippedProperty); } set { SetValue(IsClippedProperty, value); } }
        public static readonly DependencyProperty IsClippedProperty = DependencyProperty.Register("IsClipped", typeof(bool), typeof(MoveToViewboxBehavior), new PropertyMetadata(false, OnIsClippedChanged));

        private static void OnIsClippedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var beh = (MoveToViewboxBehavior)sender;
            Grid grid = beh.AssociatedObject;

            Viewbox vb = VisualHelper.FindVisualChild<Viewbox>(grid);
            ContentPresenter cp = VisualHelper.FindVisualChild<ContentPresenter>(grid);

            if ((bool)e.NewValue)
            {
                // is clipped, so move content to Viewbox
                UIElement element = cp.Content as UIElement;
                cp.Content = null;
                vb.Child = element;
            }
            else
            {
                // can be shown without clipping, so move content to ContentPresenter
                cp.Content = vb.Child;
                vb.Child = null;
            }
        }

        protected override void OnAttached()
        {
            this.AssociatedObject.SizeChanged += (s, e) => { IsClipped = CalculateIsClipped(); };
        }

        // Determines if the width of all textblocks within TextBlockContainer (using MaxFontSize) are wider than the AssociatedObject grid
        private bool CalculateIsClipped()
        {
            double totalDesiredWidth = 0d;
            //Grid grid = VisualHelper.FindVisualChildByName<Grid>(this.AssociatedObject, "TextBlockContainer");
            List<TextBlock> tbs = VisualHelper.FindVisualChildren<TextBlock>(this.AssociatedObject);

            foreach (var tb in tbs)
            {
                if (tb.TextWrapping != TextWrapping.NoWrap)
                    return false;

                totalDesiredWidth += MeasureText(tb).Width + tb.Margin.Left + tb.Margin.Right + tb.Padding.Left + tb.Padding.Right;
            }

            return Math.Round(this.AssociatedObject.ActualWidth, 5) < Math.Round(totalDesiredWidth, 5);
        }

        // Measures text size of textblock
        private Size MeasureText(TextBlock tb)
        {
            var formattedText = new FormattedText(tb.Text, CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(tb.FontFamily, tb.FontStyle, tb.FontWeight, tb.FontStretch),
                tb.FontSize, Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
}
