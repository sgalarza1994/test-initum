using Microsoft.EntityFrameworkCore;
using SistemaTurnoApi.Models;
using SistemaTurnoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaTurnoApi.Repository
{
    public interface ITurnoRepository
    {
        Task<Response> AddTurnoQueue(TurnoAdd add);
        Task<Response> TurnoAttention(TurnResponse request);
    }
    public class TurnoRepository : ITurnoRepository
    {
        #region Builder
        public TurnoRepository(TurnQueueDb db,IQueueRepository queueRepository)
        {
            Db = db;
            QueueRepository = queueRepository;
        }

        public TurnQueueDb Db { get; }
        public IQueueRepository QueueRepository { get; }

        #endregion


        #region Interfaz

        public async Task<Response> AddTurnoQueue(TurnoAdd add)
        {
            try
            {
                List<TurnTemporal> temporals = new List<TurnTemporal>();
                var repsonse = await QueueRepository.GetQueue();
                foreach (var item in repsonse)
                {
                    var items = await Db.Turns.Where(t => t.QueueId == item.QueueId && t.Status.Equals("P")).ToListAsync();
                    int countRegister = 0;
                    int timeTotal = 0;
                    if (items.Count > 0)
                    {
                         countRegister = items.Count;
                          timeTotal = countRegister * item.TimeAttention;
                      
                    }
                    else
                    {
                        countRegister = 0;
                        timeTotal = 0;
                    }
                    temporals.Add(new TurnTemporal()
                    {
                        QueueId = item.QueueId,
                        TimeAttention = timeTotal,
                        Duration = item.TimeAttention
                    });

                }
                //Luego hacemos la comparacion entre las columnas que tiene mas items
                temporals = temporals.OrderBy(t => t.TimeAttention).ToList();
                var queueS = temporals.FirstOrDefault();


                var hours = DateTime.Now.AddMinutes(queueS.Duration);
                await Db.Turns.AddAsync(new Turn
                {
                    HourAttention = hours.ToLongTimeString(),
                    PersonId = add.Id,
                    PersonName = add.Nombre,
                    QueueId = queueS.QueueId,
                    RegistrationDate = DateTime.Now,
                    Status = "P"
                   
                }) ;
                await Db.SaveChangesAsync();
                return new Response { Success = true, Message = "Proceso exitoso" };


            }
            catch (Exception e)
            {

                return new Response { Success=false,Message=e.Message};
            }
        }

        public async Task<Response> TurnoAttention(TurnResponse request)
        {
            try
            {
                var rsp = await Db.Turns.Where(t=>t.TurnId == request.TurnId).FirstOrDefaultAsync();
                if (rsp == null)
                    return new Response { Success = false, Message = "Error en la obtencion del turno" };

                rsp.Status = "A";
                await Db.SaveChangesAsync();
                return new Response { Success = true, Message = "Proceso exitoso" };

            }
            catch (Exception e)
            {

                return new Response { Success = false, Message = e.Message };
            }
        }
        #endregion
    }
}
