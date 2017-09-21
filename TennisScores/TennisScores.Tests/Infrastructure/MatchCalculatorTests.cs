using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TennisScores.Infrastructure;
using TennisScores.Models;

namespace TennisScores.Tests.Infrastructure
{
    [TestFixture]
    public class MatchCalculatorTests
    {
        private ISetCalculator _mockSetCalculator;

        private MatchCalculator _sut;

        [SetUp]
        public void Setup()
        {
            _mockSetCalculator = Substitute.For<ISetCalculator>();

            _sut = new MatchCalculator(_mockSetCalculator);
        }

        [Test]
        public async Task Calculate_WhenSingleGame_ExpectResultReturned()
        {
            var fixture = new Fixture();

            var tennisGame = fixture.Create<TennisGame>();

            var setResult = new SetResult(fixture.Create<TennisSet>(), 
                new TennisGame(new List<char>()));

            _mockSetCalculator.Calculate(tennisGame).Returns(setResult);

            var tennisMatch = await _sut.Calculate(tennisGame);

            tennisMatch.Sets.Should().BeEquivalentTo(setResult.Set);
        }

        [Test]
        public async Task Calculate_WhenSingleGame_ExpectCalculatorCalledOnce()
        {
            var fixture = new Fixture();

            var tennisGame = fixture.Create<TennisGame>();

            var setResult = new SetResult(fixture.Create<TennisSet>(),
                new TennisGame(new List<char>()));

            _mockSetCalculator.Calculate(tennisGame).Returns(setResult);

            await _sut.Calculate(tennisGame);

            await _mockSetCalculator
                .Received(1)
                .Calculate(tennisGame);
        }

        [Test]
        public async Task Calculate_WhenThreeGames_ExpectResultReturned()
        {
            var fixture = new Fixture();

            var tennisGame = fixture.Create<TennisGame>();

            var result01 = new SetResult(fixture.Create<TennisSet>(),
                new TennisGame(new List<char>
                {
                    'a', 'b', 'a'
                }));

            var result02 = new SetResult(fixture.Create<TennisSet>(),
                new TennisGame(new List<char>
                {
                    'd', 'd', 'e'
                }));

            var result03 = new SetResult(fixture.Create<TennisSet>(),
                new TennisGame(new List<char>()));

            _mockSetCalculator.Calculate(Arg.Any<TennisGame>())
                .Returns(result01, result02, result03);

            var tennisMatch = await _sut.Calculate(tennisGame);

            var expectedResult = new List<TennisSet> {result01.Set, result02.Set, result03.Set};

            tennisMatch.Sets.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task Calculate_WhenThreeGames_ExpectCalculatorCalledThreeTimes()
        {
            var fixture = new Fixture();

            var tennisGame = fixture.Create<TennisGame>();

            var result01 = new SetResult(fixture.Create<TennisSet>(),
                new TennisGame(new List<char>
                {
                    'a', 'b', 'a'
                }));

            var result02 = new SetResult(fixture.Create<TennisSet>(),
                new TennisGame(new List<char>
                {
                    'd', 'd', 'e'
                }));

            var result03 = new SetResult(fixture.Create<TennisSet>(),
                new TennisGame(new List<char>()));

            _mockSetCalculator.Calculate(Arg.Any<TennisGame>())
                .Returns(result01, result02, result03);

            await _sut.Calculate(tennisGame);

            await _mockSetCalculator
                .Received(3)
                .Calculate(Arg.Any<TennisGame>());
        }
    }
}
