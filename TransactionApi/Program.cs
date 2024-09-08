using Domain.Features.Transaction.ProcessTransactions;
using Hangfire;
using Hangfire.Dashboard;
using MediatR;
using TransactionApi.Extensions;
using TransactionApi.Extensions.Hangfire;

namespace TransactionApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplicationDatabase();
        builder.Services.AddRepositories();
        builder.Services.AddMediator();
        

        builder.Services.AddHangfire();
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHangfireDashboard("/hangfire",new DashboardOptions
        {
            Authorization = Array.Empty<IDashboardAuthorizationFilter>()
        });
        app.MapHangfireDashboard();
        
        AddRecurringJobs(app);

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void AddRecurringJobs(WebApplication app)
    {
        RecurringJob.AddOrUpdate("ProcessTransactions",() => 
            app.Services.GetRequiredService<IMediator>().Send(new ProcessTransactionsRequest(),
                default), Cron.Minutely);
    }
}
