

using LUCKYGOO.Src.Model.Enums;
namespace LUCKYGOO.Src.Model
{
    public class TicketNumbers
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public RaffleNumberType Type { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
    }
}