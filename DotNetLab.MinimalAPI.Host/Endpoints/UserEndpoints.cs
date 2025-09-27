using DotNetLab.Model.Framework;
using DotNetLab.Services.Interfaces;

namespace DotNetLab.MinimalAPI.Host.Endpoints;

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

        userGroup.MapGet("/{id:int}", GetUserById)
            .WithName("GetUserById")
            .WithSummary("Get user by ID")
            .WithDescription("Retrieves a specific user by their unique identifier")
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

        userGroup.MapPost("/", CreateUser)
            .WithName("CreateUser")
            .WithSummary("Create a new user")
            .WithDescription("Creates a new user in the system")
            .Accepts<User>("application/json")
            .Produces<User>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

        userGroup.MapPut("/{id:int}", UpdateUser)
            .WithName("UpdateUser")
            .WithSummary("Update an existing user")
            .WithDescription("Updates an existing user with the provided information")
            .Accepts<User>("application/json")
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

        userGroup.MapDelete("/{id:int}", DeleteUser)
            .WithName("DeleteUser")
            .WithSummary("Delete a user")
            .WithDescription("Removes a user from the system by their unique identifier")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
    }

    async private static Task<IResult> GetAllUsers(IUserService userService)
    {
        var users = await userService.GetAllUsersAsync();
        return Results.Ok(users);
    }

    async private static Task<IResult> GetUserById(int id, IUserService userService)
    {
        var user = await userService.GetUserByIdAsync(id);
        return user is not null ? Results.Ok(user) : Results.NotFound();
    }

    async private static Task<IResult> CreateUser(User user, IUserService userService)
    {
        var createdUser = await userService.CreateUserAsync(user);
        return Results.Created($"/users/{createdUser.Id}", createdUser);
    }

    async private static Task<IResult> UpdateUser(int id, User user, IUserService userService)
    {
        var updatedUser = await userService.UpdateUserAsync(id, user);
        return updatedUser is not null ? Results.Ok(updatedUser) : Results.NotFound();
    }

    async private static Task<IResult> DeleteUser(int id, IUserService userService)
    {
        var deleted = await userService.DeleteUserAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
