namespace configuration
{
    public class ConfigurationReader
    {
        private string _filename;
        private string _schema;

        public ConfigurationReader Schema(string schema)
        {
            this._schema = schema;
            return this;
        }
        
        public ConfigurationReader File(string filename)
        {
            this._filename = filename;
            return this;
        }
        
        public Configuration Read()
        {
            return null;
        }
    }
}