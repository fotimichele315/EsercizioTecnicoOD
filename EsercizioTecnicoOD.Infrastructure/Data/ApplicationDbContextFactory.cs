using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EsercizioTecnicoOD.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // 🔥 IMPORTANTE: NON serve Docker qui
        var connectionString =
            "server=localhost;port=3306;database=esercitazioneod_db;user=esercitazioneod_user;password=dev_password";

        optionsBuilder.UseMySql(
       connectionString,
       new MySqlServerVersion(new Version(8, 0, 0))
   );

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}