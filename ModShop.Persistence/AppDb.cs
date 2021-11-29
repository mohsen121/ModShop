using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain;
using ModShop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ModShop.Persistence
{
    public class AppDb : IdentityDbContext<User, IdentityRole, string>, IAppDb
    {
        public AppDb(DbContextOptions<AppDb> options)
            : base(options)
        {

            Db = this.Database;
        }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }


        public DatabaseFacade Db { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDb).Assembly);
            base.OnModelCreating(builder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            var entry = await base.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public Task<TEntity> Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return Task.FromResult(base.Set<TEntity>().Update(entity).Entity);
        }

        public Task Delete<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return Task.FromResult(base.Set<TEntity>().Remove(entity));
        }

        public override DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        {
            return base.Entry(entity);
        }

        [DbFunction("fn_ConvertToString", schema: "mzir12_modshop")]
        public static string ConvertToString(int input)
        {
            throw new NotImplementedException();
        }
    }
}
