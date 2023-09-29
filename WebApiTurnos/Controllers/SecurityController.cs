using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebApiTurnos.Core.Services.UserSer;
using WebApiTurnos.Data.Models;
using WebApiTurnos.Dtos.Branch;
using WebApiTurnos.Dtos.Security;

namespace WebApiTurnos.Controllers
{
    [ApiController]
    [Route("api/turnos/Security")]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public SecurityController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] Login createRequest, CancellationToken cancellationToken)
        {
            if (createRequest != null)
            {
                User entity = _mapper.Map<Login, User>(createRequest);
                var result = await _service.Login(entity, cancellationToken);
                return Created("/api/v1/projects/" + result, new ApiResponse("AccionParticipacionOCMP created.", result, 201));
            }
            return NotFound(new ApiResponse("Branch no found.", null, 404));
        }
    }
}
