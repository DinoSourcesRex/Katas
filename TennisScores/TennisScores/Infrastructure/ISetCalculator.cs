using System.Threading.Tasks;
using TennisScores.Models;

namespace TennisScores.Infrastructure
{
    public interface ISetCalculator
    {
        Task<SetResult> Calculate(TennisGame game);
    }
}