using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Core.Services.SecuritySer
{
    public interface ISecurityService
    {
        Task<Branch> Login(int id, CancellationToken cancellationtoken);
        string GenerateToken(string idUsuario);
    }
}