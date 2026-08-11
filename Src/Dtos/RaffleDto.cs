using System.ComponentModel.DataAnnotations;

namespace LUCKYGOO.Src.Dtos
{

    public class RaffleDto
    {
        [Required(ErrorMessage = "Debe ingresar la fecha del sorteo")]
        public DateOnly DateOfRaffle { get; set; }

        public required bool IsLuckyRaffle { get; set; }
        [Required(ErrorMessage = "Debe seleccionar los números ganadores del sorteo")]
        public required List<int> WinningNumbers { get; set; }
    
        public List<int>? NumbersOfLucky { get; set; }

    }
    public class RaffleResponseDto
    {
        public int Id { get; set; }
        public DateOnly DateOfRaffle { get; set; }
        public required bool IsLuckyRaffle { get; set; }
        public required List<int> WinningNumbers { get; set; }
        public List<int>? NumbersOfLucky { get; set; }
    }
}