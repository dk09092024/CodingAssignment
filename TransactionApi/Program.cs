using Domain.Features.Transaction.ProcessTransactions;
using Hangfire;
using MediatR;
using TransactionApi.Extensions;
using TransactionApi.Extensions.Hangfire;

namespace TransactionApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

        app.UseHangfireDashboard();
        app.MapHangfireDashboard();
        
        RecurringJob.AddOrUpdate("ProcessTransactions",() => 
            app.Services.GetRequiredService<IMediator>().Send(new ProcessTransactionsRequest(),
                default), Cron.Minutely);

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}