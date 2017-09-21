using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using TennisScores.Infrastructure.IO;

namespace TennisScores.Tests.Infrastructure.IO
{
    [TestFixture]
    public class ScoreReaderTests
    {
        private IStreamReader _mockStreamReader;

        private ScoreReader _sut;

        [SetUp]
        public void Setup()
        {
            _mockStreamReader = Substitute.For<IStreamReader>();

            _sut = new ScoreReader(_mockStreamReader);
        }

        [Test]
        public async Task ReadFile_WhenStreamReaderThrows_ShouldReturnEmptyGames()
        {
            var path = "path";

            _mockStreamReader.ReadLineAsync(path)
                .Throws<Exception>();

            var result = await _sut.ReadFile(path);

            result.Count.Should().Be(0);
        }

        [Test]
        public async Task ReadFile_WhenReadingAFile_ExpectAllGames()
        {
            var path = "path";

            _mockStreamReader.ReadLineAsync(path)
                .Returns(new List<string>
                {
                    "AABBAA",
                    "DDCCDDDD",
                    "EEFFFFFEE"
                });

            var games = await _sut.ReadFile(path);

            games.Count.Should().Be(3);
        }

        [Test]
        public async Task ReadFile_WhenReadingAFile_ExpectGamesMatch()
        {
            var path = "path";

            var input = new List<string>
            {
                "AABBAA",
                "DDCCDDDD",
                "EEFFFFFEE"
            };

            _mockStreamReader.ReadLineAsync(path)
                .Returns(input);

            var games = await _sut.ReadFile(path);

            var game1Chars = string.Join("", games[0].Points.Select(p => p));
            var game2Chars = string.Join("", games[1].Points.Select(p => p));
            var game3Chars = string.Join("", games[2].Points.Select(p => p));

            game1Chars.Should().BeEquivalentTo(input[0]);
            game2Chars.Should().BeEquivalentTo(input[1]);
            game3Chars.Should().BeEquivalentTo(input[2]);
        }
    }
}
