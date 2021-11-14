using System.Collections.Generic;
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
            validate(reader.Next(), line1);
            validate(reader.Next(), line1);
            validate(reader.Next(), line1);
        }

        private void validate(IDictionary<string, object> values, IDictionary<string, object> line)
        {
            ((double) values["supplier"]).Should().Be((double) line1["supplier"]);
            ((string) values["tariff_name"]).Should().Be((string) line["tariff_name"]);
            ((string) values["tariff_code"]).Should().Be((string) line["tariff_code"]);
            ((string) values["valid_from"]).Should().Be((string) line["valid_from"]);
            ((bool) values["is_ecological"]).Should().Be((bool) line["is_ecological"]);
            ((bool) values["is_differential"]).Should().Be((bool) line["is_differential"]);
            ((bool) values["is_default_tariff"]).Should().Be((bool) line["is_default_tariff"]);
            ((bool) values["is_tiered_tariff"]).Should().Be((bool) line["is_tiered_tariff"]);
        }

        private static readonly IDictionary<string, object> line1 = new Dictionary<string, object>();

        static FileStreamReaderTests()
        {
            line1.Add("supplier", (double) 791080);
            line1.Add("tariff_name", "Erdgas12");
            line1.Add("tariff_code", "GP12214B");
            line1.Add("valid_from", "01-10-2021");
            line1.Add("is_ecological", true);
            line1.Add("is_differential", false);
            line1.Add("is_default_tariff", false);
            line1.Add("is_tiered_tariff", true);
        }
    }
}