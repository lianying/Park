using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Castle.Core.Logging;
using Park.Entitys.Box;
using Park.Entitys.CarTypes;
using Park.Entitys.CarUsers;
using Park.Entitys.FareRules;
using Park.Enum;
using Park.Expansions;
using Park.IRepositories;
using Park.ParkBox;
using Park.Parks.Devices.Interfaces;
using Park.Parks.Entrance;
using Park.Parks.ParkBox.Core;
using Park.Parks.ParkBox.Models;

namespace Park.Parks.ParkBox.Interfaces
{
    public class VehicleFlow : IVehicleFlow
    {
        private readonly IRepository<CarInRecord, long> _carInRecordRepository;
        private readonly IRepository<CarOutRecord, long> _carOutRecordRepository;
        private readonly IRepository<CarPort, long> _carPortrepository;
        private readonly IRepository<CarDiscount, long> _carDiscountrepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;


        private readonly IRepository<CarTypes, long> _repositoryCarType;

        private readonly IRepository<FareRule> _repositoryFareRule;

        private readonly ICarNumberPermission _carNumberPermission;

        private readonly IParkBoxOptions _parkBoxOptions;

        private readonly LedManager _ledManager;

        private readonly IocManager _iocManager;
        private readonly IRepository<CarPort, long> _repositoryCarPort;

