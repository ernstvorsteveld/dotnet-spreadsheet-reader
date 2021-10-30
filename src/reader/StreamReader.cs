using System.Collections.Generic;

namespace reader
{
    public class StreamReader : IReader<long, string, object>
    {
        public Count<long> Count()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, object> Next()
        {
            throw new System.NotImplementedException();
        }
    }
}