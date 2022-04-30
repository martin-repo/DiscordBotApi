// -------------------------------------------------------------------------------------------------
// <copyright file="Utf8JsonReaderExtensions.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Utilities
{
    using System.Globalization;
    using System.Text;
    using System.Text.Json;

    internal static class Utf8JsonReaderExtensions
    {
        public static string ReadObjectAsJson(ref this Utf8JsonReader reader)
        {
            var jsonBuilder = new StringBuilder();

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                return ParseSingleValue(ref reader);
            }

            jsonBuilder.Append('{');
            reader.Read();

            var startDepth = reader.CurrentDepth;
            var lastTokenType = JsonTokenType.None;

            var depthItems = new Stack<Depth>();
            depthItems.Push(new Depth());

            while (reader.CurrentDepth >= startDepth)
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.StartObject:
                        switch (lastTokenType)
                        {
                            case JsonTokenType.EndObject when depthItems.Peek().IsArray:
                                jsonBuilder.Append(',');
                                break;
                        }

                        jsonBuilder.Append('{');
                        depthItems.Push(new Depth());
                        break;
                    case JsonTokenType.EndObject:
                        jsonBuilder.Append('}');
                        depthItems.Pop();
                        break;
                    case JsonTokenType.StartArray:
                        jsonBuilder.Append('[');
                        depthItems.Push(new Depth { IsArray = true });
                        break;
                    case JsonTokenType.EndArray:
                        jsonBuilder.Append(']');
                        depthItems.Pop();
                        break;

                    case JsonTokenType.PropertyName:
                        switch (lastTokenType)
                        {
                            case JsonTokenType.EndObject:
                            case JsonTokenType.EndArray:
                            case JsonTokenType.String:
                            case JsonTokenType.Number:
                            case JsonTokenType.True:
                            case JsonTokenType.False:
                            case JsonTokenType.Null:
                                jsonBuilder.Append(',');
                                break;
                        }

                        jsonBuilder.Append($@"""{reader.GetString()!}"":");
                        break;

                    case JsonTokenType.String:
                        var depthItem = depthItems.Peek();
                        if (depthItem.IsArray && depthItem.IsValueAdded)
                        {
                            jsonBuilder.Append(',');
                        }

                        jsonBuilder.Append(ParseSingleValue(ref reader));
                        depthItem.IsValueAdded = true;
                        break;
                    case JsonTokenType.Number:
                    case JsonTokenType.True:
                    case JsonTokenType.False:
                    case JsonTokenType.Null:
                        jsonBuilder.Append(ParseSingleValue(ref reader));
                        break;
                }

                lastTokenType = reader.TokenType;
                reader.Read();
            }

            if (reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException("Not at end of object.");
            }

            // Leave reader on the EndObject at the same depth, do not advance further
            jsonBuilder.Append('}');

            var json = jsonBuilder.ToString();
            if (json == null)
            {
                throw new JsonException("Failed to read JSON from object.");
            }

            return json;
        }

        private static string ParseSingleValue(ref Utf8JsonReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    var stringValue = reader.GetString()!;
                    var encodedString = JsonSerializer.Serialize(stringValue);
                    return encodedString;
                case JsonTokenType.Number:
                    return reader.GetInt32().ToString(CultureInfo.InvariantCulture);
                case JsonTokenType.True:
                    return "true";
                case JsonTokenType.False:
                    return "false";
                case JsonTokenType.Null:
                    return "null";
                default:
                    return "";
            }
        }

        private class Depth
        {
            public bool IsArray { get; init; }

            public bool IsValueAdded { get; set; }
        }
    }
}