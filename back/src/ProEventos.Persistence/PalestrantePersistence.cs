using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<List<Palestrante>> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(pe => pe.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(ev => ev.Evento);
            }

            return await query.OrderBy(pe => pe.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<Palestrante>> GetAllPalestrantesByNomeAsync(
            string nome,
            bool includeEventos = false
        )
        {
            var q2 = _context.Palestrantes.Find();
            IQueryable<Palestrante> query = _context.Palestrantes.Include(pe => pe.RedesSociais);

            if (includeEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(ev => ev.Evento);

            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
            
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(
            int palestranteId,
            bool includeEventos = false
        )
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(pe => pe.RedesSociais);

            if (includeEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(ev => ev.Evento);

            return await query.AsNoTracking().FirstOrDefaultAsync(pe => pe.Id == palestranteId);
        }
    }
}
