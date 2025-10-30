using BugStore;
using BugStore.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.MapEndpoints();

await app.RunAsync();
