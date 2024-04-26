using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private IEnumerable<Evento> _evento = new Evento[]{
                new Evento {
                Id = 1,
                Tema = "Angular 11 e .NET 5",
                Local = "Belo Horizonte",
                Lote = "1º Lote",
                QtdPessoas = 250,
                Data = DateTime.Now.AddDays(2).ToString(),
                ImagemURL = "photo.png"
            },
                new Evento {
                Id = 2,
                Tema = "Angular 12 e .NET 6",
                Local = "Salvador",
                Lote = "2º Lote",
                QtdPessoas = 350,
                Data = DateTime.Now.AddDays(5).ToString(),
                ImagemURL = "photo.jpeg"
                }
        };

        public EventoController()
        { }

        [HttpGet]
        public async Task<IEnumerable<Evento>> GetEvento()
        {
            return _evento;
        }

        [HttpGet("{id:int}")]
        public async Task<IEnumerable<Evento>> GetById(int id)
        {
            return _evento.Where(ev => ev.Id == id);
        }
    }
}
