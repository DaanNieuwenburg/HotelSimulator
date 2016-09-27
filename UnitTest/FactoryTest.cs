using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace UnitTest
{
    [TestClass]
    public class FactoryTest
    {
        [TestMethod]
        public void Test_RuimteFactory_Return_Fitness()
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
        public void Test_RuimteFactory_Return_Lift()
        {
            // Arrange
            string n = "Trap";
            //Act

            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Lift", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Kamer()
        {
            // Arrange
            string n = "Lobby";
            
            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Lobby", soort);
        }
    }
}
