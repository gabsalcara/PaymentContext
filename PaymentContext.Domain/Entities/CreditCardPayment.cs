using System;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(
            string cardHolderName, 
            string cardNumber, 
            string lastTransactionNumber,
            string transactionCode,
            string address,
            string document,
            string email,
            string payer,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid) : base(
                address,
                document,
                email,
                payer,
                paidDate,
                expireDate,
                total,
                totalPaid)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}