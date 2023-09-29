using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebApiTurnos.Core.Services.ShiftSer;
using WebApiTurnos.Dtos.Shift;
using WebApiTurnos.Data.Models;
using AutoMapper;

namespace WebApiTurnos.Controllers
{
    [ApiController]
    [Route("api/turnos/Turno")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _service;
        private readonly IMapper _mapper;

        public ShiftController(IShiftService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ShiftBase createRequest, CancellationToken cancellationToken)
        {
            if (createRequest != null)
            {
                Shift result = _mapper.Map<ShiftBase, Shift>(createRequest);
                result = await _service.Create(result, cancellationToken);
                if (!string.IsNullOrEmpty(result.Error))
                {
                    return NotFound(new ApiResponse(result.Error, null, 404));
                }
                return Created("/api/v1/projects/" + result.Id, new ApiResponse("AccionParticipacionOCMP created.", result, 201));
            }
            return NotFound(new ApiResponse("Shift no found.", null, 404));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read([FromRoute] int id, CancellationToken cancellationToken)
        {
            Shift response = await _service.Read(id, cancellationToken);
            if (response != null)
            {
                return Ok(new ApiResponse("Shift found.", response, 200));
            }
            else
            {
                return NotFound(new ApiResponse("Shift no found.", null, 404));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ShiftGeneric updateRequest, CancellationToken cancellationToken)
        {
            Shift response = await _service.Read(id, cancellationToken);
            if (response == null)
            {
                return NotFound(new ApiResponse("Shift updated.", response, 404));
            }
            else
            {
                if (updateRequest.Id != id) 
                {
                    return BadRequest(new ApiResponse("Shift Id and id doesn't match", response, 404));
                }

                Shift entity = _mapper.Map<ShiftGeneric, Shift>(updateRequest, response);
                response = await _service.Update(entity, cancellationToken);
                return Ok(new ApiResponse("Shift updated.", response, 200));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return new ApiResponse("Shift deleted.", null, 204);
        }

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] ShiftGeneric searchRequest, CancellationToken cancellationToken)
        {
            Shift entity = _mapper.Map<ShiftGeneric, Shift>(searchRequest);
            ICollection<Shift> response = await _service.Search(entity, cancellationToken);
            return Ok(new ApiResponse("Query done!", response, 200));
        }

        [HttpPut("/ActivateShiftId/{shiftId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActivateShift([FromRoute] int shiftId, CancellationToken cancellationToken)
        {
            Shift response = await _service.ActivateShift(shiftId, cancellationToken);
            if (response == null)
            {
                return BadRequest(new ApiResponse("Shift no found.", response, 404));
            }
            if (!string.IsNullOrEmpty(response.Error))
            {
                return Ok(new ApiResponse("Shift activate.", response, 200));
            }
            else
            {
                return BadRequest(new ApiResponse(response.Error, response, 404));
            }
        }

        [HttpPut("/CancelShift/{id}")]
        public async Task<ApiResponse> CancelShift([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _service.CancelShift(id, cancellationToken);
            return new ApiResponse("Shift Cancel.", null, 204);
        }
    }
}
