using MarketWizard.Application;
using MarketWizard.Data;
using MarketWizardApi;
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
    .AddApplicationServices(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseWebSockets();

app.UseCors("AllowFrontend");

app.MapGraphQL();

await app.RunWithGraphQLCommandsAsync(args);