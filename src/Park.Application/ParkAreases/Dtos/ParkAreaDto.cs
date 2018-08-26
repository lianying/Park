using Park.ParkBox.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ParkAreases.Dtos
{
    public class ParkAreaDto : NotifyPropertyChangeBase
    {
        public ParkDto Park { get; set; }


        private string areaName;
        /// <summary>
        /// AreaName
        /// </summary>
        [MaxLength(1000, ErrorMessage = "AreaName超出最大长度")]
        public string AreaName
        {
            get { return areaName; }
            set
            {
                areaName = value;
                NotifyPropertyChange(() => AreaName);
            }
        }

        private int parkAreaCarports;
        /// <summary>
        /// ParkAreaCarports
        /// </summary>
        public int ParkAreaCarports
        {
            get { return parkAreaCarports; }
            set
            {
                parkAreaCarports = value;
                NotifyPropertyChange(() => ParkAreaCarports);
            }
        }

        private int parkAreaRentableCarports;
        /// <summary>
        /// ParkAreaRentableCarports
        /// </summary>
        public int ParkAreaRentableCarports
        {
            get { return parkAreaRentableCarports; }
            set
            {
                parkAreaRentableCarports = value;
                NotifyPropertyChange(() => ParkAreaRentableCarports);
            }
        }

        public ObservableCollection<ParkAreaDto> ParkAreas { get; set; }
    }
}
