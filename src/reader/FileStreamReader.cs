using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using configuration;
using ExcelDataReader;

namespace reader
{
    public class FileStreamReader : IReader<long, string, object>
    {
        private readonly IExcelDataReader _reader;
        private readonly Configuration _configuration;

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
            throw new NotImplementedException();
        }

        public void Skip()
        {
            _reader.Read();
        }

        public IDictionary<string, object> Next()
        {
            var row = GetRow();
            return row;
        }

        private IDictionary<string, object> GetRow()
        {
            while (_reader.Read())
            {
                return ReadLine(_reader);
            }

            return null;
        }

        private void SkipBefore()
        {
            Skip(_configuration.Header.SkipBefore);
        }

        private void SkipAfter()
        {
            Skip(_configuration.Header.SkipAfter);
        }

        private void Skip(int lines)
        {
            for (var i = 0; i < lines; i++)
            {
                _reader.Read();
            }
        }

        private IDictionary<string, object> ReadLine(IDataRecord excelDataReader)
        {
            IDictionary<string, object> row = new Dictionary<string, object>();
            foreach (var mapping in _configuration.Mappings)
            {
                var key = mapping.From.Key;
                switch (mapping.From.Type)
                {
                    case "int32":
                    {
                        row.Add(key, excelDataReader.GetInt32(mapping.Index));
                        break;
                    }
                    case "double":
                    {
                        row.Add(key, _reader.GetDouble(mapping.Index));
                        break;
                    }
                    case "string":
                    {
                        row.Add(HandleStringValue(excelDataReader, mapping));
                        break;
                    }
                    case "boolean":
                    {
                        row.Add(key, _reader.GetBoolean(mapping.Index));
                        break;
                    }
                    default:
                    {
                        throw new ElementNotFoundException(mapping);
                    }
                }
            }

            return row;
        }

        private KeyValuePair<string, object> HandleStringValue(IDataRecord excelDataReader, Mapping mapping)
        {
            var key = mapping.From.Key;
            return mapping.From.Pattern switch
            {
                "date" => new KeyValuePair<string, object>(key,
                    FormatDate(excelDataReader.GetString(mapping.Index), mapping.From.Format)),
                "boolean" => new KeyValuePair<string, object>(key,
                    FormatBoolean(excelDataReader.GetString(mapping.Index))),
                _ => new KeyValuePair<string, object>(key,
                    ValidateString(excelDataReader.GetString(mapping.Index), mapping.From.RegEx))
            };
        }

        private bool FormatBoolean(string value)
        {
            return _configuration.Formats.BooleanTrueFormats.Contains(value);
        }

        private string FormatDate(string value, string pattern)
        {
            DateTime.TryParseExact(value, pattern, null, DateTimeStyles.None, out var parsedDate);
            return parsedDate.ToString(_configuration.Formats.DateFormat);
        }

        public String ValidateString(string value, string pattern)
        {
            if (Regex.IsMatch(value, pattern))
            {
                return value;
            }

            throw new ValueDoesNotMatchExpression();
        }
    }

    public class ValueDoesNotMatchExpression : Exception
    {
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