using FluentAssertions;
using io;
using Newtonsoft.Json.Schema;
using Xunit;

namespace configuration_test
{
    public class JsonSchemaReaderTests
    {
        [Fact]
        public void should_read_and_validate_jsonschema()
        {
            var schemaJson = new FileReader("configuration_schema.json").Read();
            schemaJson.Should().NotBeNull();

            var schema = JSchema.Parse(schemaJson);
            schema.Description.Should().Be("Schema for configuration.");
        }
    }
}