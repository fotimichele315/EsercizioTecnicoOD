using EsercitazioneOD.Infrastructure.Services;
using EsercizioTecnicoOD.Core.Interfaces;
using EsercizioTecnicoOD.Infrastructure.Configuration;
using EsercizioTecnicoOD.Infrastructure.Data;
using EsercizioTecnicoOD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
try
{
    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

    var services = new ServiceCollection();
    services.AddHttpClient();
    services.AddDbContext<ApplicationDbContext>(options =>
    {
        var connectionString =
            configuration.GetConnectionString("DefaultConnection");

        options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });
    services.AddTransient<IAuthService, AuthService>();
    services.AddTransient<IRemoteApiService, RemoteApiService>();
    services.AddTransient<IXmlParserService, XmlParserService>();
    services.AddTransient<IPersistenceService, PersistenceService>();
    services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

    var serviceProvider = services.BuildServiceProvider();

    var authService = serviceProvider.GetRequiredService<IAuthService>();
    var apiKey = authService.GetApiKey();

    var remoteApiService = serviceProvider.GetRequiredService<IRemoteApiService>();
    var xml = await remoteApiService.GetXmlAsync(apiKey);

    var parserService = serviceProvider.GetRequiredService<IXmlParserService>();
    var commesse = parserService.Parse(xml);

    var persistenceService = serviceProvider.GetRequiredService<IPersistenceService>();
    await persistenceService.SaveCommesseAsync(commesse);
} catch (HttpRequestException ex)
{
    Console.WriteLine("Errore HTTP durante la chiamata alla API remota.");
    Console.WriteLine(ex.Message);
} catch (DbUpdateException ex)
{
    Console.WriteLine("Errore durante il salvataggio dei dati nel database.");
    Console.WriteLine(ex.Message);
} catch (Exception ex)
{
    Console.WriteLine("Errore applicativo.");
    Console.WriteLine(ex.Message);
}