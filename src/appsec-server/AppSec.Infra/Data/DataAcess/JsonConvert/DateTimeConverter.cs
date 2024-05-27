using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppSec.Infra.Data.DataAcess.JsonConvert
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public string TimeFomart;
        public DateTimeConverter(string format = "yyyy-MM-dd'T'HH:mm:sszzz")
        {
            this.TimeFomart = format;
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString(this.TimeFomart));
        }
    }

}
