using System.Text.Json;

namespace YiJingFramework.Annotating.Zhouyi
{
    /// <summary>
    /// 专用于周易的注解仓库。
    /// Specialized annotation store for Zhouyi.
    /// </summary>
    public sealed partial class ZhouyiStore
    {
        /// <summary>
        /// 内部的 <seealso cref="AnnotationStore"/> 。
        /// The inner <seealso cref="AnnotationStore"/>.
        /// </summary>
        public AnnotationStore Store { get; }

        /// <summary>
        /// 各注解组。
        /// The annotation groups.
        /// </summary>
        public ZhouyiGroups Groups { get; }

        /// <summary>
        /// 实例化一个 <seealso cref="ZhouyiStore"/> 。
        /// Initialize a <seealso cref="ZhouyiStore"/>.
        /// </summary>
        /// <param name="store">
        /// 内部的 <seealso cref="AnnotationStore"/> 。
        /// The inner <seealso cref="AnnotationStore"/>.
        /// 用 <c>null</c> 表示自动新建一个。
        /// <c>null</c> means to create a new one as the inner store.
        /// </param>
        public ZhouyiStore(AnnotationStore? store)
        {
            Groups = new ZhouyiGroups(this);
            Store = store ?? new AnnotationStore();
        }

        /// <summary>
        /// 仓库标题。
        /// Title of the store.
        /// </summary>
        public string? Title
        {
            get => Store.Title;
            set
            {
                Store.Title = value;
            }
        }

        /// <summary>
        /// 仓库标签。
        /// Tags of the store.
        /// </summary>
        public IList<string> Tags => Store.Tags;

        /// <summary>
        /// 序列化为 json 字符串。
        /// Serialize to a json string.
        /// </summary>
        /// <param name="serializerOptions">
        /// 序列化器选项。
        /// Serializer options.
        /// </param>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        public string SerializeToJsonString(
            JsonSerializerOptions? serializerOptions = null)
        {
            return Store.SerializeToJsonString(serializerOptions);
        }

        /// <summary>
        /// 从 json 字符串反序列化。
        /// Deserialize from a json string.
        /// </summary>
        /// <param name="s">
        /// json 字符串。
        /// The json string.
        /// </param>
        /// <param name="serializerOptions">
        /// 序列化器选项。
        /// Serializer options.
        /// </param>
        /// <returns>
        /// 结果。
        /// The result.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> 是 <c>null</c> 。
        /// <paramref name="s"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="JsonException">
        /// 反序列化失败。
        /// Deserialization failed.
        /// </exception>
        public static ZhouyiStore? DeserializeFromJsonString(
            string s, JsonSerializerOptions? serializerOptions = null)
        {
            ArgumentNullException.ThrowIfNull(s);
            var d = AnnotationStore.DeserializeFromJsonString(s, serializerOptions);
            if (d is null)
                return null;
            return new(d);
        }
    }
}