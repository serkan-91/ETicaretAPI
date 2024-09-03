using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Contexts
{
    public class EticaretAPIDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }


        /// <summary>
        /// Interceptor
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // ChangeTracker : Entity uzerinde yapilan degisiklik yada yeni eklenen verinin yakalanmasini saglayan property. Track edilen veriyi yakalayip elde etmenizi saglar.

           var datas = ChangeTracker
                .Entries<BaseEntity>();
                 
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow, 
                    _ => DateTime.UtcNow,
                };
            } 
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
