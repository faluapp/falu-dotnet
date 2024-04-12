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
/// <param name="object">The <see cref="JsonObject"/> instance to be held.</param>
[JsonConverter(typeof(Serialization.MessageTemplateModelJsonConverter))]
public readonly struct MessageTemplateModel(JsonObject @object) : IEquatable<MessageTemplateModel>
{
    /// <summary>
    /// An empty template model that does not require serialization
    /// </summary>
    public static readonly MessageTemplateModel Empty = new([]);

    private readonly JsonObject @object = @object ?? throw new ArgumentNullException(nameof(@object));

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

    #region Conversion to other types

    /// <summary>Create a <typeparamref name="TValue"/> from the template's backing object.</summary>
    /// <typeparam name="TValue">The type to deserialize the template into.</typeparam>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <returns>A <typeparamref name="TValue"/> representation of the template.</returns>
    /// <exception cref="InvalidOperationException">
    /// The model does not container a backing object.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    [RequiresUnreferencedCode(MessageStrings.SerializationUnreferencedCodeMessage)]
    [RequiresDynamicCode(MessageStrings.SerializationRequiresDynamicCodeMessage)]
    public TValue? ConvertTo<TValue>(JsonSerializerOptions? options = null)
    {
        if (Object is null) throw new InvalidOperationException("The model must contain a backing object");
        EnsureAllowedModelType<TValue>();

        return JsonSerializer.Deserialize<TValue>(Object, options: options);
    }

    /// <summary>Create a <typeparamref name="TValue"/> from the template's backing object.</summary>
    /// <typeparam name="TValue">The type to deserialize the template into.</typeparam>
    /// <param name="jsonTypeInfo">Metadata about the type to convert.</param>
    /// <exception cref="InvalidOperationException">
    /// The model does not container a backing object.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="jsonTypeInfo"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// <typeparamref name="TValue" /> is not compatible with the JSON.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <typeparamref name="TValue"/> or its serializable members.
    /// </exception>
    public TValue? ConvertTo<TValue>(JsonTypeInfo<TValue> jsonTypeInfo)
    {
        if (Object is null) throw new InvalidOperationException("The model must contain a backing object");
        EnsureAllowedModelType<TValue>();

        return JsonSerializer.Deserialize(Object, jsonTypeInfo: jsonTypeInfo);
    }

    /// <summary>
    /// Converts the template's backing object into a <paramref name="returnType"/>.
    /// </summary>
    /// <param name="returnType">The type of the object to convert to and return.</param>
    /// <param name="context">A metadata provider for serializable types.</param>
    /// <returns>A <paramref name="returnType"/> representation of the JSON value.</returns>
    /// <exception cref="System.ArgumentNullException">
    /// <paramref name="returnType"/> is <see langword="null"/>.
    ///
    /// -or-
    ///
    /// <paramref name="context"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid.
    ///
    /// -or-
    ///
    /// <paramref name="returnType" /> is not compatible with the JSON.
    ///
    /// -or-
    ///
    /// There is remaining data in the string beyond a single JSON value.</exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
    /// for <paramref name="returnType"/> or its serializable members.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// The <see cref="JsonSerializerContext.GetTypeInfo(Type)"/> method of the provided
    /// <paramref name="context"/> returns <see langword="null"/> for the type to convert.
    /// </exception>
    public object? ConvertTo(Type returnType, JsonSerializerContext context)
    {
        if (Object is null) throw new InvalidOperationException("The model must contain a backing object");
        EnsureAllowedModelType(returnType);

        return JsonSerializer.Deserialize(Object, returnType: returnType, context: context);
    }
    #endregion

    #region Creation from other types

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

        return Create(JsonSerializer.SerializeToNode(value: model, options: options));
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

        return Create(JsonSerializer.SerializeToNode(value: model, jsonTypeInfo: jsonTypeInfo));
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

        return Create(JsonSerializer.SerializeToNode(value: model, inputType: inputType, context: context));
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

    #endregion

    #region Type checking

    private static readonly Type[] otherPrimitives = [
        typeof(string),
        typeof(decimal),
        typeof(DateTime),
        typeof(DateTimeOffset),
        typeof(TimeSpan),
        typeof(Guid),
    ];

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

    internal static void EnsureAllowedModelType(Type type)
    {
        if (IsAllowedModelType(type)) return;
        throw new InvalidOperationException($"Type '{type.FullName}' is not allowed for a MessageTemplate model. Try a plain object of IDictionary<string, object>");
    }

    internal static void EnsureAllowedModelType<T>() => EnsureAllowedModelType(typeof(T));

    internal static void EnsureAllowedModelType(object? @object)
    {
        if (@object is null) return;
        EnsureAllowedModelType(@object.GetType());
    }

    #endregion

}
