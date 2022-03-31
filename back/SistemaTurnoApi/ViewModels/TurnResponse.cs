using System.Collections.Generic;

namespace SistemaTurnoApi.ViewModels
{

    public class QueueResponse
    {
        public int QueueId { get; set; }    

        public string QueueName { get; set; }   

        public List<TurnResponse> Items { get; set; }
    }
    public class TurnResponse
    {
      
        public int TurnId { get; set; }
        public string RegistrationDate { get; set; } 
        public string HourAttention { get; set; } 
        public string Status { get; set; }
        public string PersonId { get; set; }
        public string PersonName { get; set; }
    }
}
