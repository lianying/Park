﻿using Park.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Park.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly ParkDbContext _context;

        public InitialHostDbBuilder(ParkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
