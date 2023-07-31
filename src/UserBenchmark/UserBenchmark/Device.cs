using UserBenchmark.Internal.Diagnostics;

namespace UserBenchmark {
    public record Device : DisplayRecord { 
        public DeviceType Type { get; init; }
        public string? Url { get; init; }
        public string? PartNumber { get; init; }
        public string? Brand { get; init; }
        public string? Model { get; init; }
        public long Rank { get; init; }
        public decimal Benchmark { get; init; }
        public long Samples { get; init; }

        public override string? GetDebuggerDisplay() {
            return $@"#{Rank}: {Brand} {Model}";
        }

    }

    
}