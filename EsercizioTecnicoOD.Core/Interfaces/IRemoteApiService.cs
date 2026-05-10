using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioTecnicoOD.Core.Interfaces
{
    public interface IRemoteApiService
    {
        Task<string> GetXmlAsync(string apiKey);
    }
}
