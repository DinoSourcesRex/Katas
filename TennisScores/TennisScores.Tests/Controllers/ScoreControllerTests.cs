using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TennisScores.Controllers;
using TennisScores.Infrastructure;
using TennisScores.Infrastructure.IO;
using TennisScores.Models;

namespace TennisScores.Tests.Controllers
{
    [TestFixture]
    public class ScoreControllerTests
    {
        private IScoreReader _mockScoreReader;
        private IScoreWriter _mockScoreWriter;
        private IMatchCalculator _mockMatchCalculator;
        private IScoreFormatter _mockScoreFormatter;

        private ScoreController _sut;

        [SetUp]
        public void Setup()
        {
            _mockScoreReader = Substitute.For<IScoreReader>();
            _mockScoreWriter = Substitute.For<IScoreWriter>();
            _mockMatchCalculator = Substitute.For<IMatchCalculator>();
            _mockScoreFormatter = Substitute.For<IScoreFormatter>();

            _sut = new ScoreController(_mockScoreReader, _mockScoreWriter, _mockMatchCalculator, _mockScoreFormatter);
        }

        [Test]
        public async Task EvaluateScores_ExpectScoreReaderCalled()
        {
            var inputPath = "input";

            _mockScoreReader.ReadFile(inputPath).Returns(new Fixture().Create<List<TennisGame>>());
            _mockScoreWriter.WriteFile(Arg.Any<string>(), Arg.Any<List<string>>()).Returns(true);

            await _sut.EvaluateScores(inputPath, "output");

            await _mockScoreReader
                .Received(1)
                .ReadFile(inputPath);
        }

        [Test]
        public void EvaluateScores_WhenNoScores_ExpectArgumentOutOfBoundsException_WithMessage()
        {
            _mockScoreReader.ReadFile(Arg.Any<string>()).Returns(new List<TennisGame>());

            Func<Task> action = async () => await _sut.EvaluateScores("input", "output");

            action.ShouldThrow<ArgumentOutOfRangeException>().WithMessage("Specified argument was out of the range of valid values.\r\nParameter name: No scores found.");
        }

        [Test]
        public async Task EvaluateScores_ExpectMatchCalculatorCalled_ForEachGame()
        {
            var games = new List<TennisGame>();
            new Fixture().AddManyTo(games, 10);

            _mockScoreReader.ReadFile(Arg.Any<string>()).Returns(games);
            _mockScoreWriter.WriteFile(Arg.Any<string>(), Arg.Any<List<string>>()).Returns(true);

            await _sut.EvaluateScores("input", "output");

            await _mockMatchCalculator
                .Received(games.Count)
                .Calculate(Arg.Any<TennisGame>());
        }

        [Test]
        public async Task EvaluateScores_ExpectScoreFormatterCalled()
        {
            var games = new List<TennisGame>();
            new Fixture().AddManyTo(games, 10);

            _mockScoreReader.ReadFile(Arg.Any<string>()).Returns(games);
            _mockMatchCalculator.Calculate(Arg.Any<TennisGame>()).Returns(new Fixture().Create<TennisMatch>());
            _mockScoreWriter.WriteFile(Arg.Any<string>(), Arg.Any<List<string>>()).Returns(true);

            await _sut.EvaluateScores("input", "output");

            await _mockScoreFormatter
                .Received(games.Count)
                .Format(Arg.Any<TennisMatch>());
        }

        [Test]
        public async Task EvaluateScores_ExpectWriterCalled()
        {
            var outputPath = "outputPath";

            var games = new List<TennisGame>();
            new Fixture().AddManyTo(games, 10);

            _mockScoreReader.ReadFile(Arg.Any<string>()).Returns(games);
            _mockMatchCalculator.Calculate(Arg.Any<TennisGame>()).Returns(new Fixture().Create<TennisMatch>());
            _mockScoreFormatter.Format(Arg.Any<TennisMatch>()).Returns(new Fixture().Create<string>());
            _mockScoreWriter.WriteFile(Arg.Any<string>(), Arg.Any<List<string>>()).Returns(true);

            await _sut.EvaluateScores("input", outputPath);

            await _mockScoreWriter
                .Received(1)
                .WriteFile(outputPath, Arg.Is<List<string>>(l => l.Count == games.Count));
        }

        [Test]
        public void EvaluateScores_WhenWriterSucceeds_ExpectNoError()
        {
            _mockScoreReader.ReadFile(Arg.Any<string>()).Returns(new Fixture().Create<List<TennisGame>>());
            _mockMatchCalculator.Calculate(Arg.Any<TennisGame>()).Returns(new Fixture().Create<TennisMatch>());
            _mockScoreFormatter.Format(Arg.Any<TennisMatch>()).Returns(new Fixture().Create<string>());
            _mockScoreWriter.WriteFile(Arg.Any<string>(), Arg.Any<List<string>>()).Returns(true);

            Func<Task> action = async () => await _sut.EvaluateScores("input", "output");

            action.ShouldNotThrow();
        }
        
        [Test]
        public void EvaluateScores_WhenWriterFailes_ExpectException_WithMessage()
        {
            _mockScoreReader.ReadFile(Arg.Any<string>()).Returns(new Fixture().Create<List<TennisGame>>());
            _mockMatchCalculator.Calculate(Arg.Any<TennisGame>()).Returns(new Fixture().Create<TennisMatch>());
            _mockScoreFormatter.Format(Arg.Any<TennisMatch>()).Returns(new Fixture().Create<string>());
            _mockScoreWriter.WriteFile(Arg.Any<string>(), Arg.Any<List<string>>()).Returns(false);

            Func<Task> action = async () => await _sut.EvaluateScores("input", "output");

            action.ShouldThrow<Exception>().WithMessage("Failed to write to file.");
        }
    }
}
