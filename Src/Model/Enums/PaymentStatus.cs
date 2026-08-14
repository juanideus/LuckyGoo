namespace LUCKYGOO.Src.Model.Enums
{
    public enum PaymentStatus
    {
        /// <summary>Pago pendiente de procesar</summary>
        Pending = 1,
        
        /// <summary>Pago completado exitosamente</summary>
        Completed = 2,
        
        /// <summary>Pago falló durante el procesamiento</summary>
        Failed = 3,
        
        /// <summary>Pago cancelado por el usuario</summary>
        Cancelled = 4,
        
        /// <summary>Pago reembolsado</summary>
        Refunded = 5,
        
        /// <summary>Pago en espera (validación pendiente)</summary>
        Processing = 6
    }
}
