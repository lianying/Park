using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Extensions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Park.CarTypeses;
using Park.EnumHelper;
using Park.ParkAreases;
using Park.ParkEntranceses;
using Park.ParkEntranceses.Dtos;
using Park.ViewModel;

namespace Park.Pages
{
    /// <summary>
    /// Entrances.xaml 的交互逻辑
    /// </summary>
    public partial class Entrances : Page,ISingletonDependency
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IParkEntrancesAppService _parkEntrancesAppService;
        private readonly ICarTypesAppService _carTypesAppService;
        private readonly IParkAreasAppService _parkAreasAppService;
        private ParkEntranceViewModel _entranceViewModel;
        public Entrances(MainWindowViewModel mainWindowViewModel, IParkEntrancesAppService parkEntrancesAppService,
            ICarTypesAppService carTypesAppService,
            IParkAreasAppService parkAreasAppService)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _parkEntrancesAppService = parkEntrancesAppService;
            _carTypesAppService = carTypesAppService;
            _entranceViewModel = new ParkEntranceViewModel();
            _parkAreasAppService = parkAreasAppService;
            this.DataContext = _entranceViewModel;
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel_MouseLeftButtonDown(sender, null);
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (_entranceViewModel.SelectDto == null)
            {
                window.ShowMessageAsync("提示", "请选择要删除的出入口");
                return;
            }
            _parkEntrancesAppService.DeleteParkEntrances(_entranceViewModel.SelectDto);
            LoadParkArea();
            _entranceViewModel.SelectDto = new ParkEntrancesListDto();
            _entranceViewModel.SelectDto.ParkEntrancePermission = new ParkEntrancePermissions.Dtos.ParkEntrancePermissionListDto();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _entranceViewModel.SelectDto.EntranceType = _entranceViewModel.SelectedMyEnumType.Key;
            var entranceDto = _entranceViewModel.SelectDto.MapTo<ParkEntrancesEditDto>();
            entranceDto.ParkEntrancePermission.CarTypes = string.Join(",", Array.ConvertAll<CarTypeses.Dtos.CarTypesListDto, long>(_entranceViewModel.CarTypesLists.Where(x => x.IsSelected).ToArray(), x => x.Id));
            await _parkEntrancesAppService.CreateOrUpdateParkEntrances(new CreateOrUpdateParkEntrancesInput() { ParkEntrances = entranceDto });

            var window = Application.Current.MainWindow as MetroWindow;

            await window.ShowMessageAsync("提示", "操作成功");
            LoadParkArea();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _entranceViewModel.SelectDto = new ParkEntrancesListDto();

            _entranceViewModel.SelectDto.ParkEntrancePermission = new ParkEntrancePermissions.Dtos.ParkEntrancePermissionListDto();

            foreach (var item in _entranceViewModel.CarTypesLists)
            {

                item.IsSelected = false;
            }
        }

        private void trvFamilies_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;
            if (treeView.SelectedValue is ParkEntranceses.Dtos.ParkEntrancesListDto) //选中的为出入口
            {
                _entranceViewModel.SelectDto = treeView.SelectedValue as ParkEntrancesListDto;

                _entranceViewModel.SelectedMyEnumType = new KeyValuePair<Enum.EntranceType, string>(_entranceViewModel.SelectDto.EntranceType, _entranceViewModel.SelectDto.EntranceType.GetAttributeOfType<DescriptionAttribute>().Description);
                if (!_entranceViewModel.SelectDto.ParkEntrancePermission.CarTypes.IsNullOrEmpty())
                {
                    var carTypeArray = Array.ConvertAll<string, long>(_entranceViewModel.SelectDto.ParkEntrancePermission.CarTypes.TrimEnd(',').Split(','), x => Convert.ToInt64(x));
                    foreach (var item in _entranceViewModel.CarTypesLists)
                    {
                        if (carTypeArray.Contains(item.Id))
                        {
                            item.IsSelected = true;
                        }
                        else
                        {
                            item.IsSelected = false;
                        }
                    }
                }
            }
            else
            {
                
            }
        }

        private async  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var carTypes = await _carTypesAppService.GetTypesListDtos();
            LoadParkArea();
            _entranceViewModel.CarTypesLists = new System.Collections.ObjectModel.ObservableCollection<CarTypeses.Dtos.CarTypesListDto>(carTypes);
            //_entranceViewModel.EntrancesListDtos = new System.Collections.ObjectModel.ObservableCollection<ParkEntranceses.Dtos.ParkEntrancesListDto>(parkEntrance);


            var parkAea = await _parkAreasAppService.GetParkAreaDtos(_mainWindowViewModel.SelectParkDto.Id);

            _entranceViewModel.ParkAreaDtos = new System.Collections.ObjectModel.ObservableCollection<ParkAreases.Dtos.ParkAreaDto>(parkAea);

            _entranceViewModel.SelectDto = new ParkEntrancesListDto();
            _entranceViewModel.SelectDto.ParkEntrancePermission = new ParkEntrancePermissions.Dtos.ParkEntrancePermissionListDto();

        }

        private async void LoadParkArea()
        {

            var parkAea = await _parkAreasAppService.GetParkAreaDtos(_mainWindowViewModel.SelectParkDto.Id);
            //trvFamilies.ItemsSource = parkAea;
            List<ParkEntrancesListDto> list;
            foreach (var item in parkAea)
            {
                list = await _parkEntrancesAppService.GetParkEntrancesListDtosByAreaId(item.Id);
                item.EntrancesListDtos = new System.Collections.ObjectModel.ObservableCollection<ParkEntrancesListDto>(list);
            }

            _entranceViewModel.ParkAreaDtos = new System.Collections.ObjectModel.ObservableCollection<ParkAreases.Dtos.ParkAreaDto>(parkAea);

        }

      

        private void Cmb_Types_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Cmb_Types.SelectedItem = null;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_entranceViewModel.Filter.IsNullOrEmpty())
            {
                LoadParkArea();
                return;
            }

            //foreach (var item in _entranceViewModel.ParkAreaDtos)
            //{
            //    foreach (var entrice in item.EntrancesListDtos)
            //    {
            //        if (!entrice.EntranceName.Contains(_entranceViewModel.Filter))
            //        {
            //            item.EntrancesListDtos.Remove(entrice);
            //        }
            //    } 
            //}
            for (int i = 0; i < _entranceViewModel.ParkAreaDtos.Count; i++)
            {
                for (int j = 0; j < _entranceViewModel.ParkAreaDtos[i].EntrancesListDtos.Count; )
                {
                    if (!_entranceViewModel.ParkAreaDtos[i].EntrancesListDtos[j].EntranceName.Contains(_entranceViewModel.Filter))
                    {
                        _entranceViewModel.ParkAreaDtos[i].EntrancesListDtos.Remove(_entranceViewModel.ParkAreaDtos[i].EntrancesListDtos[j]);
                        j = 0;
                        continue;
                    }
                    j++;
                }
            }
        }
    }
}
