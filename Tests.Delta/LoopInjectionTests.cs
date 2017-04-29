using System;
using NUnit.Framework;
using Omu.ValueInjecter.Delta;
using Tests.SampleTypes;

namespace Tests.Delta
{
    [TestFixture]
    public class LoopInjectionTests
    {
        [Test]
        public void ShouldIgnoreProperties()
        {
            var customer = GetCustomer();
            var c1 = new Customer();
            c1.InjectFrom(new Omu.ValueInjecter.Delta.Injections.LoopInjection(new[] { "FirstName" }), customer);
            
            Assert.IsNull(c1.FirstName);
            Assert.AreEqual(customer.LastName, c1.LastName);
        }

        private static Customer GetCustomer()
        {
            var customer = new Customer { FirstName = "Art", LastName = "Vandelay", Id = 123, RegDate = DateTime.UtcNow };
            return customer;
        }
    }
}