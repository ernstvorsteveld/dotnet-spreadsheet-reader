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
            var reader = new FileConfigurationReader();
            var configuration = reader
                .Schema("configuration_schema.json")
                .Configuration("configuration.json")
                .Execute();
            configuration.Mappings.Count.Should().Be(1);
        }
    }
}