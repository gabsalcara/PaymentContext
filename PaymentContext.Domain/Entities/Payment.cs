using System;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment
    {
        public Payment(string address, string document, string email, string payer, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid)
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
        public string Address { get; private set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Payer { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
    }

}