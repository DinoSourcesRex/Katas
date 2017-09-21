using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisScores.Infrastructure.IO
{
    public class StreamWriter : IStreamWriter
    {
        public async Task WriteLineAsync(string filePath, List<string> list)
        {
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                foreach (var line in list)
                {
                    await writer.WriteLineAsync(line);
                }
            }
        }
    }
}