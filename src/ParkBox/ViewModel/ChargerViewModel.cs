using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class ChargerViewModel : ViewModelBase
    {
        private decimal pay;
        public decimal Pay
        {
            get { return pay; }
            set {
                if (pay != value)
                {
                    pay = value <= 0 ? 0 : value;
                    RaisePropertyChanged("Pay");
                }
            }
        }
        private decimal receivable;
        public decimal Receivable
        {
            get { return receivable; }
            set
            {
                if (receivable != value)
                {
                    receivable = value <= 0 ? 0 : value;
                    RaisePropertyChanged("Receivable");
                }
            }
        }
        private decimal? localMoneyOrTime;
        public decimal? LocalMoneyOrTime
        {
            get { return localMoneyOrTime; }
            set
            {
                if (localMoneyOrTime != value&&value.HasValue)
                {
                    localMoneyOrTime = value.Value < 0 ? 0 : value.Value;
                    RaisePropertyChanged("LocalMoneyOrTime");
                }
            }
        }

        private string carNumber;
        public string CarNumber
        {
            get { return carNumber; }
            set
            {
                if (carNumber==null||carNumber.Equals(value) == false)
                {
                    carNumber = value;
                    RaisePropertyChanged("CarNumber");
                }
            }
        }

        private DateTime inTime;
        public DateTime InTime
        {
            get { return inTime; }
            set
            {
                if (inTime != value)
                {
                    inTime = value;
                    RaisePropertyChanged("InTime");

                }
            }
        }


        private DateTime outTime;
        public DateTime OutTime
        {
            get { return outTime; }
            set
            {
                if (outTime != value)
                {
                    outTime = value;
                    RaisePropertyChanged("OutTime");

                }
            }
        }


        private string remark;
        public string Remark
        {
            get { return remark; }
            set
            {
                if (remark==null||remark.Equals(value) == false)
                {
                    remark = value;
                    RaisePropertyChanged("Remark");
                }
            }
        }

        private bool isTimeChecked;

        public bool IsTimeChecked
        {
            get { return isTimeChecked; }
            set
            {
                if (isTimeChecked != value)
                {
                    isTimeChecked = value;
                    RaisePropertyChanged("IsTimeChecked");
                }
            }
        }

        private bool isMoneyChecked;

        public bool IsMoneyChecked
        {
            get { return isMoneyChecked; }
            set
            {
                if (isMoneyChecked != value)
                {
                    isMoneyChecked = value;
                    RaisePropertyChanged("IsMoneyChecked");
                }
            }
        }


        private decimal? advancePayment;
        public decimal? AdvancePayment
        {
            get { return advancePayment ?? 0; }
            set
            {
                if (advancePayment != value && value.HasValue)
                {
                    advancePayment = value.Value < 0 ? 0 : value.Value;
                    RaisePropertyChanged("AdvancePayment");
                }
            }
        }


        private decimal? discountedPrice;
        public decimal? DiscountedPrice
        {
            get { return discountedPrice ?? 0; }
            set
            {
                if (discountedPrice != value && value.HasValue)
                {
                    discountedPrice = value.Value < 0 ? 0 : value.Value;
                    RaisePropertyChanged("DiscountedPrice");
                }
            }
        }

    }
}
