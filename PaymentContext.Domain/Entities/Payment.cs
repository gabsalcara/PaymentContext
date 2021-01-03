using System;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using Flunt.Validations;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
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

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, Total,"Payment.Total","O total não pode ser zero")
                .IsGreaterOrEqualsThan(Total, TotalPaid,"Payment.TotalPaid","O valor pago é menor que o valor do pagamento")
            );
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