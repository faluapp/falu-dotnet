using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Falu.MessageTemplates;

/// <summary>
/// Helper for creating models used to render message templates when sending messages.
/// </summary>
[JsonConverter(typeof(Serialization.MessageTemplateModelJsonConverter))]
public readonly struct MessageTemplateModel : IEquatable<MessageTemplateModel>
{
    private readonly JsonObject @object;

    /// <summary>Creates an instance of <see cref="MessageTemplateModel"/>.</summary>
    /// <param name="object">The <see cref="JsonObject"/> instance to be held.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="object"/> is <see langword="null"/>.
    /// </exception>
    public MessageTemplateModel(JsonObject @object)
    {
        this.@object = @object ?? throw new ArgumentNullException(nameof(@object));
    }

    /// <summary>
    /// The <see cref="JsonObject"/> instance to be held.
    /// </summary>
    public JsonObject Object => @object;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is MessageTemplateModel model && Equals(model);

    /// <inheritdoc/>
    public bool Equals(MessageTemplateModel other) => EqualityComparer<JsonObject>.Default.Equals(@object, other.@object);

    /// <inheritdoc/>
    public override int GetHashCode() => @object.GetHashCode();

    ///
    public static bool operator ==(MessageTemplateModel left, MessageTemplateModel right) => left.Equals(right);

    ///
    public static bool operator !=(MessageTemplateModel left, MessageTemplateModel right) => !(left == right);

    /// <summary>Convert a <see cref="MessageTemplateModel"/> to a <see cref="JsonObject"/>.</summary>
    /// <param name="model">The <see cref="MessageTemplateModel"/> to convert.</param>
    public static implicit operator JsonObject(MessageTemplateModel model) => model.@object;

    /// <summary>Convert a <see cref="JsonObject"/> to a <see cref="MessageTemplateModel"/>.</summary>
    /// <param name="object">The <see cref="JsonObject"/> to convert.</param>
    public static implicit operator MessageTemplateModel(JsonObject @object) => new(@object);

    /// <summary>Create a <see cref="MessageTemplateModel"/> from another model object type.</summary>
    /// <typeparam name="TValue">The type of the model object.</typeparam>
    /// <param name="model">The model object used to create the model</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="model"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="model"/> cannot be serialized into a JSON object.
    /// </exception>
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    public static MessageTemplateModel Create<TValue>(TValue model, JsonSerializerOptions? options = null)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        EnsureAllowedModelType(model);

        var node = JsonSerializer.SerializeToNode(value: model, options: options);
        return new MessageTemplateModel(Create(node));
    }

    /// <summary>Create a <see cref="MessageTemplateModel"/> from another model object type.</summary>
    /// <typeparam name="TValue">The type of the model object.</typeparam>
    /// <param name="model">The model object used to create the model</param>
    /// <param name="jsonTypeInfo">Metadata about the type to convert.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="model"/> or <paramref name="jsonTypeInfo"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="model"/> cannot be serialized into a JSON object.
    /// </exception>
    public static MessageTemplateModel Create<TValue>(TValue model, JsonTypeInfo<TValue> jsonTypeInfo)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        EnsureAllowedModelType(model);

        var node = JsonSerializer.SerializeToNode(value: model, jsonTypeInfo: jsonTypeInfo);
        return new MessageTemplateModel(Create(node));
    }

    /// <summary>Create a <see cref="MessageTemplateModel"/> from another model object type.</summary>
    /// <param name="model">The model object used to create the model</param>
    /// <param name="inputType">The type of the <paramref name="model"/> to convert.</param>
    /// <param name="context">A metadata provider for serializable types.</param>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="JsonConverter"/>
    /// for <paramref name="inputType"/> or its serializable members.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="model"/>, or <paramref name="inputType"/>, or <paramref name="context"/>
    /// is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="model"/> cannot be serialized into a JSON object.
    /// </exception>
    /// 
    /// <exception cref="NotSupportedException">
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// The <see cref="JsonSerializerContext.GetTypeInfo(Type)"/> method of the provided
    /// <paramref name="context"/> returns <see langword="null"/> for the type to convert,
    /// or <paramref name="model"/> cannot be serialized into a JSON object.
    /// </exception>
    public static MessageTemplateModel Create(object model, Type inputType, JsonSerializerContext context)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        EnsureAllowedModelType(model);

        var node = JsonSerializer.SerializeToNode(value: model, inputType: inputType, context: context);
        return new MessageTemplateModel(Create(node));
    }

    /// <param name="node"></param>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="node"/> cannot be serialized into a JSON object.
    /// </exception>
    private static MessageTemplateModel Create(JsonNode? node)
    {
        return node is not JsonObject @object
            ? throw new InvalidOperationException("The model provided must be an array at the root.")
            : new MessageTemplateModel(@object);
    }

    #region Type checking

    private static readonly Type[] otherPrimitives = new[] {
        typeof(string),
        typeof(decimal),
        typeof(DateTime),
        typeof(DateTimeOffset),
        typeof(TimeSpan),
        typeof(Guid),
    };

    internal static bool IsAllowedModelType(Type type)
    {
        if (type is null) throw new ArgumentNullException(nameof(type));

        if (type.IsGenericType)
        {
            var gt = type.GetGenericTypeDefinition();
            if (gt == typeof(Nullable<>))
            {
                var gta = type.GenericTypeArguments[0];
                return IsAllowedModelType(gta);
            }
        }

        if (type.IsPrimitive || otherPrimitives.Contains(type) || type.IsEnum || type.IsArray) return false;
        return !typeof(IEnumerable).IsAssignableFrom(type) || typeof(IDictionary).IsAssignableFrom(type);
    }

    internal static void EnsureAllowedModelType(object? @object)
    {
        if (@object is not null)
        {
            var type = @object.GetType();
            if (!IsAllowedModelType(type))
            {
                throw new InvalidOperationException($"Type '{type.FullName}' is not allowed for a MessageTemplate model. Try a plain object of IDictionary<string, object>");
            }
        }
    }

    #endregion

}
