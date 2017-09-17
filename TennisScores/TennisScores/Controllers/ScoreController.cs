using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisScores.Infrastructure;
using TennisScores.Infrastructure.IO;

namespace TennisScores.Controllers
{
    public class ScoreController
    {
        private readonly IScoreReader _scoreReader;
        private readonly IScoreWriter _scoreWriter;
        private readonly IMatchCalculator _matchCalculator;
        private readonly IScoreFormatter _scoreFormatter;

        public ScoreController(IScoreReader scoreReader, IScoreWriter scoreWriter, IMatchCalculator matchCalculator, IScoreFormatter scoreFormatter)
        {
            _scoreReader = scoreReader;
            _scoreWriter = scoreWriter;
            _matchCalculator = matchCalculator;
            _scoreFormatter = scoreFormatter;
        }

        public async Task EvaluateScores(string inputFileLocation, string outputFileLocation)
        {
            var readerResult = await _scoreReader.ReadFile(inputFileLocation);

            if (readerResult.Count == 0)
            {
                throw new ArgumentOutOfRangeException("No scores found.");
            }

            var formattedMatches = new List<string>();

            foreach (var match in readerResult)
            {
                var calculatedMatch = await _matchCalculator.Calculate(match);
                var formattedMatch = await _scoreFormatter.Format(calculatedMatch);
                formattedMatches.Add(formattedMatch);
            }

            var writerSucceeded = await _scoreWriter.WriteFile(outputFileLocation, formattedMatches);

            if (!writerSucceeded)
            {
                throw new Exception("Failed to write to file.");
            }
        }
    }
}