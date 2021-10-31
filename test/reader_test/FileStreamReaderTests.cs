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
            var reader = new FileStreamReader()
                .Configuration("configuration_schema.json", "configuration.json")
                .FilePath("products1.xlsx");
            var line = reader.Next();
            ((string) line["supplier"]).Should().Be("none");
        }
    }
}