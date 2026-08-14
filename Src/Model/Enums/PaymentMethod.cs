namespace LUCKYGOO.Src.Model.Enums
{
    public enum PaymentMethod
    {
        /// <summary>Tarjeta de crédito</summary>
        CreditCard = 1,
        
        /// <summary>Tarjeta de débito</summary>
        DebitCard = 2,
        
        /// <summary>Transferencia bancaria</summary>
        BankTransfer = 3,
        
        /// <summary>Billetera digital (PayPal, etc)</summary>
        DigitalWallet = 4,
        
        /// <summary>Criptomoneda</summary>
        Cryptocurrency = 5,
        
        /// <summary>Efectivo (solo presencial)</summary>
        Cash = 6
    }
}
