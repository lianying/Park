using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Park.Entitys.Box;
using Park.Entitys.CarUsers;
using Park.Entitys.ParkEntrances;
using Park.Expansions;
using Park.IRepositories;
using Park.Parks.ParkBox.Models;

namespace Park.Parks.ParkBox.Interfaces
{
    public class CarNumberPermission : ICarNumberPermission
    {

        private readonly IRepository<CarUsers,long> _carUserRepository;
        private readonly IRepository<ParkEntrances, long> _repository;
        private readonly IRepository<CarInRecord,long> _carInRecordRepository;
        private readonly IRepository<CarPort, long> _carPortRepository;
        public CarNumberPermission(IRepository<CarUsers,long> carUserRepository,
            IRepository<ParkEntrances,long> repository,
            IRepository<CarInRecord,long> carInRecordRepository,
            IRepository<CarPort, long> carPortRepository) {
            _carUserRepository = carUserRepository;
            _repository = repository;
            _carInRecordRepository = carInRecordRepository;
            _carPortRepository = carPortRepository;
        }

        public PermissionResult CheckCarNumberPermission(string number,long entranceId)
        {


            var entrance = _repository.GetAllIncluding(x => x.ParkEntrancePermission).Where(x => x.Id == entranceId).FirstOrDefault();

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



            var user = _carUserRepository.GetAllIncluding(x => x.CarNumbers, x => x.CarPorts, x => x.Park, x => x.ParkArea)
                .Where(x => x.CarNumbers.Any(i => i.CarNumber.Equals(number)))
                .InculdeIn(x => x.CarPorts.Select(z => z.CarPortType))
                .InculdeIn(x=>x.CarPorts.Select(z=>z.ParkLevel))
                .InculdeIn(x => x.CarPorts.Select(z => z.ParkArea))
                .FirstOrDefault();
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

        public CarUsers GetUser(int parkId, string number)
        {
            var user = _carUserRepository.GetAllIncluding(x => x.CarNumbers, x => x.Park, x => x.ParkArea).Where(x => x.CarNumbers.Any(i => i.CarNumber.Equals(number))).FirstOrDefault();

            if (user != null)
            {
                var carport = _carPortRepository.GetAll().FirstOrDefault(x => x.StartTime <= DateTime.Now && x.EndTime >= DateTime.Now && x.CarUserId == user.Id);

                user.CarPorts = new List<CarPort>() { carport };
            }
            return user;
            
        }
    }
}
