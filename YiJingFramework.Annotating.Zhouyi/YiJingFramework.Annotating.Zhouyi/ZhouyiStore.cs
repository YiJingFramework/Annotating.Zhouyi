using System.Text.Json;
using YiJingFramework.Annotating.Entities;
using YiJingFramework.Core;

namespace YiJingFramework.Annotating.Zhouyi
{
    public sealed partial class ZhouyiStore
    {
        public AnnotationStore Store { get; }
        public ZhouyiStore(AnnotationStore? store = null)
        {
            this.Store = store ?? new AnnotationStore();
        }
        public string? Title 
        { 
            get => Store.Title;
            set 
            {
                Store.Title = value;
            } 
        }
        public IList<string> Tags => Store.Tags;
        public string SerializeToJsonString(
            JsonSerializerOptions? serializerOptions = null)
        {
            return JsonSerializer.Serialize(Store, serializerOptions);
        }
        public static ZhouyiStore? DeserializeFromJsonString(
            string s, JsonSerializerOptions? serializerOptions = null)
        {
            ArgumentNullException.ThrowIfNull(s);
            var d = JsonSerializer.Deserialize<AnnotationStore>(s, serializerOptions);
            if (d is null)
                return null;
            return new(d);
        }
    }
}