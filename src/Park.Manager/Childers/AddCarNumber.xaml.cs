using Abp.AutoMapper;
using Park.CarNumberses;
using Park.CarNumberses.Dtos;
using Park.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Park.Childers
{
    /// <summary>
    /// AddCarNumber.xaml 的交互逻辑
    /// </summary>
    public partial class AddCarNumber 
    {
        private readonly UserManagerViewModel _userManagerViewModel;
        private readonly CarNumbersListDto carNumbersListDto;
        private readonly ICarNumbersAppService _carNumbersAppService;
        public AddCarNumber(UserManagerViewModel userManagerViewModel, ICarNumbersAppService carNumbersAppService)
        {
            InitializeComponent();
            _userManagerViewModel = userManagerViewModel;
            carNumbersListDto = new CarNumbersListDto();
            _carNumbersAppService = carNumbersAppService;
            if (_userManagerViewModel.SelectDto != null)
            {
                carNumbersListDto.Phone = _userManagerViewModel.SelectDto.Phone;
                carNumbersListDto.Contact = _userManagerViewModel.SelectDto.Contact;
                carNumbersListDto.Remark = _userManagerViewModel.SelectDto.Remark;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _carNumbersAppService.CreateOrUpdateCarNumbers(new CreateOrUpdateCarNumbersInput() { CarNumbers = carNumbersListDto.MapTo<CarNumbersEditDto>() });

            DialogResult = true;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
