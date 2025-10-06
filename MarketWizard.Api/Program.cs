using MarketWizard.Application;
using MarketWizard.Data;
using MarketWizard.Finnhub;
using MarketWizard.User;
using MarketWizardApi;
using MarketWizardApi.Extensions;
using MarketWizardApi.Schema;

var builder = WebApplication.CreateSlimBuilder(args);

// Add CORS policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
});

builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddFiltering()
    .AddProjections()
    .AddSorting()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

builder.Services.AddPersistenceServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddUserServices(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddHealthChecks();

var app = builder.Build();
app.AddRestEndpoints();

app.UseWebSockets();

app.UseCors("AllowFrontend");

app.MapGraphQL();
app.MapHealthChecks("/health");

await app.RunWithGraphQLCommandsAsync(args);