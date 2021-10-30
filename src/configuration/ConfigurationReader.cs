using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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

        public Configuration Execute()
        {
            ValidateConfiguration();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(
                System.IO.File.ReadAllText(this._filename));
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
            var json = System.IO.File.ReadAllText(file);
            return (JObject) JSchema.Parse(json);
        }
    }

    internal class SchemaNotValidException : Exception
    {
    }
}