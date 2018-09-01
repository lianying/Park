using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Park.Entitys.ParkAreas;
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
    [AutoMap(typeof(ParkAreas), typeof(ParkAreasEditDto))]
    public class ParkAreaDto : NotifyPropertyChangeBase<long>
    {
        public ParkAreaDto ParentArea { get; set; }

        public long? ParentAreaId { get; set; }


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

        private int parkAreaTempCarports;
        /// <summary>
        /// ParkAreaRentableCarports
        /// </summary>
        public int ParkAreaTempCarports
        {
            get { return parkAreaTempCarports; }
            set
            {
                parkAreaTempCarports = value;
                NotifyPropertyChange(() => ParkAreaTempCarports);
            }
        }


        private int parkAreaFixedCarports;
        /// <summary>
        /// 固定车位
        /// </summary>
        public int ParkAreaFixedCarports
        {
            get { return parkAreaFixedCarports; }
            set
            {
                parkAreaFixedCarports = value;
                NotifyPropertyChange(() => ParkAreaFixedCarports);
            }
        }

        private bool isSelected;
        private ObservableCollection<ParkEntranceses.Dtos.ParkEntrancesListDto> _entrancesListDtos;

        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChange(() => IsSelected); } }

        public ObservableCollection<ParkAreaDto> ParkAreas { get; set; }


        public ObservableCollection<ParkEntranceses.Dtos.ParkEntrancesListDto> EntrancesListDtos
        {
            get => _entrancesListDtos; set
            {
                _entrancesListDtos = value;
                NotifyPropertyChange(() => EntrancesListDtos);
            }
        }
    }
}
