using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment
    {
        public Payment(Address address, Document document, Email email, string payer, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid)
        {
            Number = Guid.NewGuid().ToString().Replace("-","").Substring(0,10).ToUpper();
            Address = address;
            Document = document;
            Email = email;
            Payer = payer;
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
        }

        public string Number { get; private set; }
        public Address Address { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Payer { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
    }

}