using Domain.Common;

namespace Presentation.Endpoints;

public class UserFeatureEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/create-user",
                async (CreateUserCommand command,
                    IMediator mediator) =>
                {
                    Result<bool> result = await mediator.Send(command);

                    return result;
                })
            .WithName("CreateUser");

        app.MapPost("/verify-email-hash", async () =>
        {
            //some logic
        }); 


        //yet to be implemented
        app.MapPost("/login-user", async (string userLogin) =>
            {
                //implement logic 
            })
            .WithName("VerifyUser");
    }
}