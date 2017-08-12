using System.Threading.Tasks;
using Rhino.Mocks.Interfaces;

namespace VendingMachine.Tests
{
    public static class MethodOptionsExtensions
    {
        /// <summary>
        /// Usage:
        ///     _mockRepository.Stub(r => r.Method(active: null)).ReturnAsync(object);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <param name="objToReturn"></param>
        /// <returns></returns>
        public static IMethodOptions<Task<T>> ReturnAsync<T>(this IMethodOptions<Task<T>> options, T objToReturn)
        {
            return options.Return(Task.FromResult(objToReturn));
        }
    }
}