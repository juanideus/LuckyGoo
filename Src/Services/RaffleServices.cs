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
                QuantityOfTickets = r.QuantityOfTickets,
                SubTotal = r.SubTotal,
                SubTotalWhitLucky = r.SubTotalWhitLucky,
                Total = r.Total,
                IsLuckyRaffle = r.IsLuckyRaffle,
               
                User = new UserRaffleDto
                {
                    Name = r.CreatedBy.Name,
                    CreatedAt = r.CreatedAt
                }
            })

            .ToListAsync();

        return raffles;
    }
    public async Task<string> BuyRaffle(BuyRaffleDto buyRaffleDto)
    {
        if (buyRaffleDto.SelectedNumbers.Count != 5)
        {
            throw new BadRequestException("Debe seleccionar exactamente 5 números para el sorteo.");
        }
        //aunque siempre tengamos el id del raffle lo consultamos para validar que exista
        var raffle = _contextDb.Raffles.FirstOrDefault(r => r.Id == buyRaffleDto.RaffleId) ?? throw new NotFoundException("El sorteo no existe.");

        
        // Validamos que el sorteo no haya pasado
        if (raffle.DateOfRaffle < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new BadRequestException("El sorteo ya ha pasado.");
        }
        if (raffle.QuantityOfTickets <= 0)
        {
            throw new BadRequestException("No hay boletos disponibles para este sorteo.");
        }
        

        //creamos el ticket
        var ticket = new Ticket
        {
            Code = Guid.NewGuid().ToString(), // Generamos un código único para el ticket
            RaffleId = raffle.Id,
            Raffle = raffle,
            Payment = new Payment
            {
                PaymentMethod = PaymentMethod.CreditCard, // Por ejemplo, se puede cambiar según la lógica de pago
                PaymentStatus = PaymentStatus.Pending,
                Amount = 10.00m, // Por ejemplo, se puede calcular según la lógica de precio del sorteo
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            CreatedAt = DateTime.UtcNow
        };
        //devolvemos el codigo del ticket para que el usuario pueda ver su ticket comprado
        return ticket.Code;
        

    }
    public async Task<RaffleInCourseDto> GetRaffleInCourse()
    {
        //obtenemso el sorteo para este domingo o el siguiente que este disponible
        var raffleInCourse = await _contextDb.Raffles
            .Where(r => r.DateOfRaffle >= DateOnly.FromDateTime(DateTime.Now))
            .OrderBy(r => r.DateOfRaffle)
            .Select(r => new RaffleInCourseDto
            {
                Id = r.Id,
                DateOfRaffle = r.DateOfRaffle,
                IsLuckyRaffle = r.IsLuckyRaffle
            })
            .FirstOrDefaultAsync();

        if (raffleInCourse == null)
        {
            throw new NotFoundException("No hay sorteos en curso.");
        }

        return raffleInCourse;
    }
}