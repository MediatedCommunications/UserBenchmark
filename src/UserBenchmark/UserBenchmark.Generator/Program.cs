namespace UserBenchmark.Generator {
    public static class Program {
        public static async Task Main(string[] args) {
            await ContentGenerator.GenerateAsync()
                .ConfigureAwait(false)
                ;

        }
    }
}