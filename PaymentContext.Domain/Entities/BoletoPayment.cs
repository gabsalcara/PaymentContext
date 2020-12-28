using System;

namespace PaymentContext.Domain.Entities
{
    
    public class BoletoPayment : Payment
    {
        public BoletoPayment(
            string barcode, 
            string boletoNumber,
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
            Barcode = barcode;
            BoletoNumber = boletoNumber;
        }

        public string Barcode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}