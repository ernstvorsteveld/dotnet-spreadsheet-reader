using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using configuration;
using ExcelDataReader;

namespace reader
{
    public class FileStreamReader : IReader<long, string, object>
    {
        private IExcelDataReader _reader;
        private Configuration _configuration;

        public FileStreamReader(
            SchemaFile schemaFile,
            ConfigurationFile configurationFile,
            XlsFile xlsFile)
        {
            _configuration = new FileConfigurationReader(schemaFile.Value(), configurationFile.Value()).Execute();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var stream = File.Open(xlsFile.Value(), FileMode.Open, FileAccess.Read);
            // // Auto-detect format, supports:
            // //  - Binary Excel files (2.0-2003 format; *.xls)
            // //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
            _reader = ExcelReaderFactory.CreateReader(stream);
        }

        public Count<long> Count()
        {
            throw new System.NotImplementedException();
        }

        public void Skip()
        {
            _reader.Read();
        }

        public IDictionary<string, object> Next()
        {
            var row = GetRow(_reader);
            return row;
        }

        private IDictionary<string, object> GetRow(IDataReader reader)
        {
            while (reader.Read())
            {
                return ReadLine(reader);
            }

            return null;
        }

        private IDictionary<string, object> ReadLine(IDataRecord excelDataReader)
        {
            IDictionary<string, object> row = new Dictionary<string, object>();
            var i = 0;
            foreach (var mapping in _configuration.Mappings)
            {
                switch (mapping.From.Type)
                {
                    case "int32":
                    {
                        row.Add(mapping.To.Destination, excelDataReader.GetInt32(i));
                        break;
                    }
                    case "double":
                    {
                        row.Add(mapping.To.Destination, _reader.GetDouble(i));
                        break;
                    }
                    case "string":
                    {
                        row.Add(mapping.To.Destination, excelDataReader.GetString(i));
                        break;
                    }
                    default:
                    {
                        throw new ElementNotFoundException(mapping);
                    }
                }

                i++;
            }

            return row;
        }
    }

    public class XlsFile
    {
        private readonly string _filePath;

        public XlsFile(string filePath)
        {
            _filePath = filePath;
        }

        public string Value()
        {
            return _filePath;
        }
    }

    public class SchemaFile
    {
        private readonly string _schema;

        public SchemaFile(string schema)
        {
            _schema = schema;
        }

        public string Value()
        {
            return _schema;
        }
    }

    public class ConfigurationFile
    {
        private readonly string _configuration;

        public ConfigurationFile(string configurationFile)
        {
            _configuration = configurationFile;
        }

        public string Value()
        {
            return _configuration;
        }
    }

    internal class ElementNotFoundException : Exception
    {
        private readonly Mapping _mapping;

        public ElementNotFoundException(Mapping mapping)
        {
            _mapping = mapping;
        }
    }

    public class FileStreamReaderBuilder
    {
        private string _filePath;
        private string _schemaFile;
        private string _configurationFile;

        public FileStreamReaderBuilder FilePath(string filePath)
        {
            _filePath = filePath;
            return this;
        }

        public FileStreamReaderBuilder Schema(string schemaFile)
        {
            _schemaFile = schemaFile;
            return this;
        }

        public FileStreamReaderBuilder Configuration(string configurationFile)
        {
            _configurationFile = configurationFile;
            return this;
        }

        public FileStreamReader Create()
        {
            var fileStreamReader = new FileStreamReader(
                new SchemaFile(_schemaFile),
                new ConfigurationFile(_configurationFile),
                new XlsFile(_filePath));
            fileStreamReader.Skip();
            return fileStreamReader;
        }
    }
}