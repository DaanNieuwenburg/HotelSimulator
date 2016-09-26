using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_RuimteFactory()
        {
            // Arrange
            string  n = "Fitness";
            //Act

            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;
            
            //Assert
            Assert.AreEqual("Fitness", soort);
        }
        [TestMethod]
        public void Test_PersoneeFactory()
        {
            // Nog te doen
        }
    }
}
