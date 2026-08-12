using System.ComponentModel.DataAnnotations;
using LUCKYGOO.Src.Dtos.User;
using LUCKYGOO.Src.Model;
namespace LUCKYGOO.Src.Dtos
{

    public class RaffleDto
    {
        [Required(ErrorMessage = "Debe ingresar la fecha del sorteo")]
        public DateOnly DateOfRaffle { get; set; }
        [Required(ErrorMessage = "Debe indicar si el sorteo es de la suerte o no")]

        public required bool IsLuckyRaffle { get; set; }
        [Required(ErrorMessage = "Debe seleccionar los números ganadores del sorteo")]
        public required List<int> WinningNumbers { get; set; }


        public List<int>? NumbersOfLucky { get; set; }

    }
    public class RaffleResponseDto
    {
        public int Id { get; set; }
        public DateOnly DateOfRaffle { get; set; }
        public int QuantityOfTickets { get; set; }
        public int SubTotal { get; set; }
        public int SubTotalWhitLucky { get; set; }
        public int Total { get; set; }
        public required bool IsLuckyRaffle { get; set; }
        public required List<int> WinningNumbers { get; set; }
        public List<int>? NumbersOfLucky { get; set; }
        public required UserRaffleDto? User { get; set; }
    }
    public class BuyRaffleDto
    {
        [Required(ErrorMessage = "Debe ingresar el ID del sorteo")]
        public int RaffleId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar los números del sorteo")]
        public required List<int> SelectedNumbers { get; set; }
    }
}