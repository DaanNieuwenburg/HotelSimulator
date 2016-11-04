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
            string n = "Liftschacht";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Lift", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Lobby()
        {
            // Arrange
            string n = "Lobby";
            
            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Lobby", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Eetzaal()
        {
            // Arrange
            string n = "Eetzaal";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Eetzaal", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Bioscoop()
        {
            // Arrange
            string n = "Bioscoop";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Bioscoop", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Trappenhuis()
        {
            // Arrange
            string n = "Trappenhuis";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Trappenhuis", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Trap()
        {
            // Arrange
            string n = "Trap";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Trap", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Kamer()
        {
            // Arrange
            string n = "Kamer";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Kamer", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Gang()
        {
            // Arrange
            string n = "Gang";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Gang", soort);
        }
        [TestMethod]
        public void Test_RuimteFactory_Return_Zwembad()
        {
            // Arrange
            string n = "Zwembad";

            //Act
            HotelSimulatie.Model.HotelRuimteFactory factory = new HotelSimulatie.Model.HotelRuimteFactory();
            string soort = factory.MaakHotelRuimte(n).Naam;

            //Assert
            Assert.AreEqual("Zwembad", soort);
        }
        
    }
}
