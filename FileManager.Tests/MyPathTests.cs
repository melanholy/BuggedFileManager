using FileManager.Domain.Infrastructure;
using NUnit.Framework;

namespace FileManager.Tests
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

        [Test]
        public void TestJoinUnix()
        {
            var p1 = new MyPath("/test");
            var p2 = p1.Join("is");
            var p3 = p2.Join("over");
            Assert.AreEqual("/test", p1.PathStr);
            Assert.AreEqual("/test/is", p2.PathStr);
            Assert.AreEqual("/test/is/over", p3.PathStr);
        }

        [Test]
        public void TestJoinWin()
        {
            var p1 = new MyPath("C:/test");
            var p2 = p1.Join("is");
            var p3 = p2.Join("over");
            Assert.AreEqual("C:/test", p1.PathStr);
            Assert.AreEqual("C:/test/is", p2.PathStr);
            Assert.AreEqual("C:/test/is/over", p3.PathStr);
        }

        [Test]
        public void TestGetExt()
        {
            var p = new MyPath("/test/file.txt");
            Assert.AreEqual("txt", p.GetExt());
            p = new MyPath("/test/folder");
            Assert.AreEqual("", p.GetExt());
            p = new MyPath("/test.is.over");
            Assert.AreEqual("over", p.GetExt());
        }

        [Test]
        public void TestGetFilename()
        {
            var p = new MyPath("/test/file.txt");
            Assert.AreEqual("file.txt", p.GetFileName());
            p = new MyPath("/test/folder");
            Assert.AreEqual("folder", p.GetFileName());
            p = new MyPath("/i/hope/test.is.over");
            Assert.AreEqual("test.is.over", p.GetFileName());
        }
    }
}
