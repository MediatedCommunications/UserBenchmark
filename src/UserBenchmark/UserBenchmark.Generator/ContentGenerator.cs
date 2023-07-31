using CsvHelper.Configuration;

namespace UserBenchmark.Generator {

    public static class ContentGenerator {
        
        public static async Task GenerateAsync() {
            var Resources = new[] {
                "https://www.userbenchmark.com/resources/download/csv/CPU_UserBenchmarks.csv",
                "https://www.userbenchmark.com/resources/download/csv/GPU_UserBenchmarks.csv",
                "https://www.userbenchmark.com/resources/download/csv/SSD_UserBenchmarks.csv",
                "https://www.userbenchmark.com/resources/download/csv/HDD_UserBenchmarks.csv",
                "https://www.userbenchmark.com/resources/download/csv/RAM_UserBenchmarks.csv",
                "https://www.userbenchmark.com/resources/download/csv/USB_UserBenchmarks.csv",
            };

            var Values = new List<Device>();
            foreach (var Resource in Resources) { 
                var items = await DownloadAsync(Resource)
                    .ToListAsync()
                    ;
                Values.AddRange(items);
            }

            Values = (
                from x in Values
                orderby x.Type, x.Rank ascending
                select x
                ).ToList();

            var Root = System.IO.Path.GetFullPath(@"..\..\..\..\");

            var Json = $@"{Root}\UserBenchmark\Data\data.json";
            var Binn = $@"{Root}\UserBenchmark\Data\data.bin";

            UserBenchmark.Internal.Storage.DeviceJsonSerializer.ToJsonFile(Json, Values);
            UserBenchmark.Internal.Storage.DeviceBinarySerializer.ToBinaryFile(Binn, Values);

        }

        private static async IAsyncEnumerable<Device> DownloadAsync(string Url) { 

            using var C = new HttpClient();
            using var Response = await C.GetAsync(Url)
                .ConfigureAwait(false)
                ;

            using var ResponseStream = await Response.Content.ReadAsStreamAsync()
                .ConfigureAwait(false)
                ;

            using var ResponseReader = new StreamReader(ResponseStream);

            var Config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture) {
                HasHeaderRecord = true,

                Quote = char.MaxValue,
                Escape = char.MaxValue,

            };

            using var Reader = new CsvHelper.CsvReader(ResponseReader, Config);

            var query = await Reader.GetRecordsAsync<DeviceCsv>()
                .ToListAsync()
                .ConfigureAwait(false)
                ;

            foreach (var item in query) {

                var Type = item.Type?.ToUpperInvariant() switch {
                    "CPU" => DeviceType.CPU,
                    "RAM" => DeviceType.RAM,
                    "GPU" => DeviceType.GPU,
                    "USB" => DeviceType.USB,
                    "SSD" => DeviceType.SSD,
                    "HDD" => DeviceType.HDD,
                    _ => throw new NotImplementedException(),
                };


                var ret = new Device() {
                    Type = Type,
                    Benchmark = item.Benchmark,
                    Brand = item.Brand,
                    Model = item.Model,
                    PartNumber = item.PartNumber,
                    Rank = item.Rank,
                    Samples = item.Samples,
                    Url = item.Url,
                };
                yield return ret;

            }

        }

    }
}