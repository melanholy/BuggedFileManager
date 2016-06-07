using filemanager.Infrastructure;
using NUnit.Framework;

namespace File_Manager.Tests
{
    [TestFixture]
    public class MyPathTests
    {
        [Test]
        public void TestIsCorrectPathWin()
        {
            Assert.True(MyPath.IsCorrectMyPath("C:/"));
            Assert.True(MyPath.IsCorrectMyPath("Y:/"));
            Assert.True(MyPath.IsCorrectMyPath("C:/Documnests asd Seccihgfs"));
            Assert.True(MyPath.IsCorrectMyPath("D:/Igory/"));
            Assert.True(MyPath.IsCorrectMyPath("C:"));
            Assert.True(MyPath.IsCorrectMyPath("C:///////"));
            Assert.True(MyPath.IsCorrectMyPath("C://sed/////"));

            Assert.False(MyPath.IsCorrectMyPath("C/"));
            Assert.False(MyPath.IsCorrectMyPath("C"));
            Assert.False(MyPath.IsCorrectMyPath("Root"));
            Assert.False(MyPath.IsCorrectMyPath("Disk:/"));
        }

        [Test]
        public void TestIsCorrectPathUnix()
        {
            Assert.True(MyPath.IsCorrectMyPath("/"));
            Assert.True(MyPath.IsCorrectMyPath("/root/"));
            Assert.True(MyPath.IsCorrectMyPath("/roo"));
            Assert.True(MyPath.IsCorrectMyPath("////"));
            Assert.True(MyPath.IsCorrectMyPath("/aye//karamba////"));

            Assert.False(MyPath.IsCorrectMyPath("root"));
            Assert.False(MyPath.IsCorrectMyPath("roo/"));
        }
    }
}
