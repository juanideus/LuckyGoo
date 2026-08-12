namespace LUCKYGOO.Src.Model
{
    public class Raffle
    {
        public int Id { get; set; }
        public DateOnly DateOfRaffle { get; set; }
        public int QuantityOfTickets { get; set; } = 0;
        public int SubTotal { get; set; } = 0;
        public int SubTotalWhitLucky { get; set; } = 0;
        public int Total { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<RaffleNumbers> Numbers { get; set; } =[];
         public required bool IsLuckyRaffle { get; set; }
        
        public int UserId { get; set; }
        public User CreatedBy { get; set; } = null!;


    }
}