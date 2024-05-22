using Microsoft.EntityFrameworkCore;
using Test_Site_1.Domain.Entities.Products;
using Test_Site_1.Domain.Entities.Users;

namespace Test_Site_1.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }
        DbSet<ProductImages> ProductImages { get; set; }
        DbSet<ProductFeatures> ProductFeatures { get; set; }


        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
