

using BankingAccountApi.Extensions;
using Infrastructure;

namespace BankingAccountApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddRepositories();
        builder.Services.AddMediator();
        
        builder.Services.AddApplicationDatabase();
        
        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
            db.Database.EnsureCreated();
        }

        app.MapControllers();

        app.Run();
    }
}