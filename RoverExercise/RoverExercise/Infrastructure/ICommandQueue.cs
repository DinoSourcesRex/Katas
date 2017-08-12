using System.Collections.Generic;

namespace RoverExercise.Infrastructure
{
    public interface ICommandQueue<T> : IEnumerable<T>
    {
        int Limit { get; }

        int Count();
        void Queue(T obj);
        T Dequeue();
        void Clear();
    }
}