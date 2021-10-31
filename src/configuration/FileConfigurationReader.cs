using System;
using io;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace configuration
{
    public class FileConfigurationReader : IConfigurationReader
    {
        private string _filename;
        private string _schema;

        public FileConfigurationReader Schema(string schema)
        {
            _schema = schema;
            return this;
        }

        public FileConfigurationReader Configuration(string filename)
        {
            _filename = filename;
            return this;
        }

        public Configuration Execute()
        {
            ValidateConfiguration();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(new FileReader(this._filename).Read());
        }

        private void ValidateConfiguration()
        {
            var schema = ReadAndParse(this._schema);
            var configuration = ReadAndParse(this._filename);
            var valid = configuration.IsValid(((JSchema) schema)!);
            if (!valid)
            {
                throw new SchemaNotValidException();
            }
        }

        private JObject ReadAndParse(string file)
        {
            var json = new FileReader(file).Read();
            return (JObject) JSchema.Parse(json);
        }
    }

    public class SchemaNotValidException : Exception
    {
    }
}