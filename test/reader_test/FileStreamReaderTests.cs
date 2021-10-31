using System;
using configuration;
using io;
using Newtonsoft.Json;
using reader;
using Xunit;

namespace reader_test
{
    public class FileStreamReaderTests
    {
        [Fact]
        public void should_read_spreadsheet()
        {
            var reader = new FileStreamReader()
                .Configuration("configuration_schema.json", "configuration.json")
                .FilePath("products1.xlsx");
            reader.Next();
        }
    }
}