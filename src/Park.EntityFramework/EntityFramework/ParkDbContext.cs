using System.Data.Common;
using Abp.Zero.EntityFramework;
using Park.Authorization.Roles;
using Park.Authorization.Users;
using Park.MultiTenancy;
using System.Data.Entity;
using Park.Entitys.Parks;
using System.ComponentModel.DataAnnotations.Schema;
using Park.Entitys.ParkLevels;

namespace Park.EntityFramework
{
    public class ParkDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

            
            public virtual IDbSet<JinQuPark> Park { get; set; }

        public virtual IDbSet<ParkLevel> ParkLevel { get; set; }

            

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
