using EsercizioTecnicoOD.Core.Interfaces;
using EsercizioTecnicoOD.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioTecnicoOD.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApiSettings _settings;

        public AuthService(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetApiKey()
        {
            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                throw new Exception("API Key non configurata.");
            }

            return _settings.ApiKey;
        }
    }
}