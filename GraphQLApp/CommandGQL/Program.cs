using CommandGQL.Data;
using CommandGQL.GraphQL;
using CommandGQL.GraphQL.Commands;
using CommandGQL.GraphQL.Platforms;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

configuration.SetBasePath(Directory.GetCurrentDirectory());


//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
//});

//AddPooledDbContextFactory handles multithreading since DbContext is not multithreaded
builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<PlatformType>()
                .AddType<AddPlatformInputType>()
                .AddType<AddPlatformPayloadType>()
                .AddType<CommandType>()
                .AddType<AddCommandInputType>()
                .AddType<AddCommandPayloadType>()
                .AddFiltering()
                .AddSorting()
                // .AddProjections()
                .RegisterDbContext<AppDbContext>(DbContextKind.Pooled);




var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

//app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
//{
//    GraphQLEndPoint = "/graphql",
//    Path = "/graphql-voyager"
//});

app.UseGraphQLVoyager("/graphql-voyager", new GraphQL.Server.Ui.Voyager.VoyagerOptions
{
    GraphQLEndPoint = "/graphql",

});

app.Run();
