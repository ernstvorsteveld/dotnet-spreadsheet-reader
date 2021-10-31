using System;
using System.Collections.Generic;

namespace reader
{
    public interface IReader<T, TK, TV>
    {
        Count<T> Count();
        Dictionary<TK, TV> Next();
    }

}