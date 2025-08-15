using MarketWizardApi;
using MarketWizardApi.Data;

var builder = WebApplication.CreateSlimBuilder(args);

// Add CORS policies
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSingleton<IDatastore, Datastore>();

builder.Services
    .AddGraphQLServer()
    .AddFiltering()
    .AddProjections()
    .AddSorting()
    .AddQueryType<Query>();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapGraphQL();

await app.RunWithGraphQLCommandsAsync(args);