using System.Collections.Generic;

namespace mapper
{
    public interface IMapper
    {
        IDictionary<string,object> Map(IDictionary<string,object> input);
    }
}
