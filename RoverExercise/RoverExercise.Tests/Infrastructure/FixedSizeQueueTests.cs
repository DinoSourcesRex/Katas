using NUnit.Framework;
using RoverExercise.Infrastructure;

namespace RoverExercise.Tests.Infrastructure
{
    public class FixedSizeQueueTests
    {
        private ICommandQueue<int> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CommandQueue<int>(5);
        }

        [Test]
        public void ExceedLimit_ExpectLimit()
        {
            _sut.Queue(1);
            _sut.Queue(2);
            _sut.Queue(3);
            _sut.Queue(4);
            _sut.Queue(5);
            _sut.Queue(6);

            Assert.AreEqual(5, _sut.Count());
            Assert.That(_sut, Has.Exactly(1).Matches<int>(x => x == 1));
            Assert.That(_sut, Has.Exactly(1).Matches<int>(x => x == 2));
            Assert.That(_sut, Has.Exactly(1).Matches<int>(x => x == 3));
            Assert.That(_sut, Has.Exactly(1).Matches<int>(x => x == 4));
            Assert.That(_sut, Has.Exactly(1).Matches<int>(x => x == 5));
            Assert.That(_sut, Has.Exactly(0).Matches<int>(x => x == 6));
        }
    }
}