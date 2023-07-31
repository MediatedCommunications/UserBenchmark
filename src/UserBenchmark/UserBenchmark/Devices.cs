using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserBenchmark {
    public static class Devices {
        public static ImmutableArray<Device> All { get; }
        public static ILookup<DeviceType, Device> ByType { get; }

        static Devices() {
            var raw = UserBenchmark.Internal.Resources.data;
            var ret = UserBenchmark.Internal.Storage.DeviceBinarySerializer
                .FromBinary(raw)
                .ToImmutableArray()
                ;

            All = ret;
            ByType = All.ToLookup(x => x.Type);
        }
    }
}
