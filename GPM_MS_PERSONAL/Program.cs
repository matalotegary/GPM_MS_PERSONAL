using application.Interface.MyActivities;
using application.Interface.PersonalInfo;
using application.Services.MyActivities;
using application.Services.PersonalInfo;
using domain.Interfaces;
using infrastructure;
using infrastructure.Repositories.PersonalInfo;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        #region AppSettings
        //builder.Services.Configure<StorageAccountConfiguration>(configuration.GetSection(nameof(StorageAccountConfiguration)));
        //builder.Services.Configure<KeyVaultConfiguration>(configuration.GetSection(nameof(KeyVaultConfiguration)));
        //var keyVaultUri = new Uri(builder.Configuration["KeyVaultConfiguration:VaultName"]!);
        //var credential = new ClientSecretCredential(
        //    "3afbd202-e0a6-405d-a0ed-557671773326",
        //    "e38e030a-a742-4858-a97e-0dbed8ded51c",
        //    "8xV8Q~AbF1FzZDvrdvk4H.jz6vSFp19gdl65LaJy"
        //    );
        //builder.Configuration.AddAzureKeyVault(keyVaultUri, credential);
        #endregion

        #region DbContext
        var connectionString = configuration["Database:ConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'Database:ConnectionString' is missing in configuration.");
        }

        builder.Services.AddDbContext<PersonalInfoDbContext>(options =>
            options.UseSqlServer(configuration["Database:ConnectionString"],
        sqlOptions => sqlOptions.MigrationsAssembly(nameof(application)))); // Ensure "DataAccess" is the correct project name containing the migrations

        /*builder.Services.AddDbContext<PersonalInfoDbContext>(options =>
                options.UseSqlServer(builder.Configuration["Database:ConnectionString"].ToString(),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                    ))
            );*/
        #endregion

        #region Repositories
        builder.Services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
        #endregion

        #region Services
        builder.Services.AddScoped<IMyActivitiesService, MyActivitiesService>();
        builder.Services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        #endregion
        #region Background Services
        //builder.Services.AddHostedService<TimedHostedBackgroundService>();
        //builder.Services.AddHostedService<ConsumerBackgroundService>();
        //builder.Services.AddHostedService<QueuePollingBackgroundService>();
        #endregion

        var app = builder.Build();

        #region Start Migration
        ApplyMigration(app);
        #endregion

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ApplyMigration(WebApplication webApplication)
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<PersonalInfoDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine(ex.Message);
            }
        }
    }
}