using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTurnos.Data.Models;
using WebApiTurnos.Data.Repositories.BranchRep;

namespace WebApiTurnos.Core.Services.SecuritySer
{
    public class SecurityService : ISecurityService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _repository;
        private readonly IConfiguration _configuration;

        public SecurityService(IMapper mapper, IBranchRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<Branch> Login(int id, CancellationToken cancellationtoken)
        {
            return await _repository.Read(id, cancellationtoken);
        }

        /// <summary>
        /// Genera el token para un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public string GenerateToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        /// <summary>
        /// Consulta el token por usuario
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<string> GetToken(int idUser)
        {
            return "token";
        }
    }
}
