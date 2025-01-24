using Morganas.Middleware;
using Morganas.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

string umbracoHost = builder.Configuration.GetSection("Umbraco").GetValue<string>("Host")!;
builder.Services.Configure<SecurityCMS>(builder.Configuration.GetSection("Umbraco").GetSection("Security"));

builder.Services.AddScoped<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient("UmbracoAuthClient", client =>
{
    client.BaseAddress = new Uri(umbracoHost);

});
builder.Services.AddHttpClient("UmbracoApiClient", client =>
{
    client.BaseAddress = new Uri(umbracoHost);
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

WebApplication app = builder.Build();

app.MapDefaultEndpoints();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
