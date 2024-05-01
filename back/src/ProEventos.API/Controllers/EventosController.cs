using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence;
using ProEventos.Persistence.Contracts;
using ProEventos.Domain;
using ProEventos.Application;
using ProEventos.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly IGeralPersist _geralPersist;

        public EventosController(IEventoService eventoService)
        { 
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventosAsync()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync();
                if(eventos == null)
                    return NotFound("Não foi possível completar a requisição");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id);
                if(evento==null)
                    return NotFound("Evento não encontrado.");
                return Ok(evento);                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}")]
        public async Task<IActionResult> GetByTemaAsync(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema);
                if(eventos.Count == 0)
                    return NotFound("Eventos por tema não encontrados com o parâmetro fornecido.");
                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest("Não foi possível criar o evento.");
                
                var evento = await _eventoService.AddEvento(model);
                if(evento == null)
                    return BadRequest("Erro ao criar o evento.");
                
                return Created($"/{evento.Id}", evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar o evento. Erro: {ex.Message}");
            }
        }
    }
}
