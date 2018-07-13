using Abp.EntityFramework;
using Park.Entitys.Parks;
using Park.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.EntityFramework.Repositories
{
    public class ParkRepository:ParkRepositoryBase<ParkSet>,IParkRepository
    {
        public ParkRepository(IDbContextProvider<ParkDbContext> dbContextProvider) : base(dbContextProvider) {

        }
    }
}
