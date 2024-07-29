using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Todo;
using Todo.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
string ApiUrl = builder.Configuration["ApiUrl"];


builder.Services.AddHttpClient( "TodoApiClient",client =>
{
    client.BaseAddress = new Uri(ApiUrl);
});

builder.Services.AddTransient<ITodoApiRepository, TodoApiRepository>();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todos BFF",
        Version = "v1"
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpoints(typeof(Program).Assembly);


var app = builder.Build();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

var versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoint(versionedGroup);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoBff");
            options.RoutePrefix = string.Empty;
        }
    );
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();