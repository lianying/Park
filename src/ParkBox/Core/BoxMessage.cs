using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Castle.Core.Logging;
using Park.Devices.Models;
using Park.Entitys.Box;
using Park.Entitys.CarTypes;
using Park.Entitys.CarUsers;
using Park.Entitys.FareRules;
using Park.Enum;
using Park.Froms;
using Park.Models;
using Park.ParkBox;
using Park.Parks.Devices.Interfaces;
using Park.Parks.ParkBox;
using Park.Parks.ParkBox.Core;
using Park.Parks.ParkBox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Park.Core
{
    public class BoxMessage : IBoxMessage
    {
        private readonly IParkBoxOptions parkBoxOptions;
        private readonly IVehicleFlow _vehicleFlow;
        private readonly ICarNumberPermission _carNumberPermission;
        private readonly LedManager _ledManager;

        private readonly IRepository<CarTypes, long> _repositoryCarType;
        private readonly IRepository<RangeTime> _repositoryRangeTime;
        private readonly IRepository<BlackList, int> _repositoryBlackList;
        private readonly IRepository<CarPort, long> _repositoryCarPort;
        private readonly IUnitOfWorkManager _unitOfWork;


        private readonly IRepository<FareRule> _repositoryFareRule;

        public ILogger Logger { get; set; }


        public BoxMessage(IParkBoxOptions parkBoxOptions, 
            IVehicleFlow vehicleFlow,
            ICarNumberPermission carNumberPermission,
            LedManager ledManager,
            IRepository<CarTypes, long> repositoryCarType,
           IRepository<FareRule> repositoryFareRule,
           IRepository<RangeTime> repositoryRangeTime,
           IRepository<BlackList, int> repositoryBlackList,
           IRepository<CarPort, long> repositoryCarPort,
           IUnitOfWorkManager unitOfWork
           )
        {

            this.parkBoxOptions = parkBoxOptions;
            _vehicleFlow = vehicleFlow;
            _carNumberPermission = carNumberPermission;
            _ledManager = ledManager;

            _repositoryCarType = repositoryCarType;
            _repositoryFareRule = repositoryFareRule;
            _repositoryRangeTime = repositoryRangeTime;
            _repositoryBlackList = repositoryBlackList;
            _repositoryCarPort = repositoryCarPort;
            Logger = NullLogger.Instance;
            _unitOfWork = unitOfWork;

        }

        private async Task< ChargerWindow> GetChargerWindow(LedManager ledManager, string carNumber, FareRule fareRule, InOutTypeEnum inOutType,
            IRepository<CarTypes, long> repositoryCarTypes,
             IParkBoxOptions parkBoxOptions,
             IRepository<CarPort, long> repositoryCarPort,
             IVehicleFlow vehicleFlow,
             IDeviceable deviceable,
             IRepository<FareRule> repositoryFareRule,
            IRepository<RangeTime> repositoryRangeTime,
             ManualResetEvent manualResetEvent)
        {
            ChargerWindow chargerWindow = null;
            await Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                chargerWindow = new ChargerWindow(ledManager, carNumber, fareRule, inOutType, repositoryCarTypes, parkBoxOptions, repositoryCarPort, vehicleFlow, deviceable, repositoryFareRule, repositoryRangeTime, manualResetEvent);
            }));
            return chargerWindow;
        }

        private async Task<ChargerWindow> GetChargerWindow(LedManager ledManager, CarOutModel carOutModel, FareRule fareRule, decimal receivable,
            IRepository<CarTypes, long> repositoryCarTypes,
             IParkBoxOptions parkBoxOptions,
            IRepository<CarPort, long> repositoryCarPort,
            IVehicleFlow vehicleFlow,
            IDeviceable deviceable,
            IRepository<FareRule> repositoryFareRule,
            IRepository<RangeTime> repositoryRangeTime,
             ManualResetEvent manualResetEvent)
        {
            ChargerWindow chargerWindow = null;
            await Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                chargerWindow = new ChargerWindow(ledManager, carOutModel, fareRule ,receivable, repositoryCarTypes, parkBoxOptions, repositoryCarPort, vehicleFlow, deviceable, repositoryFareRule, repositoryRangeTime, manualResetEvent);
            }));
            return chargerWindow;
        }
        public async void DoMessage(DeviceInfoDto deviceInfoDto)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            using (var unitOfWork = _unitOfWork.Begin())
            {
                DateTime photoTime = DateTime.Now;
                var result = deviceInfoDto.Controlable.Camerable.GetPlateNumber();
                string carNumber = result.Item1;
                //Task.Run(async () =>
                // {
                await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetImage(await result.Item2);
                //});
                if (deviceInfoDto.EntranceDto.EntranceType == Enum.EntranceType.In)
                {
                    //禁止非机动车入场
                    if (!parkBoxOptions.NonmotorVehicleIn && result.Item4 == CarTypeEnum.NonMotorVehicle)
                    {
                        Logger.Info("非机动车辆禁止进入" + result.Item1);
                        return;
                    }
                    var backList = await _repositoryBlackList.FirstOrDefaultAsync(x => x.CarNumber == carNumber);
                    if (backList != null)
                    {
                        Logger.Info(result.Item1 + " 存在于黑名单 禁止进入");
                    }

                    var permission = _carNumberPermission.CheckCarNumberPermission(carNumber, deviceInfoDto.EntranceDto.Id);
                    var carInModel = new Parks.ParkBox.CarInModel()
                    {
                        CarNumber = carNumber,
                        Img = null,
                        InOutType = Enum.InOutTypeEnum.Photo,
                        InTime = DateTime.Now,
                        Entrance = deviceInfoDto.EntranceDto
                    };
                    if (permission.IsCarIn.HasValue && !permission.IsCarIn.Value)
                    {
                        Logger.Debug(carNumber + "no permission" + permission.ToString());

                        return;



                    }
                    else if (permission.IsCarIn == null)
                    {
                        var cancle = MessageBox.Show( "当前车辆无权进入是否放行？", "提示", MessageBoxButton.OKCancel);
                        if (cancle == MessageBoxResult.Cancel)
                            return;
                    }
                    //入场时检查是否有场内记录 
                    var isCarIn = _vehicleFlow.IsCarIn(deviceInfoDto.EntranceDto.ParkLevel.Park.Id, carNumber);
                    if (isCarIn.IsCarIn)
                    {
                        var carOutModel = new CarOutModel()
                        {
                            CarInRecord = isCarIn.CarInRecord,
                            InOutType = Enum.InOutTypeEnum.Photo,
                            OutTime = DateTime.Now,
                            Receivable = 0,
                            ParkId = deviceInfoDto.EntranceDto.ParkLevel.Park.Id,
                            Remark = "有场内纪录，再次入场"

                        };
                        _vehicleFlow.CarErrorOut(isCarIn.CarInRecord, carOutModel);
                    }
                    var carIn = _vehicleFlow.CarIn(carInModel, permission);
                    if (carIn != null)
                    {
                        await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                        //await deviceInfoDto.Controlable.Camerable.OpenRod(); //抬杆
                        await _ledManager.SpeakAndShowText(deviceInfoDto, carInModel, permission); //播报语音
                        await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(carIn);
                    }
                    else
                    {
                        MessageBox.Show("入场失败");
                        return;
                    }

                }
                else
                {
                    /*
                     出场逻辑中 无场内车辆的记录也弹出    
                    */


                    var isCarIn = _vehicleFlow.IsCarIn(deviceInfoDto.EntranceDto.ParkLevel.Park.Id, carNumber);
                    if (isCarIn.IsCarIn)
                    {

                        var user = _carNumberPermission.GetUser(deviceInfoDto.EntranceDto.ParkLevel.Park.Id, carNumber);
                        if (user != null)
                        {
                            var typeId = user.CarPorts.FirstOrDefault()?.CarPortTypeId;
                            var carType = _repositoryCarType.GetAll().FirstOrDefault(x => x.Id == (typeId.HasValue ? typeId.Value : parkBoxOptions.TempCarTypeId));
                            //月租车正常出场
                            if (carType.Category == CarType.Month)
                            {
                                if (!isCarIn.CarInRecord.IsMonthTempIn)
                                {

                                    var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, new Parks.ParkBox.CarOutModel() { Pay = 0, InOutType = Enum.InOutTypeEnum.Artificial, OutTime = DateTime.Now });
                                    if (outRcode != null)
                                    {
                                        await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(outRcode);
                                    }
                                    else
                                    {

                                        MessageBox.Show( "出场失败!", "提示");
                                    }
                                }
                                else
                                {  //月租车收费
                                    DateTime outTime = DateTime.Now;

                                    FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == parkBoxOptions.TempCarTypeId);
                                    var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                                    fareRule.TimeRangeList = rangTimes;

                                    if (isCarIn.CarInRecord.TempConvertMonthTime.HasValue)
                                    {
                                        outTime = isCarIn.CarInRecord.TempConvertMonthTime.Value;
                                    }
                                    var receivable = fareRule.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0);
                                    var carOutModel = new CarOutModel()
                                    {
                                        CarInRecord = isCarIn.CarInRecord,
                                        InOutType = Enum.InOutTypeEnum.Artificial,
                                        OutTime = DateTime.Now,
                                        Receivable = receivable,
                                        ParkId = deviceInfoDto.EntranceDto.ParkLevel.Park.Id
                                    };
                                    if (parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                                    {
                                        var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, carOutModel);
                                        if (outRcode != null)
                                        {
                                            await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(outRcode);

                                            await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                            await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                        }
                                        else
                                        {

                                            MessageBox.Show( "出场失败!","提示");
                                        }
                                        return;
                                    }


                                    await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.CalculationFee); //播报语音
                                    var tollWindow = await GetChargerWindow(_ledManager, carOutModel, fareRule, receivable, _repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, deviceInfoDto, _repositoryFareRule, _repositoryRangeTime,manualResetEvent);
                                    tollWindow.Init();
                                    tollWindow.Show();
                                    WaitHandle.WaitAll(new WaitHandle[] { manualResetEvent });
                                    var isFree = tollWindow.IsClose;
                                    if (isFree.HasValue && isFree.Value)
                                    {


                                        await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                        await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(tollWindow.CarOutRecord);
                                        await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                    }
                                }
                            }
                            else
                            {
                                //其他车类型
                                DateTime outTime = DateTime.Now;

                                FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == carType.Id);
                                var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                                fareRule.TimeRangeList = rangTimes;

                                var receivable = fareRule?.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0) ?? 0;
                                var carOutModel = new CarOutModel()
                                {
                                    CarInRecord = isCarIn.CarInRecord,
                                    InOutType = Enum.InOutTypeEnum.Artificial,
                                    OutTime = outTime,
                                    Receivable = receivable,
                                    ParkId = deviceInfoDto.EntranceDto.ParkLevel.Park.Id
                                };

                                if (parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                                {
                                    var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, carOutModel);
                                    if (outRcode != null)
                                    {

                                        await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                        await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(outRcode);
                                        await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                    }
                                    else
                                    {

                                        MessageBox.Show( "出场失败!", "提示");
                                    }
                                    return;
                                }

                                await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.CalculationFee); //播报语音
                                var tollWindow = await GetChargerWindow(_ledManager, carOutModel, fareRule, receivable, _repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, deviceInfoDto, _repositoryFareRule, _repositoryRangeTime,manualResetEvent);
                                tollWindow.Init();
                                tollWindow.Show();
                                WaitHandle.WaitAll(new WaitHandle[] { manualResetEvent });
                                var isFree = tollWindow.IsClose;
                                if (isFree.HasValue && isFree.Value)
                                {


                                    await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                    await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(tollWindow.CarOutRecord);


                                    await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                }
                            }
                        }
                        else
                        {  //临时车

                            DateTime outTime = DateTime.Now;

                            FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == parkBoxOptions.TempCarTypeId);

                            var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                            fareRule.TimeRangeList = rangTimes;

                            var receivable = fareRule?.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0) ?? 0;

                            var carOutModel = new CarOutModel()
                            {
                                CarInRecord = isCarIn.CarInRecord,
                                InOutType = Enum.InOutTypeEnum.Artificial,
                                OutTime = outTime,
                                Receivable = receivable,
                                ParkId = deviceInfoDto.EntranceDto.ParkLevel.Park.Id

                            };

                            if (parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                            {
                                var outRcode = _vehicleFlow.CarOut(isCarIn.CarInRecord, carOutModel);
                                if (outRcode != null)
                                {
                                    await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                    await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(outRcode);
                                    await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                }
                                else
                                {

                                    MessageBox.Show( "出场失败!","提示");
                                }
                                return;
                            }

                            await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.CalculationFee); //播报语音
                            var tollWindow = await GetChargerWindow(_ledManager, carOutModel, fareRule, receivable, _repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, deviceInfoDto, _repositoryFareRule, _repositoryRangeTime,manualResetEvent);
                            tollWindow.Init();
                            tollWindow.Show();
                            var isFree = tollWindow.IsClose;
                            if (isFree.HasValue && isFree.Value)
                            {



                                await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(tollWindow.CarOutRecord);
                                await _ledManager.SpeakAndShowText(deviceInfoDto, carOutModel, OutEnum.SuccessfulPayment); //播报语音


                            }
                        }

                    }
                    else
                    {
                        Logger.Info(carNumber + " 无场内记录");
                        //无在场记录如果为月租车直接放行  否则弹出收费框
                        var user = _carNumberPermission.GetUser(deviceInfoDto.EntranceDto.ParkLevel.Park.Id, carNumber);
                        if (user != null)
                        {
                            var model = new Parks.ParkBox.CarOutModel() { Pay = 0, InOutType = Enum.InOutTypeEnum.Artificial, OutTime = DateTime.Now, ParkId = deviceInfoDto.EntranceDto.ParkLevel.Park.Id };
                            var outRcode = _vehicleFlow.CarOut(carNumber, user, model);
                            if (outRcode != null)
                            {


                                await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(outRcode);


                                await _ledManager.SpeakAndShowText(deviceInfoDto, model, OutEnum.SuccessfulPayment); //播报语音
                            }
                            else
                            {

                                MessageBox.Show( "出场失败!", "提示");
                            }
                            return;
                        }
                        else
                        {
                            FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == parkBoxOptions.TempCarTypeId);
                            var rangTimes = _repositoryRangeTime.GetAllIncluding(x => x.FareRule).Where(x => x.FareRuleId == fareRule.Id).ToList();
                            fareRule.TimeRangeList = rangTimes;

                            ///弹出收费框
                            var tollWindow = await GetChargerWindow(_ledManager, carNumber, fareRule, InOutTypeEnum.Photo, _repositoryCarType, parkBoxOptions, _repositoryCarPort, _vehicleFlow, deviceInfoDto, _repositoryFareRule, _repositoryRangeTime,manualResetEvent);
                            tollWindow.Init();
                            tollWindow.Show();
                            WaitHandle.WaitAll(new WaitHandle[] { manualResetEvent });
                            var isFree = tollWindow.IsClose;
                            if (isFree.HasValue && isFree.Value)
                            {


                                await deviceInfoDto.Controlable?.Camerable?.OpenRod();
                                await parkBoxOptions.SetInfosDic?[deviceInfoDto.EntranceDto.Id]?.SetInfo(tollWindow.CarOutRecord);


                                await _ledManager.SpeakAndShowText(deviceInfoDto, tollWindow.CarOutModel, OutEnum.SuccessfulPayment); //播报语音


                            }
                            return;
                        }
                    }
                }


                await unitOfWork.CompleteAsync();
            }
        }
    }
}
