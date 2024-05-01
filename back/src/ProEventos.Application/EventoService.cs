using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if(await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int id, Evento model)
        {
           try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(id, false);
                if(evento == null)
                    return null;
                
                evento.Update(model);

                _geralPersist.Update<Evento>(evento);
                
                if(await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> DeleteEvento(int id)
        {            
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(id, false);
                if(evento == null)
                    throw new Exception("Não foi possível encontrar o evento com o id informado");
                
                _geralPersist.Delete<Evento>(evento);
                
                return await _geralPersist.SaveChangesAsync();                
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Evento>> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                if(!includePalestrantes)
                {
                    var eventos = await _eventoPersist.GetAllEventosAsync(false);
                    if(eventos == null)
                        return null;
                    return eventos;
                }
                var eventosComPalestrantes = await _eventoPersist.GetAllEventosAsync(true);
                if(eventosComPalestrantes == null) 
                    return null;
                return eventosComPalestrantes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                if(!includePalestrantes)
                {
                    var eventosComPalestrantes = await _eventoPersist.GetAllEventosByTemaAsync(tema, false);
                    if(eventosComPalestrantes == null) return null;
                    return eventosComPalestrantes;
                }
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null) return null;
                return eventos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            try
            {
                if(!includePalestrantes)
                {
                    var evento = await _eventoPersist.GetEventoByIdAsync(id, false);
                    if(evento == null)
                        return null;
                    return evento;
                }
                
                var eventoComPalestrante = await _eventoPersist.GetEventoByIdAsync(id, true);
                if(eventoComPalestrante == null)
                    return null;
                return eventoComPalestrante;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}