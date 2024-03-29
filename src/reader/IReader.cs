﻿using System.Collections.Generic;

namespace reader
{
    public interface IReader<T, TK, TV>
    {
        Count<T> Count();
        IDictionary<TK, TV> Next();
    }

    public enum Attributes {
        Supplier
    }

}