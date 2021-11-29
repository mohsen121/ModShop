using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Persistence
{
    public class MZ121DbContextFactory : DesignTimeDbContextFactoryBase<AppDb>
    {
        protected override AppDb CreateNewInstance(DbContextOptions<AppDb> options)
        {
            return new AppDb(options);
        }
    }
}
