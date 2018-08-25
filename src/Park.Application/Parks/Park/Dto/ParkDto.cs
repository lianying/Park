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
using Park.Parks.Park.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park.ParkBox.Dto
{
    [AutoMap(typeof(ParkSet),typeof(CreateParkDto))]
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


        [Required]
        public virtual decimal Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        /// 
        [Required]
        public virtual decimal Latitude { get; set; }

        [Required]
        [MaxLength(ParkBase.MaxAreaCodeLength)]
        /// <summary>
        /// 区域编码
        /// </summary>
        public virtual string AreaCode { get; set; }
        public virtual bool IsSync { get; set; }
        [MaxLength(ParkSet.MaxParkSoureLength)]
        /// <summary>
        /// 车场来源
        /// </summary>
        public virtual string ParkSoure { get; set; }

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
        [NotMapped]
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
        [NotMapped]
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
