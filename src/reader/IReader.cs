using System;
using System.Collections.Generic;

namespace reader
{
    public interface IReader<T, TK, TV>
    {
        Count<T> Count();
        Dictionary<TK, TV> Next();
    }

    public class Count<T>
    {
        private readonly T _count;

        public Count(T count)
        {
            this._count = count;
        }

        public T Get()
        {
            return _count;
        }
    }
}