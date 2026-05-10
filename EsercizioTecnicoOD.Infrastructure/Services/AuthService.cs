using EsercizioTecnicoOD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioTecnicoOD.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public string GetApiKey()
        {
            return "my-secret-key";
        }
    }
}