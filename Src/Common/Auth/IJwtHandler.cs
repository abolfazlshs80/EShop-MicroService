﻿namespace Auth;

public interface IJwtHandler
{
    JsonWebToken Create(Int64 userId);
}
