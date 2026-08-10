
using LUCKYGOO.Src.Model.Enums;
namespace LUCKYGOO.Src.Model
{
    public class RaffleNumbers
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public RaffleNumberType Type { get; set; }
        public int RaffleId { get; set; }
        public Raffle Raffle { get; set; } = null!;
    }
}