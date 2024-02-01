namespace AutoLot.Dal.EfStructures
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionOption = new DbContextOptionsBuilder<ApplicationDbContext>();
            string connectionString = @"server=localhost\SQLEXPRESS01;Integrated Security=true;Initial Catalog=AutoLot;Trusted_Connection=True;TrustServerCertificate=true;";
            connectionOption.UseSqlServer(connectionString, option => option.EnableRetryOnFailure());
            return new ApplicationDbContext(connectionOption.Options);
        }
    }
}
