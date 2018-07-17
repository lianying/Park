using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Park.Entitys.ParkEntrances;
using Park.Expansions;
using Park.IRepositories;
using Park.Parks.ParkBox.Models;

namespace Park.Parks.ParkBox.Interfaces
{
    public class CarNumberPermission : ICarNumberPermission
    {

        private readonly ICarUserRepository _carUserRepository;
        private readonly IRepository<ParkEntrances, long> _repository;
        private readonly ICarInRecordRepository _carInRecordRepository;
        public CarNumberPermission(ICarUserRepository carUserRepository,
            IRepository<ParkEntrances,long> repository,
            ICarInRecordRepository carInRecordRepository) {
            _carUserRepository = carUserRepository;
            _repository = repository;
            _carInRecordRepository = carInRecordRepository;
        }

        public PermissionResult CheckCarNumberPermission(string number,long entranceId)
        {


            var entrance = _repository.GetAllIncluding(x => x.ParkEntrancePermission).Single(x => x.Id == entranceId);

            if (number.IsNoBrandCar())
            {  //无牌车
                switch (entrance.ParkEntrancePermission.NoNumberOptions)
                {
                    case Enum.NoNumberOptions.CarIn:
                        return new PermissionResult(true, Enum.CarNumberPermissionEnum.TempIn, null, false);

                    case Enum.NoNumberOptions.Confirm:
                        return new PermissionResult(null, Enum.CarNumberPermissionEnum.TempConfimIn, null, false);
                    case Enum.NoNumberOptions.CanNotIn:
                        return new PermissionResult(false, Enum.CarNumberPermissionEnum.TempNotIn, null, false);
                }
            }



            var user = _carUserRepository.GetUser(number);
            if (user == null) //临时车
            {
                if (entrance.ParkEntrancePermission.IsTempCarIn)
                {
                    return new PermissionResult(true, Enum.CarNumberPermissionEnum.TempIn, null, false);
                }
                else if (entrance.ParkEntrancePermission.IsTempCarConfirm)
                {
                    return new PermissionResult(null, Enum.CarNumberPermissionEnum.TempConfimIn, null, false);
                }
                else
                {
                    return new PermissionResult(false, Enum.CarNumberPermissionEnum.TempNotIn, null, false);
                }
            }
            else
            {   //月租车
                return new PermissionResult(true, Enum.CarNumberPermissionEnum.MonthIn, user, false);
            }
        }
    }
}
