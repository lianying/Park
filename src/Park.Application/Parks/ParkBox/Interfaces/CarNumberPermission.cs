using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
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

        public PermissionResult CheckCarNumberPermission(string number, long entranceId)
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
                .InculdeIn(x => x.CarPorts.Select(z => z.ParkLevel))
                .InculdeIn(x => x.CarPorts.Select(z => z.ParkArea))
                .InculdeIn(x => x.ParkArea.Park)
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

                var inCount = _carInRecordRepository.GetAll().Where(x => x.CarId == user.Id && x.CarNumber != number).Select(x => x.CarNumber).ToList();
                //判断出入口权限
                bool hasPermission = false;
                foreach (var item in user.CarPorts)
                {
                    if (entrance.ParkEntrancePermission.CarTypes.Any(x => x.Id == item.CarPortTypeId))
                    {
                        hasPermission = true;
                        break;
                    }
                }
                if (!hasPermission)
                {
                    return new PermissionResult(false, Enum.CarNumberPermissionEnum.NoPermissionNotIn, user, false, inCount.Count, inCount);
                }
                if (inCount.Count >= user.CarPorts.Count)
                {
                    if (user.FullInType == Enum.FullInType.Temp)
                        return new PermissionResult(true, Enum.CarNumberPermissionEnum.CarportsFullIn, user, true, inCount.Count, inCount);
                    else

                        return new PermissionResult(false, Enum.CarNumberPermissionEnum.CarportsFullNotIn, user, false, inCount.Count, inCount);
                }
                else
                    return new PermissionResult(true, Enum.CarNumberPermissionEnum.MonthIn, user, false, inCount.Count, inCount);
            }
        }

        public CarUsers GetUser(int parkId, string number)
        {
            var user = _carUserRepository.GetAllIncluding(x => x.CarNumbers, x => x.Park, x => x.ParkArea, x => x.CarPorts)
                .Where(x => x.CarNumbers.Any(i => i.CarNumber.Equals(number)))
                .Where(x => x.CarPorts.Any(i => i.StartTime <= DateTime.Now && i.EndTime >= DateTime.Now))
                .InculdeIn(x=>x.CarPorts.Select(i=>i.CarPortType))
                .FirstOrDefault();
            
            return user;
            
        }
    }
}
