using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TennisScores.Infrastructure;
using TennisScores.Models;

namespace TennisScores.Tests.Infrastructure
{
    [TestFixture]
    public class SetCalculatorTests
    {
        private SetCalculator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SetCalculator();
        }

        [Test]
        public async Task Calculate_WhenGameHasNoData_ExpectReturnEmptyGame_GameNotComplete_NoWinner_ServerScore0_ReceiverScore0()
        {
            var result = await _sut.Calculate(new TennisGame(new List<char>()));

            result.Set.GameCompleted.Should().BeFalse();
            result.Set.Winner.Should().Be(' ');
            result.Set.ServerScore.Should().Be(0);
            result.Set.ReceiverScore.Should().Be(0);
        }

        [Test]
        public async Task Calculate_GameCompleted_ExpectRemainingPointsReturned()
        {
            var game = new TennisGame
            (
                new List<char>
                {
                    'A',
                    'A',
                    'A',
                    'A',
                    'B',
                    'B',
                    'A',
                    'B'
                }
            );

            var set = await _sut.Calculate(game);

            var expectedRemaining = new List<char>
            {
                'B',
                'B',
                'A',
                'B'
            };

            set.Remaining.Points.ShouldBeEquivalentTo(expectedRemaining, options => options.WithStrictOrdering());
        }

        [Test]
        public async Task Calculate_3PointsInARow_ExpectGameNotComplete_NoWinner_ServerScore3_ReceiverScore0()
        {
            var game = new TennisGame
            (
                new List<char>
                {
                    'A',
                    'A',
                    'A'
                }
            );

            var set = await _sut.Calculate(game);

            set.Set.GameCompleted.Should().BeFalse();
            set.Set.Winner.Should().Be(' ');
            set.Set.ServerScore.Should().Be(3);
            set.Set.ReceiverScore.Should().Be(0);
        }

        [Test]
        public async Task Calculate_4PointsInARow_ExpectGameComplete_WinnerA_ServerScore4_ReceiverScore0()
        {
            var game = new TennisGame
            (
                new List<char>
                {
                    'A',
                    'A',
                    'A',
                    'A'
                }
            );

            var set = await _sut.Calculate(game);

            set.Set.GameCompleted.Should().BeTrue();
            set.Set.Winner.Should().Be('A');
            set.Set.ServerScore.Should().Be(4);
            set.Set.ReceiverScore.Should().Be(0);
        }

        [Test]
        public async Task Calculate_2PointsForA_4PointsForB_Expect_ExpectGameComplete_WinnerB_ServerScore2_ReceiverScore4()
        {
            var game = new TennisGame
            (
                new List<char>
                {
                    'A',
                    'A',
                    'B',
                    'B',
                    'B',
                    'B'
                }
            );

            var set = await _sut.Calculate(game);

            set.Set.GameCompleted.Should().BeTrue();
            set.Set.Winner.Should().Be('B');
            set.Set.ServerScore.Should().Be(2);
            set.Set.ReceiverScore.Should().Be(4);
        }

        [Test]
        public async Task Calculate_3PointsForB_4PointsForA_3PointsForB_ExpectGameComplete_WinnerB_ServerScore6_ReceiverScore4()
        {

            var game = new TennisGame
            (
                new List<char>
                {
                    'B',
                    'B',
                    'B',
                    'A',
                    'A',
                    'A',
                    'A',
                    'B',
                    'B',
                    'B'
                }
            );

            var set = await _sut.Calculate(game);

            set.Set.GameCompleted.Should().BeTrue();
            set.Set.Winner.Should().Be('B');
            set.Set.ServerScore.Should().Be(6);
            set.Set.ReceiverScore.Should().Be(4);
        }

        [Test]
        public async Task Calculate_3PointsForA_4PointsForB_3PointsForA_ExpectGameComplete_WinnerA_ServerScore6_ReceiverScore4()
        {

            var game = new TennisGame
            (
                new List<char>
                {
                    'A',
                    'A',
                    'A',
                    'B',
                    'B',
                    'B',
                    'B',
                    'A',
                    'A',
                    'A'
                }
            );

            var set = await _sut.Calculate(game);

            set.Set.GameCompleted.Should().BeTrue();
            set.Set.Winner.Should().Be('A');
            set.Set.ServerScore.Should().Be(6);
            set.Set.ReceiverScore.Should().Be(4);
        }

        [Test]
        public async Task Calculate_Deuce_3PointsB_4PointsA_2PointsB_ExpectGameNotComplete_NoWinner_ServerScore5_ReceiverScore4()
        {

            var game = new TennisGame
            (
                new List<char>
                {
                    'B',
                    'B',
                    'B',
                    'A',
                    'A',
                    'A',
                    'A',
                    'B',
                    'B',
                }
            );

            var set = await _sut.Calculate(game);

            set.Set.GameCompleted.Should().BeFalse();
            set.Set.Winner.Should().Be(' ');
            set.Set.ServerScore.Should().Be(5);
            set.Set.ReceiverScore.Should().Be(4);
        }
    }
}