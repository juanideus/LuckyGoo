using LUCKYGOO.Src.Dtos;
namespace LUCKYGOO.Src.Services.Interfaces
{

    public interface IRaffleServices
    {
        public Task<string> RegisterRaffle(RaffleDto raffle, int userId);
    }
}