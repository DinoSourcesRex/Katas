using System.Collections;
using System.Collections.Generic;

namespace RoverExercise.Infrastructure
{
    public class CommandQueue<T> : ICommandQueue<T>
    {
        private readonly Queue<T> _queue = new Queue<T>();

        public int Limit { get; }

        public CommandQueue(int limit)
        {
            Limit = limit;
        }

        public int Count()
        {
            return _queue.Count;
        }

        public void Queue(T obj)
        {
            if (_queue.Count < Limit)
            {
                _queue.Enqueue(obj);
            }
        }

        public T Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Clear()
        {
            _queue.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }
    }
}