using EsercizioTecnicoOD.Core.Interfaces;
using EsercizioTecnicoOD.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using EsercizioTecnicoOD.Infrastructure.Services;
var services = new ServiceCollection();

services.AddHttpClient();

services.AddTransient<IAuthService, AuthService>();
services.AddTransient<IRemoteApiService, RemoteApiService>();
services.AddTransient<IXmlParserService, XmlParserService>();

var serviceProvider = services.BuildServiceProvider();

var authService = serviceProvider.GetRequiredService<IAuthService>();

var remoteApiService = serviceProvider.GetRequiredService<IRemoteApiService>();

var apiKey = authService.GetApiKey();

var xml = await remoteApiService.GetXmlAsync(apiKey);

Console.WriteLine("XML ricevuto:");
Console.WriteLine(xml);

var parserService = serviceProvider.GetRequiredService<IXmlParserService>();

var commesse = parserService.Parse(xml);

Console.WriteLine("Numero commesse:");
Console.WriteLine(commesse.Items.Count);

