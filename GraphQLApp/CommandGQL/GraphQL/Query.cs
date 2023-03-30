using CommandGQL.Data;
using CommandGQL.Models;

namespace CommandGQL.GraphQL;

public class Query
{
    //[UseDbContext(typeof(AppDbContext))]

    //  [UseProjection] // Not required since we have Provied decriptors

    [UseFiltering]
    [UseSorting]
    public IQueryable<Platform> GetPlatforms(AppDbContext context)
    {
        return context.Platforms;
    }

    //  [UseProjection]

    [UseFiltering]
    [UseSorting]
    public IQueryable<Command> GetCommand(AppDbContext context)
    {
        return context.Commands;
    }
}
