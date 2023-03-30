using CommandGQL.Data;
using CommandGQL.Models;

namespace CommandGQL.GraphQL.Platforms;

public record AddPlatformInput(string Name);

public class AddPlatformInputType : InputObjectType<AddPlatformInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<AddPlatformInput> descriptor)
    {
        descriptor.Description("Represents the input to add for a platform.");

        descriptor
            .Field(p => p.Name)
            .Description("Represents the name for the platform.");

        base.Configure(descriptor);
    }
}

public record AddPlatformPayload(Platform platform);

public class AddPlatformPayloadType : ObjectType<AddPlatformPayload>
{
    protected override void Configure(IObjectTypeDescriptor<AddPlatformPayload> descriptor)
    {
        descriptor.Description("Represents the payload to return for an added platform.");

        descriptor
            .Field(p => p.platform)
            .Description("Represents the added platform.");

        base.Configure(descriptor);
    }
}

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Represents any software or service that has a command line interface.");

        descriptor
            .Field(p => p.Id)
            .Description("Represents the unique ID for the platform.");

        descriptor
            .Field(p => p.Name)
            .Description("Represents the name for the platform.");

        descriptor
            .Field(p => p.LicenseKey).Ignore();

        descriptor
            .Field(p => p.Commands)
            .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the list of available commands for this platform.");
    }

    private class Resolvers
    {
        public IQueryable<Command> GetCommands(Platform platform, AppDbContext context)
        {
            return context.Commands.Where(p => p.PlatformId == platform.Id);
        }
    }
}