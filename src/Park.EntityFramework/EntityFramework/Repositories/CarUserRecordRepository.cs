using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.Extensions;
using Park.Entitys.CarUsers;
using Park.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.EntityFramework.Repositories
{
    public class CarUserRecordRepository : ParkRepositoryBase<CarUsers,long>, ICarUserRepository
    {
        public CarUserRecordRepository(IDbContextProvider<ParkDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CarUsers GetUser(string CarNumber)
        {
            if (CarNumber.IsNullOrEmpty())
                return null;
            var carUser = GetAll();

            return carUser.Where(x => x.CarNumbers.Any(z => z.CarNumber == CarNumber))
                .Include(x => x.Park)
                .Include(x => x.ParkArea)
                .Include(x => x.CarPorts)
                .Include(x => x.CarNumbers)
                .FirstOrDefault();
        }
    }
}
