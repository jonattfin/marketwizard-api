using MarketWizard.Data;
using MarketWizardApi;

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
    .AddFiltering()
    .AddProjections()
    .AddSorting()
    .AddQueryType<Query>();

builder.Services.AddPersistenceServices();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapGraphQL();

await app.RunWithGraphQLCommandsAsync(args);