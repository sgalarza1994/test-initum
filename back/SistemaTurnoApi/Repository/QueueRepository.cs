using Microsoft.EntityFrameworkCore;
using SistemaTurnoApi.Models;
using SistemaTurnoApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SistemaTurnoApi.Repository
{
    public interface IQueueRepository
    {
        Task<List<Queue>> GetQueue();
        Task<Response<List<QueueResponse>>> GetQueueAsync();
    }
    public class QueueRepository : IQueueRepository
    {
        #region Builder
        public QueueRepository(TurnQueueDb db)
        {
            Db = db;
        }

        public TurnQueueDb Db { get; }


        #endregion

        #region Interfaz

        public async Task<List<Queue>> GetQueue()
        {
            return await Db.Queues.ToListAsync();
        }

        public async Task<Response<List<QueueResponse>>> GetQueueAsync()
        {
            try
            {
                var rsp = await Db.Queues.Include(t => t.Turns)
                          .Select(t => new QueueResponse
                          {
                              QueueId = t.QueueId,
                              QueueName = t.QueueName,
                              Items = t.Turns.ToList().Select(t => new TurnResponse
                              {
                                  PersonId = t.PersonId,
                                  HourAttention = t.HourAttention,
                                  PersonName = t.PersonName,
                                  RegistrationDate = t.RegistrationDate.ToString("yyyy-MM-dd"),
                                  TurnId = t.TurnId,
                                  Status = t.Status
                              }).Where(t=>t.Status.Equals("P")).ToList()
                       

                          }).ToListAsync();


                return new Response<List<QueueResponse>> { Success=true,Result=rsp };

            }
            catch (Exception e)
            {

                return new Response<List<QueueResponse>> { Success = false, Message = e.Message };
            }
        }

        #endregion
    }
}
