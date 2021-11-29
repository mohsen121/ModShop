using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Domain.Entities;
using ModShop.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ModShop.Application.Common.Interfaces
{
    public interface IAppDb
    {


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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<TEntity> Add<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity;
        Task<TEntity> Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity;
        Task Delete<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseEntity;
        DbSet<T> Set<T>() where T : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        public DatabaseFacade Db { get; set; }
    }
}
