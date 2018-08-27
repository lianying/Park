using Park.ParkAreases.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class ParkAreaViewModel:NotifyPropertyChangeBase
    {
        private ParkAreaDto selectedParkArea;
        public ParkAreaDto SelectedParkArea
        {
            get { return selectedParkArea; }
            set
            {
                selectedParkArea = value;
                NotifyPropertyChange(() => SelectedParkArea);
            }
        }


        public List<ParkAreaDto> TreeViewMenu { get; set; }


        public List<ParkAreaDto> ComboxData { get; set; }

        //private bool isSelected;
        //public bool IsSelected
        //{
        //    get { return isSelected; }
        //    set
        //    {
        //        isSelected = value;
        //        NotifyPropertyChange(() => IsSelected);
        //    }
        //}


    }
}
