﻿using System.IdentityModel.Tokens.Jwt;

namespace BlogEngine.Security
{
    public class JwtToken
    {
        private readonly JwtSecurityToken _token;

        internal JwtToken(JwtSecurityToken token)
        {
            _token = token;
        }

        public DateTime ValidTo => _token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(_token);
    }
}
