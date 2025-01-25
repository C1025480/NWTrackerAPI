using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using NWTrackerAPI.Data;
using System;
using NWTrackerAPI.Processors;
using NWTrackerAPI.Processors.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register services with the default DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext
builder.Services.AddDbContext<APIContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Allow all origins
              .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader(); // Allow any headers
    });
});


// **Configure Autofac as the DI container**
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        // Get all the types from the assembly where your interfaces and implementations reside
        var assembly = typeof(ICreateProject).Assembly; // Assuming your interfaces are in the same assembly

        // Register types dynamically by scanning the assembly
        containerBuilder.RegisterAssemblyTypes(assembly)
            .Where(t => t.IsClass && !t.IsAbstract) // Only consider concrete classes
            .AsImplementedInterfaces(); // Automatically register all interfaces the class implements
    });

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
