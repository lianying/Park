using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Entitys.Parks;
using Abp.Application.Services.Dto;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Park.ParkBox.Dto
{
    [AutoMap(typeof(ParkSet))]
    public  class ParkDto:EntityDto,INotifyPropertyChanged
    {
        //public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        private int carportCount;
        public int CarportCount
        {
            get { return carportCount; }
            set
            {
                carportCount = value;
                NotifyPropertyChanged("CarportCount");
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                NotifyPropertyChanged("Address");
            }
        }



        private string propertyParty;
        /// <summary>
        /// 产权方
        /// </summary>
        public string PropertyParty
        {
            get { return propertyParty; }
            set
            {
                this.propertyParty = value;
                NotifyPropertyChanged("PropertyParty");
            }
        }

        private string @operator;
        /// <summary>
        /// 运营方
        /// </summary>
        public string Operator
        {
            get { return @operator; }
            set
            {
                @operator = value;
                NotifyPropertyChanged("Operator");
            }
        }



        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }

            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        private string headColor;
        public string HeadColor
        {
            get { return headColor; }
            set { headColor = value;
                NotifyPropertyChanged("HeadColor");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
