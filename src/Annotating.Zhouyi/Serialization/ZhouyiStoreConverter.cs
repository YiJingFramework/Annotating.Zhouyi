using System.Text.Json;
using System.Text.Json.Serialization;

namespace YiJingFramework.Annotating.Zhouyi.Serialization;

/// <summary>
/// 用于 <seealso cref="ZhouyiStore"/> 的 <seealso cref="JsonConverter"/> 。
/// <seealso cref="JsonConverter"/> for <seealso cref="ZhouyiStore"/>.
/// </summary>
public sealed class ZhouyiStoreConverter : JsonConverter<ZhouyiStore>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override ZhouyiStore? Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        options = new JsonSerializerOptions(options);
        var context = new AnnotationStoreSerializerContext(options);
        var store = JsonSerializer.Deserialize(ref reader, context.AnnotationStore);
        return new ZhouyiStore(store);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(
        Utf8JsonWriter writer, ZhouyiStore value, JsonSerializerOptions options)
    {
        options = new JsonSerializerOptions(options);
        var context = new AnnotationStoreSerializerContext(options);
        JsonSerializer.Serialize(writer, value.Store, context.AnnotationStore);
    }
}
