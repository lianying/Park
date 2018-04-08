using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Interfaces
{
    /// <summary>
    /// 数据同步接口 
    /// 将基础数据同步到平台
    /// </summary>
    public interface ISynchronize
    {
        bool IsSuccess { get; set; }

        string CloudId { get; set; }
    }
}
