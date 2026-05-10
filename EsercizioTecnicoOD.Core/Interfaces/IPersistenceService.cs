using EsercizioTecnicoOD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioTecnicoOD.Core.Interfaces
{
    public interface IPersistenceService
    {
        Task SaveAsync(Commesse commesse);
    }
}
