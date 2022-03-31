using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaTurnoApi.Models
{
    public class Turn
    {
        [Key]
        public int TurnId { get; set; }
    
        [ForeignKey("QueueId")]
        public int QueueId { get; set; }
        public Queue Queue { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string HourAttention { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Status { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string PersonId { get;set; }

        [Column(TypeName ="varchar(300)")]
        public string PersonName { get; set; }
    }
}
