using System;
using Flunt.Notifications;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Commands;
using PaymentContext.Shared.Commands;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Services;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
        Notifiable, 
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>, 
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }
            
            // Verificar se o documento já está cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document","Este CPF já está em uso");
            }

            // Verificar se o e-mail já está cadastrado
            if(_repository.EmailExists(command.Email))
            {
                AddNotification("Email","Este e-mail já está em uso");
            }
            
            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document,EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number,command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber,
                address,
                new Document(command.PayerDocument, command.PayerDocumentType),
                email,
                command.Payer,
                command.PaidDate,
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid 
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem-vindo ao balto.io", "Sua Assinatura foi criada");
            
            // Retornar informações
             return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            // Verificar se o documento já está cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document","Este CPF já está em uso");
            }

            // Verificar se o e-mail já está cadastrado
            if(_repository.EmailExists(command.Email))
            {
                AddNotification("Email","Este e-mail já está em uso");
            }
            
            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document,EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number,command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName, 
                command.CardNumber,
                command.LastTransactionNumber,
                command.TransactionCode,
                address,
                new Document(command.PayerDocument, command.PayerDocumentType),
                email,
                command.Payer,
                command.PaidDate,
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid 
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem-vindo ao balto.io", "Sua Assinatura foi criada");
            
            // Retornar informações
                return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    
        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Verificar se o documento já está cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document","Este CPF já está em uso");
            }

            // Verificar se o e-mail já está cadastrado
            if(_repository.EmailExists(command.Email))
            {
                AddNotification("Email","Este e-mail já está em uso");
            }
            
            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document,EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number,command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode, 
                address,
                new Document(command.PayerDocument, command.PayerDocumentType),
                email,
                command.Payer,
                command.PaidDate,
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid 
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(),student.Email.Address,"Bem-vindo ao balto.io", "Sua Assinatura foi criada");
            
            // Retornar informações
                return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}