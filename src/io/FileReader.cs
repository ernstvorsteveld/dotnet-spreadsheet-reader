namespace io
{
    public class FileReader : IReader<string>
    {
        private readonly string _filename;

        public FileReader(string filename)
        {
            this._filename = filename;
        }
        
        public string Read()
        {
            return System.IO.File.ReadAllText(this._filename);
        }
    }
}