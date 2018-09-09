using System.Data.Common;
using Abp.Zero.EntityFramework;
using Park.Authorization.Roles;
using Park.Authorization.Users;
using Park.MultiTenancy;
using System.Data.Entity;
using Park.Entitys.Parks;
using System.ComponentModel.DataAnnotations.Schema;
using Park.Entitys.ParkLevels;
using Park.Entitys.ParkEntrances;
using Park.Entitys.CarTypes;
using Park.Entitys.ParkAreas;
using Park.Entitys.CarUsers;
using Park.Entitys.Box;
using Park.Entitys.FareRules;
using Park.Entitys.Devices;

namespace Park.EntityFramework
{
    public class ParkDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

            
        public virtual IDbSet<ParkSet> Parks { get; set; }

        public virtual IDbSet<ParkLevels> ParkLevels { get; set; }

        public virtual IDbSet<ParkEntrances> ParkEntrances { get; set; }

        public virtual IDbSet<CarTypes> CarTypes { get; set; }

        public virtual IDbSet<ParkEntrancePermission> ParkEntrancePermissions { get; set; }

        public virtual IDbSet<ParkAreas> ParkAreas { get; set; }

        //public virtual IDbSet<Device> Devices { get; set; }

        public virtual IDbSet<CarUsers> CarUsers { get; set; }

        public virtual IDbSet<CarNumbers> CarNumbers { get; set; }

        public virtual IDbSet<CarPort> CarPorts { get; set; }


        public virtual IDbSet<CarInRecord> CarIns { get; set; }

        public virtual IDbSet<CarOutRecord> CarOuts { get; set; }


        public virtual IDbSet<CarDiscount> CarDiscounts { get; set; }


        public virtual IDbSet<FareRule> FareRules { get; set; }


        public virtual IDbSet<RangeTime> RangeTimes { get; set; }


        public virtual IDbSet<BlackList> BlackLists { get; set; }

        public virtual IDbSet<CarErrorRecord> CarErrorRecords { get; set; }

        public virtual IDbSet<CarUserGroup> CarUserGroups { get; set; }

        public virtual IDbSet<MonthFee> MonthFees { get; set; }

        public virtual IDbSet<RechargeRecord> RechargeRecords { get; set; }

        public virtual IDbSet<Camera> Cameras { get; set; }


        public virtual IDbSet<Led> Leds { get; set; }









        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ParkDbContext()
            : base("Default")
        {
            
        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ParkDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ParkDbContext since ABP automatically handles it.
         */
        public ParkDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //this.Configuration.LazyLoadingEnabled = false; //关闭延迟加载
        }

        //This constructor is used in tests
        public ParkDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public ParkDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
