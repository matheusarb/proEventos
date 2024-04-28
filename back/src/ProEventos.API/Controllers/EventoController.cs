using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.API.Data;
using ProEventos.Domain;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;

        public EventoController(DataContext context)
        { 
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Evento>> GetEventos()
        {
            var eventos = await _context.Eventos.AsNoTracking().ToListAsync();
            return eventos;
        }

        [HttpGet("{id:int}")]
        public async Task<Evento> GetById(int id)
        {
            var evento = await _context.Eventos.FirstOrDefaultAsync(ev => ev.Id == id);
            return evento;
        }
    }
}
