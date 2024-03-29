﻿using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WeatherForecastAPI.Infrastructure.Encryption
{
    public class ECDsSigner
    {
        private readonly ECDsa _key;

        public ECDsSigner()
        {
            _key = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                D = Base64UrlEncoder.DecodeBytes("eQWpRW_kd6N3fLzb8ngLgAACjNNvWxWHnJa9rQ6_FvY"),
                Q = new ECPoint
                {
                    X = Base64UrlEncoder.DecodeBytes("28QJtSh28OVqJ-mUEuAPVfwV3iIkX9AVrfNTu2o8SfY"),
                    Y = Base64UrlEncoder.DecodeBytes("9jwkgqnMYPGIm-LSr8q5n60EiqvXL8DFgSiheLII9JQ")
                }
            });
        }

        public string SignJwt(string audience, TimeSpan issueFrom, TimeSpan issueTo)
        {
            var now = DateTime.UtcNow;
            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "zyxHome",
                Audience = audience,
                NotBefore = now.Add(issueFrom),
                Expires = now.Add(issueTo),
                IssuedAt = now,
                Claims = new Dictionary<string, object> { { "sub", "SwaggerPermission" } },
                SigningCredentials = new SigningCredentials(new ECDsaSecurityKey(_key), "ES256")
            });
            return token;
        }
    }
}
