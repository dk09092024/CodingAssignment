using Hangfire;

namespace TransactionApi.Extensions.Hangfire;

public static class HangfireExtension 
{
    public static void AddHangfire(this IServiceCollection services)
    {
        services.AddHangfireServer(
            provider =>
            {
                provider.ServerName = "TransactionServer";
                provider.WorkerCount = 5;
            });
        services.AddHangfire(cfg =>
        {
            cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage();
        });
    }
}