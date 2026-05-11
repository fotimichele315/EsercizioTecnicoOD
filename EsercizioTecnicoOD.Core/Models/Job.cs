using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioTecnicoOD.Core.Models
{
    public class Job
    {
        public string Id { get; set; } = string.Empty;
        public string CommessaId { get; set; } = string.Empty;
        public Commessa Commessa { get; set; } = null!;
        public Item Item { get; set; } = null!;
        public string FaseDiLavorazione { get; set; } = string.Empty;
        public string StatoAvanzamento { get; set; } = string.Empty;
    }
}
