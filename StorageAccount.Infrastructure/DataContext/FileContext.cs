using Microsoft.EntityFrameworkCore;
using StorageAccount.Domain.Model;


namespace StorageAccount.Infrastructure.DataContext
{ 
    public class FileContext : DbContext
    {
        public FileContext() { }

        public FileContext(DbContextOptions options) : base(options)
        {
        }
   
        public virtual DbSet<FileOrBlobUpload> FileDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FileUpload;Integrated Security=True;TrustServerCertificate=True;");
    }
}
