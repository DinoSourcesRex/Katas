using System.Collections.Generic;
using System.Threading.Tasks;

namespace TennisScores.Infrastructure.IO
{
    public class StreamReader : IStreamReader
    {
        public async Task<List<string>> ReadLineAsync(string filePath)
        {
            var list = new List<string>();

            using (var reader = new System.IO.StreamReader(filePath))
            {
                string line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    list.Add(line);
                }
            }

            return list;
        }
    }
}