using HotelSimulatie.Model;
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
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Fitness));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Lift()
        {
            // Arrange
            string n = "Liftschacht";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Liftschacht));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Lobby()
        {
            // Arrange
            string n = "Lobby";
            
            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Lobby));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Eetzaal()
        {
            // Arrange
            string n = "Eetzaal";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);


            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Eetzaal));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Bioscoop()
        {
            // Arrange
            string n = "Bioscoop";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Bioscoop));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Trappenhuis()
        {
            // Arrange
            string n = "Trappenhuis";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Trappenhuis));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Trap()
        {
            // Arrange
            string n = "Trap";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Trap));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Kamer()
        {
            // Arrange
            string n = "Kamer";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Kamer));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Gang()
        {
            // Arrange
            string n = "Gang";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Gang));
        }

        [TestMethod]
        public void Test_RuimteFactory_Return_Zwembad()
        {
            // Arrange
            string n = "Zwembad";

            //Act
            HotelRuimteFactory factory = new HotelRuimteFactory();
            HotelRuimte ruimte = factory.MaakHotelRuimte(n);

            //Assert
            Assert.IsInstanceOfType(ruimte, typeof(Zwembad));
        }
    }
}
