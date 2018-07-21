using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Park.Helpers
{
    public class VisualHelper
    {
        public static List<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            List<T> children = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var o = VisualTreeHelper.GetChild(obj, i);
                if (o != null)
                {
                    if (o is T)
                        children.Add((T)o);

                    children.AddRange(FindVisualChildren<T>(o)); // recursive
                }
            }
            return children;
        }

        public static T FindUpVisualTree<T>(DependencyObject initial) where T : DependencyObject
        {
            DependencyObject current = initial;

            while (current != null && current.GetType() != typeof(T))
            {
                current = VisualTreeHelper.GetParent(current);
            }
            return current as T;
        }

        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            T child = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var o = VisualTreeHelper.GetChild(obj, i);
                if (o != null)
                {
                    child = o as T;
                    if (child != null) break;
                    else
                    {
                        child = FindVisualChild<T>(o); // recursive
                        if (child != null) break;
                    }
                }
            }
            return child;
        }

       

        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            T child = default(T);
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var o = VisualTreeHelper.GetChild(parent, i);
                if (o != null)
                {
                    child = o as T;
                    if (child != null && child.Name == name)
                        break;
                    else
                        child = FindVisualChildByName<T>(o, name);

                    if (child != null) break;
                }
            }
            return child;
        }
    }
}
