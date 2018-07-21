using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Park.Entitys.Box;
using Park.Expansions;
using Park.IRepositories;
using Park.Parks.ParkBox.Models;

namespace Park.Parks.ParkBox.Interfaces
{
    public class VehicleFlow : IVehicleFlow
    {
        private readonly IRepository<CarInRecord,long> _carInRecordRepository;
        private readonly IRepository<CarOutRecord, long> _carOutRecordRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public VehicleFlow(IRepository<CarInRecord, long> carInRecordRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<CarOutRecord, long> carOutRecordRepository) {
            _carInRecordRepository = carInRecordRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _carOutRecordRepository = carOutRecordRepository;
        }
        public CarInRecord CarIn(CarInModel carIn, PermissionResult permissionResult)
        {
            var carInRecord = new CarInRecord() { CarNumber=carIn.CarNumber,InType=carIn.InOutType, InTime = carIn.InTime, CarUser = permissionResult.CarUser, CarPort = permissionResult.CarUser?.CarPorts.FirstOrDefault(), ParkId = carIn.Entrance.ParkLevel.Park.Id, CarInCount = 0 };
            var id= _carInRecordRepository.InsertAndGetId(carInRecord);
            _unitOfWorkManager.Current.SaveChanges();
            return _carInRecordRepository.GetAllIncluding(x => x.CarPort, x => x.CarUser, x => x.Park).Where(x => x.Id == id).FirstOrDefault();
        }

        public CarOutRecord CarOut(CarInRecord carIn, CarOutModel carOutModel)
        {
            _carInRecordRepository.Delete(carIn);
            var carOut = new CarOutRecord() { CarInCount = carIn.CarInCount, ParkId= carIn.ParkId, CarId = carIn.CarId, CarNumber = carIn.CarNumber,  InTime = carIn.InTime, Pay = carOutModel.Pay, OutType = carOutModel.InOutType, OutTime = DateTime.Now, Park = carIn.Park };
            var id= _carOutRecordRepository.InsertAndGetId(carOut);

            _unitOfWorkManager.Current.SaveChanges();

            return _carOutRecordRepository.GetAllIncluding(x => x.CarPort, x => x.CarUser, x => x.Park).Where(x => x.Id == id).FirstOrDefault();
        }

        public IsCarInModel IsCarIn(int parkId, string carNumber)
        {
            var carIn = GetCar(parkId, carNumber);
            return new IsCarInModel() { CarInRecord = carIn };
        }


        private CarInRecord GetCar(int parkId,string carNumber)
        {
            var query = _carInRecordRepository.GetAllIncluding(x => x.CarUser, x => x.Park, x => x.CarPort)
                .InculdeIn(x => x.CarPort.ParkLevel)
                .InculdeIn(x => x.CarPort.ParkArea)
                .InculdeIn(x=>x.CarPort.CarPortType)
                .InculdeIn(x=>x.CarUser.ParkArea)
                .InculdeIn(x=>x.CarUser.Park)
                .InculdeIn(x => x.CarUser.CarNumbers)
                .InculdeIn(x => x.CarUser.CarPorts);

            if (parkId > 0) {
                query= query.Where(x => x.ParkId == parkId);
            }
            if (!carNumber.IsNullOrEmpty()) {
                query = query.Where(x => x.CarNumber.Equals(carNumber));
            }
            return query.FirstOrDefault();
            
        }
    }
}
