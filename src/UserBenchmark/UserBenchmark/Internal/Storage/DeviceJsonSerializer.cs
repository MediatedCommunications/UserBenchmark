using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserBenchmark.Internal.Storage
{

    /// <summary>
    /// Read and write <see cref="Device"/>s in raw JSON form.
    /// </summary>
    public static partial class DeviceJsonSerializer
    {
        private static JsonSerializerOptions SerializerOptions()
        {
            var ret = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
                Converters =
                {
                    new JsonStringEnumConverter(),
                }
            };

            return ret;
        }

        public static Device[] FromJson(string Content)
        {
            return FromJson(default, Content);
        }

        public static Device[] FromJson(JsonSerializerOptions? Options, string Content)
        {
            var MyOptions = Options ?? SerializerOptions();
            var ret = System.Text.Json.JsonSerializer.Deserialize<Device[]>(Content, MyOptions)
                ?? Array.Empty<Device>()
                ;

            return ret;
        }

        public static Device[] FromJsonFile(string FileName)
        {
            return FromJson(default, FileName);
        }

        public static Device[] FromJsonFile(JsonSerializerOptions? Options, string FileName)
        {
            var Content = System.IO.File.ReadAllText(FileName);

            var ret = FromJson(Options, Content);

            return ret;
        }


        public static string ToJson(params Device[] Values)
        {
            return ToJson(default, Values.AsEnumerable());
        }

        public static string ToJson(JsonSerializerOptions? Options, params Device[] Values)
        {
            return ToJson(Options, Values.AsEnumerable());
        }

        public static string ToJson(IEnumerable<Device> Values)
        {
            return ToJson(default, Values);
        }

        public static string ToJson(JsonSerializerOptions? Options, IEnumerable<Device> Values)
        {
            var MyOptions = Options ?? SerializerOptions();

            var ret = System.Text.Json.JsonSerializer.Serialize(Values, MyOptions);

            return ret;
        }

        public static void ToJsonFile(string FileName, params Device[] Values)
        {
            ToJsonFile(default, FileName, Values.AsEnumerable());
        }

        public static void ToJsonFile(JsonSerializerOptions? Options, string FileName, params Device[] Values)
        {
            ToJsonFile(Options, FileName, Values.AsEnumerable());
        }

        public static void ToJsonFile(string FileName, IEnumerable<Device> Values)
        {
            ToJsonFile(default, FileName, Values);
        }

        public static void ToJsonFile(JsonSerializerOptions? Options, string FileName, IEnumerable<Device> Values)
        {
            var Content = ToJson(Options, Values);
            System.IO.File.WriteAllText(FileName, Content);
        }

    }
}
