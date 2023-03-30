using CommandGQL.Data;
using CommandGQL.Models;

namespace CommandGQL.GraphQL.Commands;

public record AddCommandInput(string HowTo, string CommandLine, int PlatformId);

public class AddCommandInputType : InputObjectType<AddCommandInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<AddCommandInput> descriptor)
    {
        descriptor.Description("Represents the input to add for a command.");

        descriptor
            .Field(c => c.HowTo)
            .Description("Represents the how-to for the command.");
        descriptor
            .Field(c => c.CommandLine)
            .Description("Represents the command line.");
        descriptor
            .Field(c => c.PlatformId)
            .Description("Represents the unique ID of the platform which the command belongs.");

        base.Configure(descriptor);
    }
}

public record AddCommandPayload(Command command);

public class AddCommandPayloadType : ObjectType<AddCommandPayload>
{
    protected override void Configure(IObjectTypeDescriptor<AddCommandPayload> descriptor)
    {
        descriptor.Description("Represents the payload to return for an added command.");

        descriptor
            .Field(c => c.command)
            .Description("Represents the added command.");

        base.Configure(descriptor);
    }
}

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command.");

        descriptor
            .Field(c => c.Id)
            .Description("Represents the unique ID for the command.");

        descriptor
            .Field(c => c.HowTo)
            .Description("Represents the how-to for the command.");

        descriptor
            .Field(c => c.CommandLine)
            .Description("Represents the command line.");

        descriptor
            .Field(c => c.PlatformId)
            .Description("Represents the unique ID of the platform which the command belongs.");

        descriptor
            .Field(c => c.Platform)
            .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the platform to which the command belongs.");

    }

    private class Resolvers
    {
        public Platform GetPlatform(Command command, AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
        }
    }
}