using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisScores.Infrastructure.IO
{
    public class ScoreWriter : IScoreWriter
    {
        private readonly IStreamWriter _streamWriter;

        public ScoreWriter(IStreamWriter streamWriter)
        {
            _streamWriter = streamWriter;
        }

        public async Task<bool> WriteFile(string filePath, List<string> matches)
        {
            bool completed = true;

            try
            {
                await _streamWriter.WriteLineAsync(filePath, matches);
            }
            catch (Exception)
            {
                completed = false;
            }

            return completed;
        }
    }
}