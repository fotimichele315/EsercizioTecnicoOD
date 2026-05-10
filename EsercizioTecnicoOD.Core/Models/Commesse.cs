using EsercizioTecnicoOD.Core.Models;
using System.Xml.Serialization;

namespace EsercizioTecnicoOD.Core.Models;

[XmlRoot("Commesse")]
public class Commesse
{
    [XmlElement("Commessa")]
    public List<Commessa> Items { get; set; } = new();
}