        private ILogger Logger { get; set; }
        public VehicleFlow(IRepository<CarInRecord, long> carInRecordRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<CarOutRecord, long> carOutRecordRepository,
           IRepository<CarPort, long> carPortrepository,
           IRepository<CarTypes, long> repositoryCarType,
           IRepository<CarPort, long> repositoryCarPort,
           IRepository<FareRule> repositoryFareRule,
           ICarNumberPermission carNumberPermission,
           IParkBoxOptions parkBoxOption,
           LedManager ledManager,
            IRepository<CarDiscount, long> carDiscountrepository)
        {
            _carInRecordRepository = carInRecordRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _carPortrepository = carPortrepository;
            _carOutRecordRepository = carOutRecordRepository;
            Logger = NullLogger.Instance;
            _repositoryCarType = repositoryCarType;
            _repositoryFareRule = repositoryFareRule;
            _carNumberPermission = carNumberPermission;
            _parkBoxOptions = parkBoxOption;
            _ledManager = ledManager;
            _repositoryCarPort = repositoryCarPort;
            _iocManager = IocManager.Instance;
            _carDiscountrepository = carDiscountrepository;
        }
        public CarInRecord CarIn(CarInModel carIn, PermissionResult permissionResult)
        {
            var carInRecord = new CarInRecord() { CarNumber = carIn.CarNumber, InType = carIn.InOutType, InTime = carIn.InTime, CarUser = permissionResult.CarUser, CarPort = permissionResult.CarUser?.CarPorts.FirstOrDefault(), ParkId = carIn.Entrance.ParkLevel.Park.Id, CarInCount = 0 };
            var id = _carInRecordRepository.InsertAndGetId(carInRecord);
            _unitOfWorkManager.Current.SaveChanges();
            return _carInRecordRepository.GetAllIncluding(x => x.CarPort, x => x.CarUser, x => x.Park).Where(x => x.Id == id).FirstOrDefault();
        }
        public CarOutRecord CarOut(CarInRecord carIn, CarOutModel carOutModel)
        {
            var carOut = new CarOutRecord()
            {
                CarInCount = carIn.CarInCount,
                ParkId = carIn.ParkId,
                CarId = carIn.CarId,
                CarNumber = carIn.CarNumber,
                InTime = carIn.InTime,
                Pay = carOutModel.Pay,
                Receivable = carOutModel.Receivable,
                Remark = carOutModel.Remark,
                CarPortId = carIn.CarPortId,
                IsMonthTempIn = carIn.IsMonthTempIn,
                TempConvertMonthTime = carIn.TempConvertMonthTime,
                PayType = carOutModel.PayType,
                OfferTime = carOutModel.OfferTime,
                OfferMoney = carOutModel.OfferMoney,
                CarInPhotoId = carIn.CarInPhotoId,
                CarOutPhotoId = carOutModel.ImageId,
                InCloudId = carIn.CloudId,
                IsMonthTimeOutInt = carIn.IsMonthTimeOutInt,
                InType = carIn.InType,
                IsErrorOut = carOutModel.IsErrorOut,
                OutTime = carOutModel.OutTime,
                OutPhotoTime = carOutModel.OutPhotoTime
            };
            _carInRecordRepository.Delete(carIn.Id);

            var id = _carOutRecordRepository.InsertAndGetId(carOut);

            _unitOfWorkManager.Current.SaveChanges();

            return _carOutRecordRepository.GetAllIncluding(x => x.CarPort, x => x.CarUser, x => x.Park).Where(x => x.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// 月租车无在场记录 根据车牌出场
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="users"></param>
        /// <param name="carOutModel"></param>
        /// <returns></returns>
        public CarOutRecord CarOut(string carNumber, CarUsers users, CarOutModel carOutModel)
        {
            var carport = _carPortrepository.GetAll().Where(x => x.CarUserId == users.Id).FirstOrDefault();
            var carOut = new CarOutRecord()
            {
                InTime = DateTime.Now,
                CarId = users?.Id,
                OutTime = carOutModel.OutTime,
                InType = carOutModel.InOutType,
                Remark = "无在场记录出场",
                CarPort = carport,
                CarInCount = 0,
                CarNumber = carNumber,
                CarOutPhotoId = carOutModel.ImageId,
                OutPhotoTime = carOutModel.OutPhotoTime,
                OutType = carOutModel.InOutType,
                Pay = carOutModel.Pay,
                AdvancePayment = 0,
                ParkId = carOutModel.ParkId
            };
            var id = _carOutRecordRepository.InsertAndGetId(carOut);



            return _carOutRecordRepository.GetAllIncluding(x => x.CarPort, x => x.CarUser, x => x.Park).Where(x => x.Id == id).FirstOrDefault();

        }

        public async void CarOutRecord(string carNumber, EntranceDto entranceDto, IDeviceable deviceable, Action openRod, Action<CarOutRecord> setOutInfo, Action<string, string> showMessage)
        {
            var isCarIn = IsCarIn(entranceDto.ParkLevel.Park.Id, carNumber);
            if (isCarIn.IsCarIn)
            {

                var user = _carNumberPermission.GetUser(entranceDto.ParkLevel.Park.Id, carNumber);
                if (user != null)
                {
                    var typeId = user.CarPorts.FirstOrDefault()?.CarPortTypeId;
                    var carType = _repositoryCarType.GetAll().FirstOrDefault(x => x.Id == (typeId.HasValue ? typeId.Value : _parkBoxOptions.TempCarTypeId));
                    //月租车正常出场
                    if (carType.Category == CarType.Month)
                    {
                        if (!isCarIn.CarInRecord.IsMonthTempIn)
                        {

                            var outRcode = CarOut(isCarIn.CarInRecord, new Parks.ParkBox.CarOutModel() { Pay = 0, InOutType = Enum.InOutTypeEnum.Artificial, OutTime = DateTime.Now });
                            if (outRcode != null)
                            {
                                setOutInfo?.Invoke(outRcode);
                            }
                            else
                            {

                                showMessage?.Invoke("提示", "出场失败!");
                            }
                        }
                        else
                        {  //月租车收费
                            DateTime outTime = DateTime.Now;

                            FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == _parkBoxOptions.TempCarTypeId);
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
                                ParkId = entranceDto.ParkLevel.Park.Id
                            };
                            if (_parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                            {
                                var outRcode = CarOut(isCarIn.CarInRecord, carOutModel);
                                if (outRcode != null)
                                {
                                    openRod?.Invoke();
                                    setOutInfo?.Invoke(outRcode);
                                    await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                                }
                                else
                                {

                                    showMessage?.Invoke("提示", "出场失败!");
                                }
                                return;
                            }


                            await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.CalculationFee); //播报语音
                            var tollWindow = _iocManager.Resolve<ICharger>(new { carOutModel = carOutModel, fareRule = fareRule, receivable = receivable, _repositoryCarType, parkBoxOptions = _parkBoxOptions, _repositoryCarPort, vehicleFlow = this });
                            tollWindow.Init(false);
                            var isFree = tollWindow.ShowDialog();
                            if (isFree.HasValue && isFree.Value)
                            {


                                openRod?.Invoke();
                                setOutInfo?.Invoke(tollWindow.CarOutRecord);
                                await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                            }
                        }
                    }
                    else
                    {
                        //其他车类型
                        DateTime outTime = DateTime.Now;

                        FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == carType.Id);

                        var receivable = fareRule?.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0) ?? 0;
                        var carOutModel = new CarOutModel()
                        {
                            CarInRecord = isCarIn.CarInRecord,
                            InOutType = Enum.InOutTypeEnum.Artificial,
                            OutTime = outTime,
                            Receivable = receivable,
                            ParkId = entranceDto.ParkLevel.Park.Id
                        };

                        if (_parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                        {
                            var outRcode = CarOut(isCarIn.CarInRecord, carOutModel);
                            if (outRcode != null)
                            {
                                openRod?.Invoke();
                                setOutInfo?.Invoke(outRcode);
                                await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                            }
                            else
                            {

                                showMessage?.Invoke("提示", "出场失败!");
                            }
                            return;
                        }

                        await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.CalculationFee); //播报语音
                        var tollWindow = _iocManager.Resolve<ICharger>(new { carOutModel = carOutModel, fareRule = fareRule, receivable = receivable, _repositoryCarType, parkBoxOptions = _parkBoxOptions, _repositoryCarPort, vehicleFlow = this });
                        tollWindow.Init(false);
                        var isFree = tollWindow.ShowDialog();
                        if (isFree.HasValue && isFree.Value)
                        {


                            openRod?.Invoke();
                            setOutInfo?.Invoke(tollWindow.CarOutRecord);


                            await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                        }
                    }
                }
                else
                {  //临时车

                    DateTime outTime = DateTime.Now;

                    FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == _parkBoxOptions.TempCarTypeId);


