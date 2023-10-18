using WednesdayTalkLabs;

namespace WednesdayTalkTests
{
    [TestClass]
    public class TryConvertDecimalTests
    {
        [TestMethod]
        public void Should_Allow_NullTest()
        {
            //arrange
            string number = null;


            //act
            var result = Program.TryConvertDecimal(number, 1, "A", 10, true);

            //assert
            Assert.AreEqual(0, result);


        }

        [TestMethod]
        public void Shouldnt_Allow_Null_Test()
        {
            //arrange
            string number = null;


            //act
            var result = Program.TryConvertDecimal(number, 1, "A", 10, false);

            //assert
            Assert.AreEqual(0, result);


        }





        [TestMethod]
        public void Shouldnt_Allow_Broibinha_Test()
        {
            //arrange
            string number = "abobrinha";


            //act
            var result = Program.TryConvertDecimal(number, 1, "A", 10, true);

            //assert
            Assert.AreEqual(0, result);


        }




        [TestMethod]
        public void Should_Convert_Number()
        {
            //arrange
            string number = "123,45";


            //act
            var result = Program.TryConvertDecimal(number, 1, "A", 10, true);

            //assert
            Assert.AreEqual(123.45m, result);


        }
    }
}