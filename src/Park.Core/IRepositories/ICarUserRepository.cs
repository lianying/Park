using Abp.Domain.Repositories;
using Park.Entitys.CarUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.IRepositories
{
    public interface ICarUserRepository : IRepository<CarUsers, long>
    {
        CarUsers GetUser(string CarNumber);
        
    }
}
