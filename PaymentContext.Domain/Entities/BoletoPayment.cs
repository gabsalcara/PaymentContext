using System;
using PaymentContext.Domain.ValueObjects;
namespace PaymentContext.Domain.Entities
{
    
    public class BoletoPayment : Payment
    {
        public BoletoPayment(
            string barcode, 
            string boletoNumber,
            Address address,
            Document document,
            Email email,
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