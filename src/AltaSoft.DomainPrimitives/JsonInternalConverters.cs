using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides a set of static methods for retrieving JSON converters for various data types.
/// </summary>
public static class JsonInternalConverters
{
    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="float"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<float> SingleConverter => s_singleConverter ??= GetConverter<float>();

    private static JsonConverter<float>? s_singleConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="Guid"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<Guid> GuidConverter => s_guidConverter ??= GetConverter<Guid>();

    private static JsonConverter<Guid>? s_guidConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="bool"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<bool> BooleanConverter => s_booleanConverter ??= GetConverter<bool>();

    private static JsonConverter<bool>? s_booleanConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="byte"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<byte> ByteConverter => s_byteConverter ??= GetConverter<byte>();

    private static JsonConverter<byte>? s_byteConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="char"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<char> CharConverter => s_charConverter ??= GetConverter<char>();

    private static JsonConverter<char>? s_charConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="DateTime"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<DateTime> DateTimeConverter => s_dateTimeConverter ??= GetConverter<DateTime>();

    private static JsonConverter<DateTime>? s_dateTimeConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="DateTimeOffset"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<DateTimeOffset> DateTimeOffsetConverter =>
        s_dateTimeOffsetConverter ??= GetConverter<DateTimeOffset>();

    private static JsonConverter<DateTimeOffset>? s_dateTimeOffsetConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="DateOnly"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<DateOnly> DateOnlyConverter => s_dateOnlyConverter ??= GetConverter<DateOnly>();

    private static JsonConverter<DateOnly>? s_dateOnlyConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="TimeOnly"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<TimeOnly> TimeOnlyConverter => s_timeOnlyConverter ??= GetConverter<TimeOnly>();

    private static JsonConverter<TimeOnly>? s_timeOnlyConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="decimal"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<decimal> DecimalConverter => s_decimalConverter ??= GetConverter<decimal>();

    private static JsonConverter<decimal>? s_decimalConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="double"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<double> DoubleConverter => s_doubleConverter ??= GetConverter<double>();

    private static JsonConverter<double>? s_doubleConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="short"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<short> Int16Converter => s_int16Converter ??= GetConverter<short>();

    private static JsonConverter<short>? s_int16Converter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="int"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<int> Int32Converter => s_int32Converter ??= GetConverter<int>();

    private static JsonConverter<int>? s_int32Converter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="long"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<long> Int64Converter => s_int64Converter ??= GetConverter<long>();

    private static JsonConverter<long>? s_int64Converter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="sbyte"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<sbyte> SByteConverter => s_sbyteConverter ??= GetConverter<sbyte>();

    private static JsonConverter<sbyte>? s_sbyteConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="string"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<string?> StringConverter => s_stringConverter ??= GetConverter<string?>();

    private static JsonConverter<string?>? s_stringConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="TimeSpan"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<TimeSpan> TimeSpanConverter => s_timeSpanConverter ??= GetConverter<TimeSpan>();

    private static JsonConverter<TimeSpan>? s_timeSpanConverter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="ushort"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<ushort> UInt16Converter => s_uint16Converter ??= GetConverter<ushort>();

    private static JsonConverter<ushort>? s_uint16Converter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="uint"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<uint> UInt32Converter => s_uint32Converter ??= GetConverter<uint>();

    private static JsonConverter<uint>? s_uint32Converter;

    /// <summary>
    /// Returns a <see cref="JsonConverter{T}"/> instance that converts <see cref="ulong"/> values.
    /// </summary>
    /// <remarks>This API is for use by the output of the System.Text.Json source generator and should not be called directly.</remarks>
    public static JsonConverter<ulong> UInt64Converter => s_uint64Converter ??= GetConverter<ulong>();

    private static JsonConverter<ulong>? s_uint64Converter;

    internal static JsonConverter<T> GetConverter<T>()
    {
        //todo keep track and remove later.

        var jsonConverterType = typeof(JsonConverter<>).MakeGenericType(typeof(T));

        var prop = typeof(JsonMetadataServices)
            .GetProperties(BindingFlags.Static | BindingFlags.Public)
            .First(x => x.PropertyType == jsonConverterType);

        var instance = (JsonConverter<T>)prop.GetValue(null)! ?? throw new JsonException("Cannot retrieve to value");

        var type = instance.GetType();
        var internalConverterProp = type.GetProperty("IsInternalConverter", BindingFlags.Instance | BindingFlags.NonPublic)
                                    ?? throw new JsonException("Cannot convert to value");

        internalConverterProp.SetMethod!.Invoke(instance, [false]);
        return instance;
    }
}
