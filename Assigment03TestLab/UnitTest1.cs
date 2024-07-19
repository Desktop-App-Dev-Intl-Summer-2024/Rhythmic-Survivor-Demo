using Lab02.ShippingCalculation;

namespace Assigment03TestLab
{
    public class Tests
    {
        ShippingCharges shipping { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            shipping = new ShippingCharges();
        }

        [Test]
        public void TestShipping0Miles()
        {
            shipping.weight = 1.7;
            shipping.distance = 0;

            var result = shipping.calculateShippingCost();

            Assert.That(result, Is.TypeOf<double>());
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void TestShipping634Miles()
        {
            shipping.weight = 13.5;
            shipping.distance = 634;

            var result = shipping.calculateShippingCost();

            Assert.That(result, Is.TypeOf<double>());
            Assert.AreEqual(4.80, result);
        }

        [Test]
        public void TestShipping1284Miles()
        {
            shipping.weight = 6.2;
            shipping.distance = 1284;

            var result = shipping.calculateShippingCost();

            Assert.That(result, Is.TypeOf<double>());
            Assert.AreEqual(7.40, result);
        }
    }
}