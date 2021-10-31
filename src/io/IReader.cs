using System;

namespace io
{
    public interface IReader<out TO>
    {
        TO Read();
    }
}
