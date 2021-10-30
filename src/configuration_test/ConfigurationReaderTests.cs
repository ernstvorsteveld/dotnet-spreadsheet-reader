using Xunit;
using FluentAssertions;
using Newtonsoft.Json.Schema;


namespace configuration_test
{
    public class ConfigurationReaderTests
    {
        [Fact]
        public void should_read_and_validate_jsonschema()
        {
            var schemaJson = System.IO.File.ReadAllText("configuration_schema.json");
            schemaJson.Should().NotBeNull();
            
            JSchema schema = JSchema.Parse(schemaJson);
            schema.Description.Should().Be("Schema for configuration.");
        }
    }
}
