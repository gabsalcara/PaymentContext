using System;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(
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
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}