using FileManager.GUI.Application;
using NUnit.Framework;

namespace FileManager.Tests
{
    [TestFixture]
    public class HistoryKeeperTests
    {
        [Test]
        public void TestUndoThrowsExcpOnEmpty()
        {
            var hk = new HistoryKeeper<int>(0);
            Assert.Throws<EmptyHistoryException>(() => hk.GoBack());
            hk.Do(4);
            hk.GoBack();
            Assert.Throws<EmptyHistoryException>(() => hk.GoBack());
        }

        [Test]
        public void TestUndoNormal()
        {
            var hk = new HistoryKeeper<int>(0);
            hk.Do(4);
            Assert.AreEqual(0, hk.GoBack());

            hk.Do(1);
            hk.Do(2);
            hk.Do(3);
            Assert.AreEqual(2, hk.GoBack());
            Assert.AreEqual(1, hk.GoBack());
            Assert.AreEqual(0, hk.GoBack());
        }

        [Test]
        public void TestRedoThrowsExcpOnEmpty()
        {
            var hk = new HistoryKeeper<int>(0);
            Assert.Throws<EmptyHistoryException>(() => hk.GoForward());
            hk.Do(4);
            Assert.Throws<EmptyHistoryException>(() => hk.GoForward());
            hk.GoBack();
            hk.GoForward();
            Assert.Throws<EmptyHistoryException>(() => hk.GoForward());
            hk.Do(1);
            hk.Do(2);
            hk.Do(3);
            hk.GoBack();
            hk.GoBack();
            Assert.AreEqual(2, hk.GoForward());
            Assert.AreEqual(3, hk.GoForward());
            hk.GoBack();
            Assert.AreEqual(3, hk.GoForward());
        }

        [Test]
        public void TestRedoNormal()
        {
            var hk = new HistoryKeeper<int>(0);
            hk.Do(4);
            hk.GoBack();
            Assert.AreEqual(4, hk.GoForward());
            hk.Do(1);
            hk.Do(2);
            hk.Do(3);
            hk.GoBack();
            hk.GoBack();
            Assert.AreEqual(2, hk.GoForward());
            Assert.AreEqual(3, hk.GoForward());
            hk.GoBack();
            Assert.AreEqual(3, hk.GoForward());
        }

        [Test]
        public void TestRedoClearsOnDo()
        {
            var hk = new HistoryKeeper<int>(0);
            hk.Do(4);
            hk.Do(4);
            hk.GoBack();
            hk.Do(5);
            Assert.Throws<EmptyHistoryException>(() => hk.GoForward());
        }
    }
}