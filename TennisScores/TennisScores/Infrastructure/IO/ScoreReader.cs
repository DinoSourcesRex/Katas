using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure.IO
{
    public class ScoreReader : IScoreReader
    {
        private readonly IStreamReader _streamReader;

        public ScoreReader(IStreamReader streamReader)
        {
            _streamReader = streamReader;
        }

        public async Task<List<TennisGame>> ReadFile(string filePath)
        {
            var gamesList = new List<TennisGame>();

            var fileLines = new List<string>();

            try
            {
                fileLines = await _streamReader.ReadLineAsync(filePath);
            }
            catch (Exception)
            {
            }

            foreach (var line in fileLines)
            {
                var tennisGame = new TennisGame(new List<char>());

                foreach (var character in line)
                {
                    tennisGame.Points.Add(character);
                }
                gamesList.Add(tennisGame);
            }

            return gamesList;
        }
    }
}