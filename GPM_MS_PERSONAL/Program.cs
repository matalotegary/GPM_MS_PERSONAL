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
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region AppSettings
        // Commented out for now
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
            options.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly(nameof(application)))); // Ensure "application" is the correct project name containing the migrations
        #endregion

        #region Repositories
        builder.Services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();
        builder.Services.AddTransient<CodeDefaultUserDataSeed>();

        #endregion

        #region Services
        builder.Services.AddScoped<IMyActivitiesService, MyActivitiesService>();
        builder.Services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        #endregion

        #region Background Services
        // Commented out for now
        //builder.Services.AddHostedService<TimedHostedBackgroundService>();
        //builder.Services.AddHostedService<ConsumerBackgroundService>();
        //builder.Services.AddHostedService<QueuePollingBackgroundService>();
        #endregion

        var app = builder.Build();

        #region Start Migration and Seed Data
        ApplyMigrationAndSeed(app);
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

    private static void ApplyMigrationAndSeed(WebApplication webApplication)
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<PersonalInfoDbContext>();

                // Apply migrations
                context.Database.Migrate();

                // Resolve the seeding service
                var seed = services.GetRequiredService<CodeDefaultUserDataSeed>();

                // Seed data
                seed.InsertData(context);

                // Resolve and run the seeding services
                // Add here for more Data seeding
                var codeDefaultUserSeed = services.GetRequiredService<CodeDefaultUserDataSeed>();
                codeDefaultUserSeed.InsertData(context);

                //Sample
                //var anotherDataSeed = services.GetRequiredService<AnotherDataSeed>();
                //anotherDataSeed.InsertData(context);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred during migration and seeding: {ex.Message}");
            }
        }
    }

}
