﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Auth;

public static class Extensions
{

    public static void AddJwt(IServiceCollection services, IConfiguration configuration)
    {
        var options = new JwtOptions();
        var section = configuration.GetSection("jwt");
        section.Bind(options);
        services.Configure<JwtOptions>(configuration.GetSection("jwt"));
        services.AddSingleton<IJwtHandler, JwtHandler>();
        services.AddAuthentication()
        .AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = options.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey))
            };


        });

    }


}
