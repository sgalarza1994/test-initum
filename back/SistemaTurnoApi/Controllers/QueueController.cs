using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaTurnoApi.Models;
using SistemaTurnoApi.Repository;
using SistemaTurnoApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaTurnoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        public QueueController(ITurnoRepository turnoRepository,IQueueRepository queueRepository)
        {
            TurnoRepository = turnoRepository;
            QueueRepository = queueRepository;
        }

        public ITurnoRepository TurnoRepository { get; }
        public IQueueRepository QueueRepository { get; }

        #region Turno
        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> AddTurno(TurnoAdd turnoAdd)
        {
            var response = turnoAdd.Valid();
            if (!response.Success)
                return Ok(response);

            return Ok(await TurnoRepository.AddTurnoQueue(turnoAdd));

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> TurnoAttention(TurnResponse turnoAdd)
        {

            return Ok(await TurnoRepository.TurnoAttention(turnoAdd));

        }
        #endregion
        #region Cola
        //Obtenemos todas las cosas 
        [HttpGet("[action]")]
        public async Task<ActionResult<Response<List<Queue>>>> GetQueueAll()
        {
            var rsp = await QueueRepository.GetQueue();

            var response = new Response<List<Queue>> { Success = true, Result = rsp };

            return Ok(response);
        }
        //Obtener los detalles de esas cosas 
        [HttpGet("[action]")]
        public async Task<ActionResult<Response<List<QueueResponse>>>> GetQueueDetail()
        {


            return Ok(await QueueRepository.GetQueueAsync());
        }



        #endregion

    }
}
