using LUCKYGOO.Src.Model.Enums;

namespace LUCKYGOO.Src.Model
{
    /// <summary>
    /// Modelo que representa un pago asociado a un ticket de rifa.
    /// Mantiene la trazabilidad completa del ciclo de vida del pago.
    /// Los pagos pueden realizarse de forma anónima sin cuenta de usuario.
    /// </summary>
    public class Payment
    {
        /// <summary>Identificador único del pago</summary>
        public int Id { get; set; }

        /// <summary>ID del ticket asociado al pago</summary>
        public int TicketId { get; set; }

        /// <summary>Ticket asociado a este pago</summary>
        public Ticket Ticket { get; set; } = null!;

        /// <summary>Método de pago utilizado (crédito, débito, transferencia, etc)</summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>Estado actual del pago (pendiente, completado, fallido, etc)</summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>Monto del pago en la moneda principal. Debe ser mayor a 0</summary>
        public decimal Amount { get; set; }

        /// <summary>ID único de la transacción para auditoría y seguimiento</summary>
        public string? TransactionId { get; set; }

        /// <summary>Referencia del procesador de pagos (gateway response ID)</summary>
        public string? GatewayReference { get; set; }

        /// <summary>Fecha y hora de creación del registro de pago</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Fecha y hora de la última actualización del estado</summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Fecha y hora en que expira la validez del pago (ej: para ofertas limitadas)</summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>Fecha y hora en que se completó el pago, null si aún no se completa</summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>Mensaje de error en caso de que el pago haya fallado</summary>
        public string? ErrorMessage { get; set; }
    }
}