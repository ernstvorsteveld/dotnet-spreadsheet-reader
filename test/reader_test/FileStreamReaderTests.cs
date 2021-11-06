using System;
using reader;
using Xunit;
using FluentAssertions;

namespace reader_test
{
    public class FileStreamReaderTests
    {
        [Fact]
        public void should_read_spreadsheet()
        {
            var reader = new FileStreamReaderBuilder()
                .Schema("configuration_schema.json")
                .Configuration("configuration.json")
                .FilePath("products1.xlsx")
                .Create();
            var line = reader.Next();
            ((double) line["supplier"]).Should().Be(791080);
            line = reader.Next();
            ((double) line["supplier"]).Should().Be(791080);
        }
    }
}