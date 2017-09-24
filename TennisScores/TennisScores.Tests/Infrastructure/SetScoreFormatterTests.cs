using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TennisScores.Infrastructure;
using TennisScores.Models;

namespace TennisScores.Tests.Infrastructure
{
    [TestFixture]
    public class SetScoreFormatterTests
    {
        private SetSetScoreFormatter _sut;

        [Test]
        public async Task Format_WhenNoMatches_Expect0_0()
        {
            _sut = new SetSetScoreFormatter('A', 'B');

            var tennisMatch = new TennisMatch(new List<TennisSet>());

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("0-0");
        }

        [Test]
        public async Task Format_WhenIncompleteGame_ExpectCompletedGames_CurrentScore()
        {
            _sut = new SetSetScoreFormatter('A', 'B');

            var sets = new List<TennisSet>
            {
                new TennisSet(false, ' ', 15, 0)
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("0-0 15-0");
        }

        [Test]
        public async Task Format_WhenCompleteGame_AndA_ExpectCompletedGameOnly()
        {
            _sut = new SetSetScoreFormatter('B', 'A');

            var sets = new List<TennisSet>
            {
                new TennisSet(true, 'A', 40, 0)
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("0-1");
        }

        [Test]
        public async Task Format_WhenCompleteGame_AndBWins_ExpectCompletedGameOnly()
        {
            _sut = new SetSetScoreFormatter('B', 'A');

            var sets = new List<TennisSet>
            {
                new TennisSet(true, 'B', 40, 0)
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("1-0");
        }

        [Test]
        public async Task Format_WhenDeuce_AdvantageA_ExpectDeuce()
        {
            _sut = new SetSetScoreFormatter('A', 'B');

            var sets = new List<TennisSet>
            {
                new TennisSet(false, ' ', 40, 40, 'A')
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("0-0 A-40");
        }

        [Test]
        public async Task Format_WhenDeuce_AdvantageB_ExpectDeuce()
        {
            _sut = new SetSetScoreFormatter('A', 'B');

            var sets = new List<TennisSet>
            {
                new TennisSet(false, ' ', 40, 40, 'B')
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("0-0 40-A");
        }

        [Test]
        public async Task Format_WhenCompletedOneSet_5to5_WithCompletedSet_ExpectCompletedGameWithScores()
        {
            _sut = new SetSetScoreFormatter('A', 'B');

            var sets = new List<TennisSet>
            {
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("5-5");
        }

        [Test]
        public async Task Format_WhenCompletedOneSet_7to5_WithCompletedSet_ExpectCompletedGameWithScores()
        {
            _sut = new SetSetScoreFormatter('A', 'B');

            var sets = new List<TennisSet>
            {
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'A', 40, 0)
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("7-5 0-0");
        }

        [Test]
        public async Task Format_WhenCompletedOneSet_WithIncompletedSet_ExpectIncompletedGameWithScores()
        {
            _sut = new SetSetScoreFormatter('A', 'B');

            var sets = new List<TennisSet>
            {
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(false, ' ', 15, 0)
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("6-4 0-0 15-0");
        }

        [Test]
        public async Task Format_WhenCompletedTwoSets_WithIncompletedSet_ExpectIncompletedGameWithScores()
        {
            _sut = new SetSetScoreFormatter('B', 'A');

            var sets = new List<TennisSet>
            {
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'A', 40, 0),

                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'A', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(true, 'B', 40, 0),
                new TennisSet(false, ' ', 0, 15)
            };
            var tennisMatch = new TennisMatch(sets);

            var result = await _sut.Format(tennisMatch);

            result.Should().Be("3-6 6-4 0-0 0-15");
        }
    }
}