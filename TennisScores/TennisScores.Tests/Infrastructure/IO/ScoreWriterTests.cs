using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using TennisScores.Infrastructure.IO;

namespace TennisScores.Tests.Infrastructure.IO
{
    [TestFixture]
    public class ScoreWriterTests
    {
        private IStreamWriter _mockWriter;

        private ScoreWriter _sut;

        [SetUp]
        public void Setup()
        {
            _mockWriter = Substitute.For<IStreamWriter>();

            _sut = new ScoreWriter(_mockWriter);
        }

        [Test]
        public async Task WriteFile_WhenStreamWriterThrows_ShouldReturnFalse()
        {
            var path = "path";

            var list = new List<string>
            {
                "I",
                "am",
                "an",
                "item"
            };

            _mockWriter.WriteLineAsync(Arg.Is(path), Arg.Is(list))
                .Throws<Exception>();

            var result = await _sut.WriteFile(path, list);

            result.Should().BeFalse();
        }

        [Test]
        public async Task WriteFile_WhenExecuted_ShouldCallStreamWriter()
        {
            var path = "path";

            var list = new List<string>
            {
                "I",
                "am",
                "an",
                "item"
            };

            await _mockWriter.WriteLineAsync(Arg.Is(path), Arg.Is(list));

            await _sut.WriteFile(path, list);

            await _mockWriter.Received(1)
                .WriteLineAsync(path, list);
        }

        [Test]
        public async Task WriteFile_WhenExecuted_ShouldReturnTrue()
        {
            var path = "path";

            var list = new List<string>
            {
                "I",
                "am",
                "an",
                "item"
            };

            await _mockWriter.WriteLineAsync(Arg.Is(path), Arg.Is(list));

            var result = await _sut.WriteFile(path, list);

            result.Should().BeTrue();
        }
    }
}
