using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsercizioTecnicoOD.Core.Models
{
    public class Item
    {
        public string Id { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string Colore { get; set; } = string.Empty;

        public string Taglia { get; set; } = string.Empty;

        public string Modello { get; set; } = string.Empty;
    }
}
