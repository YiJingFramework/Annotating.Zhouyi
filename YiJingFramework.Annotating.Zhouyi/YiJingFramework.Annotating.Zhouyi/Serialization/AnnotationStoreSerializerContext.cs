using System.Text.Json.Serialization;

namespace YiJingFramework.Annotating.Zhouyi.Serialization;

[JsonSerializable(typeof(AnnotationStore))]
internal sealed partial class AnnotationStoreSerializerContext : JsonSerializerContext
{
}
