using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using System.Linq;
using System;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _logger;

        public WebStoreDbInitializer(WebStoreDB  db, ILogger<WebStoreDbInitializer> Logger)
        {
            this._db = db;
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
    }
}
