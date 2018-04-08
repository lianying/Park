using System.Linq;
using Park.EntityFramework;
using Park.MultiTenancy;

namespace Park.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly ParkDbContext _context;

        public DefaultTenantCreator(ParkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
