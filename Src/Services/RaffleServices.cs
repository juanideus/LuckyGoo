using LUCKYGOO.Src.Db;
using LUCKYGOO.Src.Services.Interfaces;
using LUCKYGOO.Src.Dtos;
using LUCKYGOO.Src.Model.Enums;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Model;
using LUCKYGOO.Src.Exceptions;
using LUCKYGOO.Src.Dtos.User;

public class RaffleServices(ContextDb contextDb) : IRaffleServices
{
    private readonly ContextDb _contextDb = contextDb;

    public async Task<string> RegisterRaffle(RaffleDto raffle, int userId)
    {

        if (raffle.WinningNumbers.Count != 5)
        {
            throw new BadRequestException("Debe seleccionar exactamente 5 números ganadores para el sorteo.");
        }
        if (raffle.IsLuckyRaffle && (raffle.NumbersOfLucky == null || raffle.NumbersOfLucky.Count != 5))
        {
            throw new BadRequestException("Debe seleccionar exactamente 5 números para el sorteo de la suerte.");
        }
        var newRaffle = new Raffle
        {
            DateOfRaffle = raffle.DateOfRaffle,

            IsLuckyRaffle = raffle.IsLuckyRaffle,

            UserId = userId
        };
        // Agregar los números ganadores
        foreach (var winningNumber in raffle.WinningNumbers)
        {
            newRaffle.Numbers.Add(new RaffleNumbers
            {
                Number = winningNumber,
                Type = RaffleNumberType.WinningNumber
            });
        }
        if (raffle.IsLuckyRaffle)
        {
            foreach (var luckyNumber in raffle.NumbersOfLucky!)
            {
                newRaffle.Numbers.Add(new RaffleNumbers
                {
                    Number = luckyNumber,
                    Type = RaffleNumberType.LuckyNumber
                });
            }
        }
        await _contextDb.Raffles.AddAsync(newRaffle);
        await _contextDb.SaveChangesAsync();

        return "Sorteo registrado correctamente";
    }
    public async Task<List<RaffleResponseDto>> GetRaffles()
    {
        //Obtenemos los sorteos que sean iguales o mayores a la fecha actual Y NO LAS QUE YA PASARON
        var raffles = await _contextDb.Raffles
            .Include(r => r.CreatedBy)
            .Select(r => new RaffleResponseDto
            {
                Id = r.Id,
                DateOfRaffle = r.DateOfRaffle,
                IsLuckyRaffle = r.IsLuckyRaffle,
                WinningNumbers = r.Numbers.Where(n => n.Type == RaffleNumberType.WinningNumber).Select(n => n.Number).ToList(),
                NumbersOfLucky = r.IsLuckyRaffle ? r.Numbers.Where(n => n.Type == RaffleNumberType.LuckyNumber).Select(n => n.Number).ToList() : null,
                User = new UserRaffleDto
                {
                    Name = r.CreatedBy.Name,
                    CreatedAt = r.CreatedAt
                }
            })
            .Where(r => r.DateOfRaffle >= DateOnly.FromDateTime(DateTime.Now))
            .ToListAsync();

        return raffles;
    }
    public Task<string> BuyRaffle(int userId, BuyRaffleDto buyRaffleDto)
    {
        //aunque siempre tengamos el id del raffle lo consultamos para validar que exista
        var raffle = _contextDb.Raffles.FirstOrDefault(r => r.Id == buyRaffleDto.RaffleId);

        if (raffle == null)
        {
            throw new NotFoundException("El sorteo no existe.");
        }
        throw new NotImplementedException("La compra de sorteos aún no está implementada.");

    }
}