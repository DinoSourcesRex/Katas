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
        private readonly ISetScoreFormatter _setScoreFormatter;

        public ScoreController(IScoreReader scoreReader, IScoreWriter scoreWriter, IMatchCalculator matchCalculator, ISetScoreFormatter setScoreFormatter)
        {
            _scoreReader = scoreReader;
            _scoreWriter = scoreWriter;
            _matchCalculator = matchCalculator;
            _setScoreFormatter = setScoreFormatter;
        }

        public async Task<bool> EvaluateScores(string inputFileLocation, string outputFileLocation)
        {
            var succeeded = true;

            var readerResult = await _scoreReader.ReadFile(inputFileLocation);

            if (readerResult.Count > 0)
            {
                var formattedMatches = new List<string>();

                foreach (var match in readerResult)
                {
                    var calculatedMatch = await _matchCalculator.Calculate(match);
                    var formattedMatch = await _setScoreFormatter.Format(calculatedMatch);
                    formattedMatches.Add(formattedMatch);
                }

                var writerSucceeded = await _scoreWriter.WriteFile(outputFileLocation, formattedMatches);

                if (!writerSucceeded)
                {
                    succeeded = false;
                }
            }
            else
            {
                succeeded = false;
            }

            return succeeded;
        }
    }
}