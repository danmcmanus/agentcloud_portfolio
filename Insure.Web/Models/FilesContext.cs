using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Backload.Contracts.Context;
using Backload.Contracts.Eventing;
using Backload.Contracts.Services.Database;

namespace Insure.Web.Models
{
    public class FilesContext : DbContext, IBackloadStorageProvider
    {

        public IEnumerable<IBackloadStorageProviderFile> Get(ICommandArgument args)
        {
            return this.Files;
        }
        public IBackloadStorageProviderFile Add(ICommandArgument args)
        {
            var f = this.Files.Add(new File(args.FileContext));
            if (args.SaveChanges) this.SaveChanges();

            return f;
        }
        public IBackloadStorageProviderFile Update(ICommandArgument args)
        {
            File f = this.Files.Find(args.FileId);

            // Update or add file to context
            if (f != null) f.Update(args.FileContext);
            else f = this.Files.Add(new File(args.FileContext));
            if (args.SaveChanges) this.SaveChanges();

            return f;
        }
        public IBackloadStorageProviderFile Remove(ICommandArgument args)
        {
            var file = this.Files.Find(args.FileId);
            if (file != null)
            {
                this.Files.Remove(file);
                if (args.SaveChanges) this.SaveChanges();
            }

            return file;
        }
        public IEnumerable<T> SqlQuery<T>(ICommandArgument args)
        {
            return this.Database.SqlQuery<T>(args.SqlCommand, args.SqlParameter);
        }
        public int SqlExecute(ICommandArgument args)
        {
            return this.Database.ExecuteSqlCommand(args.SqlCommand, args.SqlParameter);
        }

        public FilesContext()
        {
        }
            public DbSet<AppUser> AppUsers { get; set; }
            public DbSet<Company> Companies { get; set; }
            public DbSet<Policy> Policies { get; set; }
            public System.Data.Entity.DbSet<Insure.Web.Models.Calculator> Calculators { get; set; }
            public System.Data.Entity.DbSet<Insure.Web.Models.Salesforce1.Contact> Contacts { get; set; }
            public DbSet<File> Files { get; set; }
    }
}

