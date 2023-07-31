using UserBenchmark.Internal.Diagnostics;

namespace UserBenchmark.Generator {
    public class DeviceCsv : DisplayClass {
        [CsvHelper.Configuration.Attributes.Name("Type")]
        public string? Type { get; init; }

        [CsvHelper.Configuration.Attributes.Name("Part Number")]
        public string? PartNumber { get; init; }

        [CsvHelper.Configuration.Attributes.Name("Brand")]
        public string? Brand { get; init; }

        [CsvHelper.Configuration.Attributes.Name("Model")]
        public string? Model { get; init; }

        [CsvHelper.Configuration.Attributes.Name("Rank")]
        public long Rank { get; init; }

        [CsvHelper.Configuration.Attributes.Name("Benchmark")]
        public decimal Benchmark { get; init; }

        [CsvHelper.Configuration.Attributes.Name("Samples")]
        public long Samples { get; init; }

        [CsvHelper.Configuration.Attributes.Name("URL")]
        public string? Url { get; init; }

        public override string? GetDebuggerDisplay() {
            return $@"#{Rank}: {Brand} {Model}";
        }

    }
}