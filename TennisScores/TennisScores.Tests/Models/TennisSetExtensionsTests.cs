using FluentAssertions;
using NUnit.Framework;
using TennisScores.Models;

namespace TennisScores.Tests.Models
{
    [TestFixture]
    public class TennisSetExtensionsTests
    {
        [Test]
        public void Convert_0_to_0()
        {
            var score = 0;
            score.ScoreToTennisScore().Should().Be(0);
        }

        [Test]
        public void Convert_1_to_15()
        {
            var score = 1;
            score.ScoreToTennisScore().Should().Be(15);
        }

        [Test]
        public void Convert_2_to_30()
        {

            var score = 2;
            score.ScoreToTennisScore().Should().Be(30);
        }

        [Test]
        public void Convert_30_to_40()
        {
            var score = 3;
            score.ScoreToTennisScore().Should().Be(40);
        }

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void Convert_AnythingOver3_To_40(int score)
        {
            score.ScoreToTennisScore().Should().Be(40);
        }
    }
}