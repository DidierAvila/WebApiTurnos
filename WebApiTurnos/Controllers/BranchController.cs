using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebApiTurnos.Core.Services.BranchSer;
using WebApiTurnos.Data.Models;
using WebApiTurnos.Dtos.Branch;

namespace WebApiTurnos.Controllers
{
    [ApiController]
    [Route("api/turnos/Branch")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _service;
        private readonly IMapper _mapper;

        public BranchController(IBranchService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_CREAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> Create([FromBody] BranchBase createRequest, CancellationToken cancellationToken)
        {
            if (createRequest != null)
            {
                Branch entity = _mapper.Map<BranchBase, Branch>(createRequest);
                Branch result = await _service.Create(entity, cancellationToken);
                return Created("/api/v1/projects/" + result.Id, new ApiResponse("AccionParticipacionOCMP created.", result, 201));
            }
            return NotFound(new ApiResponse("Branch no found.", null, 404));
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_VER, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> Read([FromRoute] int id, CancellationToken cancellationToken)
        {
            Branch response = await _service.Read(id, cancellationToken);
            if (response != null)
            {
                return Ok(new ApiResponse("AccionParticipacionOCMP found.", response, 200));
            }
            else
            {
                return NotFound(new ApiResponse("AccionParticipacionOCMP no found.", null, 404));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_ACTUALIZAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BranchGeneric updateRequest, CancellationToken cancellationToken)
        {
            Branch response = await _service.Read(id, cancellationToken);
            if (response == null)
            {
                return NotFound(new ApiResponse("Branch updated.", response, 404));
            }
            else
            {
                if (updateRequest.Id != id)
                {
                    return BadRequest(new ApiResponse("Branch Id and id doesn't match", response, 404));
                }

                Branch entity = _mapper.Map<BranchGeneric, Branch>(updateRequest, response);
                response = await _service.Update(entity, cancellationToken);
                return Ok(new ApiResponse("Branch updated.", response, 200));
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_ELIMINAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<ApiResponse> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return new ApiResponse("Branch deleted.", null, 204);
        }

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "UBPD_INVESTIGACION_ACCIONPARTICIPACIONOCMP_BUSCAR, UBPD_INVESTIGACION_SUPER_ADMINISTRADOR")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            ICollection<Branch> response = await _service.GetAll(cancellationToken);
            return Ok(new ApiResponse("Query done!", response, 200));
        }
    }
}
