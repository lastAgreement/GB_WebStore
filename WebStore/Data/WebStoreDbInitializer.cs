using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;
using System.Threading.Tasks;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<WebStoreDbInitializer> _logger;

        public WebStoreDbInitializer(
            WebStoreDB  db, 
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILogger<WebStoreDbInitializer> Logger)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = Logger;
        }

        public void Initialize()
        {
            Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade db = _db.Database;
            if (db.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Has pending migrations..");
                db.Migrate();
                _logger.LogInformation("Success");
            }
            else _logger.LogInformation("No pending migrations."); 
            

            try 
            { 
                InitializeProducts(); 
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Initialize products data exception.\n" + ex.ToString());
            }
            try
            {
                InitializeIdentityAsync().Wait();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Initialize identity exception.\n" + ex.ToString());
            }

        }

        private void InitializeProducts()
        {
            if (_db.Products.Any())
                return;

            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                _db.Database.CommitTransaction();
            }
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                _db.Database.CommitTransaction();
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                _db.Database.CommitTransaction();
            }
        }

        private async Task InitializeIdentityAsync()
        {
            async Task CheckRole(string RoleName)
            {
                if (!await _roleManager.RoleExistsAsync(RoleName))
                    await _roleManager.CreateAsync(new Role { Name = RoleName });
            }

            await CheckRole(Role.Administrator);
            await CheckRole(Role.User);

            if (await _userManager.FindByNameAsync(User.Administrator) is null)
            {
                var admin = new User { UserName = User.Administrator };
                var creation_result = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                    await _userManager.AddToRoleAsync(admin, Role.Administrator);
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании учетной записи администратора {string.Join(',', errors)}");
                }

            }
        }
    }
}
