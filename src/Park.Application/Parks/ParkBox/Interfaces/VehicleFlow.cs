using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Entitys.Box;
using Park.IRepositories;
using Park.Parks.ParkBox.Models;

namespace Park.Parks.ParkBox.Interfaces
{
    public class VehicleFlow : IVehicleFlow
    {
        private readonly ICarInRecordRepository _carInRecordRepository;
        public VehicleFlow(ICarInRecordRepository carInRecordRepository) {
            _carInRecordRepository = carInRecordRepository;
        }
        public bool CarIn(CarInModel carIn, PermissionResult permissionResult)
        {
            throw new NotImplementedException();
        }

        public bool CarOut(CarInRecord carIn, CarOutModel carOutModel)
        {
            throw new NotImplementedException();
        }

        public IsCarInModel IsCarIn(int parkId, string carNumber)
        {
            var carIn = _carInRecordRepository.IsCarIn(parkId, carNumber);
            return new IsCarInModel() { CarInRecord = carIn };
        }
    }
}
