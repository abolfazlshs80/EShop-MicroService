namespace Auth;

public interface IJwtHandler
{
    JsonWebToken Create(Guid userId);
}
