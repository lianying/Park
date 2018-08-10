using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Handlers;
using Park.Entitys.Box;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.EventHandlers
{
    public class CarOutHandler : IEventHandler<CarOutRecordCreateedEventData>, ITransientDependency
    {

        private readonly IRepository<CarInRecord, long> _repositoryCarIn;
        
        public CarOutHandler(IRepository<CarInRecord,long> repositoryCarIn)
        {
            _repositoryCarIn = repositoryCarIn;
        }

        public void HandleEvent(CarOutRecordCreateedEventData eventData)
        {
            if (eventData?.CarOutRecord != null)
            {
                if (eventData.CarOutRecord.CarId.HasValue && eventData.CarOutRecord.CarId.Value > 0)
                {
                    var carIn = _repositoryCarIn.GetAll().Where(x => x.CarId == eventData.CarOutRecord.CarId.Value).OrderBy(x => x.InTime).FirstOrDefault();
                    if (carIn != null) //占用释放车位
                    {
                        carIn.TempConvertMonthTime = DateTime.Now;
                        _repositoryCarIn.Update(carIn);
                    }
                }

                //调用出场接口


            }
        }
    }
}
