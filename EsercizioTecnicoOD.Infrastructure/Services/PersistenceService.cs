
using EsercizioTecnicoOD.Core.Interfaces;
using EsercizioTecnicoOD.Core.Models;
using EsercizioTecnicoOD.Infrastructure.Data;

namespace EsercitazioneOD.Infrastructure.Services;

public class PersistenceService : IPersistenceService
{
    private readonly ApplicationDbContext _context;

    public PersistenceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveCommesseAsync(Commesse commesse)
    {
        _context.Commesse.AddRange(commesse.Items);
        await _context.SaveChangesAsync();
    }
}