using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTurnoApi.Models
{
    public class Queue
    {
        [Key]
        public int QueueId { get; set; } 
        [Column(TypeName ="varchar(300)")]
        public string QueueName { get; set; }   

        public int TimeAttention { get; set; }   
        [JsonIgnore]
        public virtual List<Turn> Turns { get; set; }
    }
}
