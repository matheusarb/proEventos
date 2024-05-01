using System.Collections.Generic;
using System.Threading.Tasks;
using ProEventos.Persistence.Contracts;
using ProEventos.Persistence.Context;

namespace ProEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly ProEventosContext _context;

        public GeralPersist(ProEventosContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity)
            where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity)
            where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity)
            where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(List<T> entities)
            where T : class
        {
            _context.RemoveRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}