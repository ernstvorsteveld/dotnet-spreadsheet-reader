using FluentAssertions;
using Newtonsoft.Json.Schema;
using Xunit;

namespace configuration_test
{
    public class JsonSchemaReaderTests
    {
        [Fact]
        public void should_read_and_validate_jsonschema()
        {
            var schemaJson = System.IO.File.ReadAllText("configuration_schema.json");
            schemaJson.Should().NotBeNull();

            var schema = JSchema.Parse(schemaJson);
            schema.Description.Should().Be("Schema for configuration.");
        }
    }
}