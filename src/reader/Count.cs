namespace reader
{
    public class Count<T>
    {
        private readonly T _count;

        public Count(T count)
        {
            _count = count;
        }

        public T Get()
        {
            return _count;
        }
    }
}