using System.ComponentModel;
using DotNetLab.Model.Framework;
using DotNetLab.Services.Interfaces;
using ModelContextProtocol.Server;

namespace DotNetLab.MinimalAPI.Host.Endpoints;

[McpServerToolType]
public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var userGroup = endpoints.MapGroup("/users")
            .WithTags("Users");

        userGroup.MapGet("/", GetAllUsers)
            .WithName("GetAllUsers")
            .WithSummary("Get all users")
            .WithDescription("Retrieves a list of all users in the system")
            .Produces<IEnumerable<User>>(StatusCodes.Status200OK)
            .WithOpenApi();

        userGroup.MapGet("/{id:guid}", GetUserById)
            .WithName("GetUserById")
            .WithSummary("Get user by ID")
            .WithDescription("Retrieves a specific user by their unique identifier")
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

        userGroup.MapPost("/", CreateUser)
            .WithName("CreateUser")
            .WithSummary("Create a new user")
            .WithDescription("Creates a new user in the system. The Id is automatically generated.")
            .Accepts<CreateUserDto>("application/json")
            .Produces<User>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

        userGroup.MapPut("/{id:guid}", UpdateUser)
            .WithName("UpdateUser")
            .WithSummary("Update an existing user")
            .WithDescription("Updates an existing user with the provided information")
            .Accepts<User>("application/json")
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

        userGroup.MapDelete("/{id:guid}", DeleteUser)
            .WithName("DeleteUser")
            .WithSummary("Delete a user")
            .WithDescription("Removes a user from the system by their unique identifier")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
    }

    [McpServerTool(Name = "get_all_users"), Description("Get all users")]
    async private static Task<IResult> GetAllUsers(IUserService userService)
    {
        var users = await userService.GetAllUsersAsync();
        return Results.Ok(users);
    }

    [McpServerTool(Name = "get_user_by_id"), Description("Get user by ID")]
    async private static Task<IResult> GetUserById(Guid id, IUserService userService)
    {
        var user = await userService.GetUserByIdAsync(id);
        return user is not null ? Results.Ok(user) : Results.NotFound();
    }

    [McpServerTool(Name = "create_user"), Description("Create a new user")]
    async private static Task<IResult> CreateUser(CreateUserDto createUserDto, IUserService userService)
    {
        var createdUser = await userService.CreateUserAsync(createUserDto);
        return Results.Created($"/users/{createdUser.Id}", createdUser);
    }

    [McpServerTool(Name = "update_user"), Description("Update an existing user")]
    async private static Task<IResult> UpdateUser(Guid id, User user, IUserService userService)
    {
        var updatedUser = await userService.UpdateUserAsync(id, user);
        return updatedUser is not null ? Results.Ok(updatedUser) : Results.NotFound();
    }

    [McpServerTool(Name = "delete_user"), Description("Delete a user")]
    async private static Task<IResult> DeleteUser(Guid id, IUserService userService)
    {
        var deleted = await userService.DeleteUserAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
