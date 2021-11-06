using configuration;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json.Schema;


namespace configuration_test
{
    public class ConfigurationReaderTests
    {
        [Fact]
        public void should_read_configuration()
        {
            var reader = new FileConfigurationReader("configuration_schema.json", "configuration.json");
            var configuration = reader.Execute();
            configuration.Mappings.Count.Should().Be(1);
            var mapping = configuration.Mappings[0];
            mapping.From.Column.Should().Be("SupplierNumber");
            mapping.From.Type.Should().Be("string");
            mapping.To.Destination.Should().Be("supplier");
        }
    }
}