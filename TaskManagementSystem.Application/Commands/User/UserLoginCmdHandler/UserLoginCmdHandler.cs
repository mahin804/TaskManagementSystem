using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.User.UserCmd;
using TaskManagementSystem.Application.Response;
using TaskManagementSystem.Authoriz;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Commands.User.UserLoginCmdHandler
{
    public class UserLoginCmdHandler : IRequestHandler<UserLoginCmd, IApiResponse>
    {
        public async Task<IApiResponse> Handle(UserLoginCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var user = DummyUsers.Users
                    .FirstOrDefault(u =>
                        u.Username == request.UserName &&
                        u.Password == request.Password);

                if (user == null)
                    return new FetchApiExeResult<TaskItem>
                    {
                        ResultType = ResultType.Error,
                        Message = "Invalid credentials",
                        Result = null
                    };


                var token = GenerateJwtToken("THIS_IS_A_VERY_LONG_SECRET_KEY_1234567890",user.Username,user.Role);

                var responseData = new UserLoginResponse
                {
                    Token = token,
                    Username = user.Username,
                    Role = user.Role
                };

                var response = new FetchApiExeResult<UserLoginResponse>
                {
                    ResultType = ResultType.Success,
                    Message = "Login successful",
                    Result = new ResultData<UserLoginResponse>
                    {
                        Data = responseData
                    }
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
        private string GenerateJwtToken(string key, string username, string role = "User", int expmin = 20)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
             new Claim(ClaimTypes.NameIdentifier, username),
             new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expmin),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
