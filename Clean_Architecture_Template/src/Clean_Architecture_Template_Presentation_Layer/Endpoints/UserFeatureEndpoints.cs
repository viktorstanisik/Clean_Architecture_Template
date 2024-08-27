namespace Clean_Architecture_Template_Presentation_Layer.Endpoints;

public class UserFeatureEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create-user",
                async (CreateUserCommand command,
                    IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return Results.Ok(result);
                })
            .WithName("CreateUser");


        //yet to be implemented
        app.MapPost("/login-user", async (string userLogin) =>
            {
                //implement logic 
            })
            .WithName("VerifyUser");
    }
}