using Microsoft.EntityFrameworkCore;

namespace SistemaTurnoApi.Models
{
    public class TurnQueueDb : DbContext
    {

        public DbSet<Turn> Turns { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public TurnQueueDb(DbContextOptions<TurnQueueDb> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Queue>().HasData(
               new Queue { QueueId = 1 , QueueName= "Cola 1" ,TimeAttention =2 },
               new Queue {QueueId = 2, QueueName = "Cola 2", TimeAttention=3 }
           );

            base.OnModelCreating(modelBuilder);
        }

       
    }
}
