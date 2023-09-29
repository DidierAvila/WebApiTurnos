using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebApiTurnos.Core.Services.UserSer;
using WebApiTurnos.Data.Models;
using WebApiTurnos.Dtos.User;

namespace WebApiTurnos.Controllers
{
    [ApiController]
    [Route("api/turnos/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_CREAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> Create([FromBody] UserBase createRequest, CancellationToken cancellationToken)
        {
            if (createRequest != null)
            {
                User entity = _mapper.Map<UserBase, User>(createRequest);
                User result = await _service.Create(entity, cancellationToken);
                return Created("/api/v1/projects/" + result.Id, new ApiResponse("User created.", result, 201));
            }
            return NotFound(new ApiResponse("User no found.", null, 404));
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_VER, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> Read([FromRoute] int id, CancellationToken cancellationToken)
        {
            User response = await _service.Read(id, cancellationToken);
            UserGeneric responseRead = _mapper.Map<User, UserGeneric>(response);
            if (response != null)
            {
                return Ok(new ApiResponse("User found.", responseRead, 200));
            }
            else
            {
                return NotFound(new ApiResponse("User no found.", null, 404));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_ACTUALIZAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserGeneric updateRequest, CancellationToken cancellationToken)
        {
            User response = await _service.Read(id, cancellationToken);
            if (response == null)
            {
                return NotFound(new ApiResponse("User updated.", response, 404));
            }
            else
            {
                if (updateRequest.Id != id)
                {
                    return BadRequest(new ApiResponse("User Id and id doesn't match", response, 404));
                }

                User entity = _mapper.Map<UserGeneric, User>(updateRequest, response);
                response = await _service.Update(entity, cancellationToken);
                return Ok(new ApiResponse("User updated.", response, 200));
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_ELIMINAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<ApiResponse> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return new ApiResponse("User deleted.", null, 204);
        }

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_BUSCAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            ICollection<User> response = await _service.GetAll(cancellationToken);
            return Ok(new ApiResponse("Query done!", response, 200));
        }
    }
}
