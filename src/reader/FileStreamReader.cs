using System.Collections.Generic;
using System.IO;
using configuration;
using ExcelDataReader;

namespace reader
{
    public class FileStreamReader : IReader<long, string, object>
    {
        private IExcelDataReader _reader;
        private Configuration _configuration;

        public Count<long> Count()
        {
            throw new System.NotImplementedException();
        }

        public FileStreamReader FilePath(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            // Auto-detect format, supports:
            //  - Binary Excel files (2.0-2003 format; *.xls)
            //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
            _reader = ExcelReaderFactory.CreateReader(stream);
            return this;
        }

        public FileStreamReader Configuration(string schema, string configuration)
        {
            _configuration = new FileConfigurationReader()
                .Schema(schema)
                .Configuration(configuration)
                .Execute();
            return this;
        }

        public Dictionary<string, object> Next()
        {
            if (_reader.Read())
            {
                var row = getRow(_configuration, _reader);
                _reader.NextResult();
                return row;
            }

            return null;
        }

        private Dictionary<string, object> getRow(Configuration configuration, IExcelDataReader reader)
        {
            var row = new Dictionary<string, object>();
            return row;
        }
    }
}