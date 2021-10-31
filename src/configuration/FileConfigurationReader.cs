using System;
using System.Collections.Generic;
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
            var schema = ReadAndParseSchema(this._schema);
            var configuration = ReadAndParseJson(this._filename);
            var valid = configuration.IsValid(schema!, out IList<string> errorMessages);
            if (!valid)
            {
                throw new ConfigurationNotValidException(errorMessages);
            }
        }

        private JObject ReadAndParseJson(string file)
        {
            return JObject.Parse(new FileReader(file).Read());
        }

        private JSchema ReadAndParseSchema(string file)
        {
            return JSchema.Parse(new FileReader(file).Read());
        }
    }

    public class ConfigurationNotValidException : Exception
    {
        private readonly IList<string> _errors;

        public ConfigurationNotValidException(IList<string> errorMessages)
        {
            _errors = errorMessages;
        }
    }
}