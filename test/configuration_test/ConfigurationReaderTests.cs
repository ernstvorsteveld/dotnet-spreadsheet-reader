using configuration;
using Xunit;
using FluentAssertions;

namespace configuration_test
{
    public class ConfigurationReaderTests
    {
        [Fact]
        public void should_read_configuration()
        {
            var reader = new FileConfigurationReader("configuration_schema.json", "configuration.json");
            var configuration = reader.Execute();
            configuration.Formats.DateFormat.Should().Be("dd-MM-yy");
            configuration.Mappings.Count.Should().Be(1);
            var mapping = configuration.Mappings[0];
            mapping.Index.Should().Be(0);
            mapping.From.Column.Should().Be("SupplierNumber");
            mapping.From.Type.Should().Be("string");
            mapping.To.Destination.Should().Be("supplier");

            configuration.Header.Read.Should().Be(true);
            configuration.Header.SkipBefore.Should().Be(0);
            configuration.Header.SkipAfter.Should().Be(0);
        }
    }
}