                    var receivable = fareRule?.CalculateFees(isCarIn.CarInRecord.InTime, outTime, 0) ?? 0;

                    var carOutModel = new CarOutModel()
                    {
                        CarInRecord = isCarIn.CarInRecord,
                        InOutType = Enum.InOutTypeEnum.Artificial,
                        OutTime = outTime,
                        Receivable = receivable,
                        ParkId = entranceDto.ParkLevel.Park.Id
                    };

                    if (_parkBoxOptions.IsZeroMoneyOut && receivable == 0) //收费为0 直接放行
                    {
                        var outRcode = CarOut(isCarIn.CarInRecord, carOutModel);
                        if (outRcode != null)
                        {
                            openRod?.Invoke();
                            setOutInfo?.Invoke(outRcode);
                            await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.SuccessfulPayment); //播报语音
                        }
                        else
                        {

                            showMessage?.Invoke("提示", "出场失败!");
                        }
                        return;
                    }

                    await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.CalculationFee); //播报语音
                    var tollWindow = _iocManager.Resolve<ICharger>(new { carOutModel = carOutModel, fareRule = fareRule, receivable = receivable, _repositoryCarType, parkBoxOptions = _parkBoxOptions, _repositoryCarPort, vehicleFlow = this });
                    tollWindow.Init(false);
                    var isFree = tollWindow.ShowDialog();
                    if (isFree.HasValue && isFree.Value)
                    {


                        openRod?.Invoke();
                        setOutInfo?.Invoke(tollWindow.CarOutRecord);


                        await _ledManager.SpeakAndShowText(deviceable, carOutModel, OutEnum.SuccessfulPayment); //播报语音


                    }
                }

            }
            else
            {
                Logger.Info(carNumber + " 无场内记录");
                //无在场记录如果为月租车直接放行  否则弹出收费框
                var user = _carNumberPermission.GetUser(entranceDto.ParkLevel.Park.Id, carNumber);
                if (user != null)
                {
                    var model = new Parks.ParkBox.CarOutModel() { Pay = 0, InOutType = Enum.InOutTypeEnum.Artificial, OutTime = DateTime.Now, ParkId = entranceDto.ParkLevel.Park.Id };
                    var outRcode = CarOut(carNumber, user, model);
                    if (outRcode != null)
                    {

                        openRod?.Invoke();
                        setOutInfo?.Invoke(outRcode);


                        await _ledManager.SpeakAndShowText(deviceable, model, OutEnum.SuccessfulPayment); //播报语音
                    }
                    else
                    {

                        showMessage?.Invoke("提示", "出场失败!");
                    }
                    return;
                }
                else
                {
                    FareRule fareRule = _repositoryFareRule.GetAll().FirstOrDefault(x => x.CarTypeId == _parkBoxOptions.TempCarTypeId);
                    ///弹出收费框
                    var tollWindow = _iocManager.Resolve<ICharger>(new { ledManager = _ledManager, carNumber = carNumber, fareRule = fareRule, parkBoxOptions = _parkBoxOptions, vehicleFlow = this });
                    tollWindow.Init(false);

                    var isFree = tollWindow.ShowDialog();
                    if (isFree.HasValue && isFree.Value)
                    {

                        openRod?.Invoke();
                        setOutInfo?.Invoke(tollWindow.CarOutRecord);


                        await _ledManager.SpeakAndShowText(deviceable, tollWindow.CarOutModel, OutEnum.SuccessfulPayment); //播报语音


                    }
                    return;
                }
                //await this.ShowMessageAsync("提示", "当前车辆不在场内!");
            }
        }

        public IsCarInModel IsCarIn(int parkId, string carNumber)
        {
            var carIn = GetCar(parkId, carNumber);
            return new IsCarInModel() { CarInRecord = carIn };
        }


        private CarInRecord GetCar(int parkId, string carNumber)
        {
            var query = _carInRecordRepository.GetAllIncluding(x => x.CarUser, x => x.Park, x => x.CarPort)
                .InculdeIn(x => x.CarPort.ParkLevel)
                .InculdeIn(x => x.CarPort.ParkArea)
                .InculdeIn(x => x.CarPort.CarPortType)
                .InculdeIn(x => x.CarUser.ParkArea)
                .InculdeIn(x => x.CarUser.Park)
                .InculdeIn(x => x.CarUser.CarNumbers)
                .InculdeIn(x => x.CarUser.CarPorts);

            if (parkId > 0)
            {
                query = query.Where(x => x.ParkId == parkId);
            }
            if (!carNumber.IsNullOrEmpty())
            {
                query = query.Where(x => x.CarNumber.Equals(carNumber));
            }
            return query.FirstOrDefault();

        }

        public List<CarInRecord> LevenshteinDistance(int parkId, string carNumber)
        {

            var query = _carInRecordRepository.GetAll().Where(x => x.CarNumber != string.Empty);
            if (carNumber.IsNullOrEmpty()) //车牌为空返回前5条
            {
                return query.Take(5).ToList();
            }
            if (parkId > 0)
            {
                query = query.Where(x => x.ParkId == parkId);
            }
            //取100条场内记录比较
            var tempList = query.OrderByDescending(x => x.InTime).Take(100).Where(x => x.CarNumber.Length == carNumber.Length).ToList();
            Dictionary<CarInRecord, float> pairs = new Dictionary<CarInRecord, float>();
            float similarity = 0;
            foreach (var item in tempList)
            {
                similarity = Levenshtein(item.CarNumber, carNumber);
                pairs.Add(item, similarity);
            }

            return pairs.OrderBy(t => t.Value).ToDictionary(t => t.Key, t => t.Value).Take(5).Select(x => x.Key).ToList();
            

            
        }

        /// <summary>
        /// 字符串相似度算法
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static float Levenshtein(string str1, string str2)
        {
            char[] char1 = str1.ToCharArray();
            char[] char2 = str2.ToCharArray();
            //计算两个字符串的长度。  
            int len1 = char1.Length;
            int len2 = char2.Length;
            //建二维数组，比字符长度大一个空间  
            int[,] dif = new int[len1 + 1, len2 + 1];
            //赋初值  
            for (int a = 0; a <= len1; a++)
            {
                dif[a, 0] = a;
            }
            for (int a = 0; a <= len2; a++)
            {
                dif[0, a] = a;
            }
            //计算两个字符是否一样，计算左上的值  
            int temp;
            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    if (char1[i - 1] == char2[j - 1])
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = 1;
                    }
                    //取三个值中最小的  
                    dif[i, j] = Min(dif[i - 1, j - 1] + temp, dif[i, j - 1] + 1, dif[i - 1, j] + 1);
                }
            }
            //计算相似度  
            float similarity = 1 - (float)dif[len1, len2] / Math.Max(len1, len2);
            return similarity;
        }


        private static int Min(params int[] nums)
        {
            int min = int.MaxValue;
            foreach (int item in nums)
            {
                if (min > item)
                {
                    min = item;
                }
            }
            return min;
        }

        public CarDiscount GetCarDiscount(int parkId, string carNumber)
        {
            return _carDiscountrepository.GetAll().FirstOrDefault(x => x.ParkId == parkId && x.CarNumber == carNumber && DateTime.Now <= x.DiscountExpire);
        }
    }
}
