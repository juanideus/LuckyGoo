
namespace LUCKYGOO.Src.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public int RaffleId { get; set; }
        public Raffle Raffle { get; set; } = null!;
        //el ticket tiene asociado un pago

        public Payment Payment { get; set; } = null!;
        public ICollection<TicketNumbers> Numbers { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}