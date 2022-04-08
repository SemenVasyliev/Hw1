using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hw1.Tests.Tools
{
    internal static class KeyValueExtensions
    {
        public static Dictionary<string, string> KeyValueToDictionary(this string text, char[] propSeparator = null)
        {
            propSeparator ??= new char[] { '\n', '\r' };
            return (text ?? throw new ArgumentNullException(nameof(text)))
                .Split(propSeparator, StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Split('=', 2))
                .Where(pair => pair.Length > 0)
                .ToDictionary(pair => pair[0]?.Trim(), pair => pair.Length > 1 ? pair[1]?.Trim() : null);
        }

        public static T KeyValueToObject<T>(this string text, char[] propSeparator = null)
        {
            var source = text.KeyValueToDictionary(propSeparator);
            var json = JsonSerializer.Serialize(source);
            return JsonSerializer.Deserialize<T>(
                json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                });
        }
    }
}
