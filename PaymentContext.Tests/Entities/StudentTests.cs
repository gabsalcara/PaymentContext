using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("34225545806",EDocumentType.CPF);
            _address = new Address("Avenida Paulista","123","Paulista","SP","SP","Brazil","03340180");
            _email = new Email("batman@dc.com");
            _student = new Student(_name, _document, _email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("123456890",_address,_document,_email,"WAYNE CORP.",DateTime.Now,DateTime.Now.AddDays(5),10,10);
            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);            
            
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);           
            Assert.IsTrue(_student.Invalid);
        }

[       TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("123456890",_address,_document,_email,"WAYNE CORP.",DateTime.Now,DateTime.Now.AddDays(5),10,10);
            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);        
        }
    }
}