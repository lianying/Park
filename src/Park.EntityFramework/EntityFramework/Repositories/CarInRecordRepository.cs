using Abp.EntityFramework;
using Park.Entitys.Box;
using Park.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.EntityFramework.Repositories
{
    public class CarInRecordRepository : ParkRepositoryBase<CarInRecord, long>, ICarInRecordRepository
    {
        protected CarInRecordRepository(IDbContextProvider<ParkDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<CarInRecord> GetCarInByUserId(long userId)
        {
            
            var query = GetAll();
            if (userId > 0)
            {
                query.Where(x => x.CarId == userId);
            }
            return query.ToList();
        }

        public CarInRecord IsCarIn(int parkId, string CarNumber)
        {
            var query = GetAll();

            query.Where(x => x.ParkId == parkId && x.CarNumber == CarNumber)
                .Include(i => i.Park)
                .Include(i => i.CarPort)
                .Include(i => i.CarUser)
                .Include(i => i.CarInOutImage);

            return query.FirstOrDefault();
        }





    }
}
