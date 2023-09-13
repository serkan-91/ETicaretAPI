using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Persistence.Contexts
{
    public class EticaretAPIDbContext : DbContext
    {
        public EticaretAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }


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
