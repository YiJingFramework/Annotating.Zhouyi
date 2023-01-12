using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YiJingFramework.Annotating.Zhouyi.Serialization
{
    internal sealed class ZhouyiStoreConverter : JsonConverter<ZhouyiStore>
    {
        public override ZhouyiStore? Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var store = JsonSerializer.Deserialize<AnnotationStore>(ref reader, options);
            return new ZhouyiStore(store);
        }

        public override void Write(
            Utf8JsonWriter writer, ZhouyiStore value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Store, options);
        }
    }
}
