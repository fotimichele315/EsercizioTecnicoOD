
using System.Xml.Serialization;
using EsercizioTecnicoOD.Core.Interfaces;
using EsercizioTecnicoOD.Core.Models;

namespace EsercizioTecnicoOD.Infrastructure.Services;

public class XmlParserService : IXmlParserService
{
    public Commesse Parse(string xml)
    {
        var serializer = new XmlSerializer(typeof(Commesse));

        using var reader = new StringReader(xml);

        var result = serializer.Deserialize(reader);

        if (result is not Commesse commesse)
        {
            throw new Exception("Errore durante la deserializzazione XML.");
        }

        return commesse;
    }
}