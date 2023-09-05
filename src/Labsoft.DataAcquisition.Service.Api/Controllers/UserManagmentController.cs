using AssessmentProject.Api.DTOs;
using AssessmentProject.Application.CQRS.Commands.EquipmentTypes.CreateEquipmentType;
using AssessmentProject.Application.CQRS.Query.GetPersonList;
using AssessmentProject.Domain.Entity;
using AssessmentProject.Service.Api.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AssessmentProject.Service.Api.Controllers
{
    [ApiController]
    [Route($"api/v1/[controller]")]
    public class UserManagmentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserManagmentController> _logger;

        public UserManagmentController(
            IMediator mediator,
            ILogger<UserManagmentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Login", Name = "Login Person and get JWT Token")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(LoginRequestDTO))]
        public async Task<IActionResult> LoginPerson([FromBody] LoginRequestDTO loginRequestDTO, CancellationToken cancellationToken)
        {
            try
            {
                var request = new LoginPersonCommandRequest(loginRequestDTO.Email, loginRequestDTO.Password);
                var response = await _mediator.Send(request);
                return StatusCode(
                    statusCode: (int)HttpStatusCode.OK,
                    value: response.JwtToken);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    value: $"Bad request message: {ex.Message}");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Register", Name = "Register Person")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.Created, type: typeof(PersonDto))]
        public async Task<IActionResult> RegisterPerson([FromBody] PersonDto personDto, CancellationToken cancellationToken)
        {
            try
            {
                var personEntity = new PersonEntity
                (
                    Guid.NewGuid(), 
                    personDto.PersonType, 
                    personDto.Document, 
                    personDto.Nome, 
                    personDto.Apelido, 
                    personDto.EnderecoCadastro, 
                    personDto.Email, 
                    personDto.Qualification, 
                    personDto.Role, 
                    personDto.Password
                );
                var request = new RegisterPersonCommandRequest(personEntity);
                var response = await _mediator.Send(request);
                return StatusCode(
                    statusCode: (int)HttpStatusCode.Created, value: response.Person);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    value: $"Bad request message: {ex.Message}");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("Update", Name = "Update Person")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.NoContent, type: typeof(PersonDto))]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonWithIdDto personDto, CancellationToken cancellationToken)
        {
            try
            {
                var personEntity = new PersonEntity
                (
                    personDto.Id,
                    personDto.PersonType,
                    personDto.Document,
                    personDto.Nome,
                    personDto.Apelido,
                    personDto.EnderecoCadastro,
                    personDto.Email,
                    personDto.Qualification,
                    personDto.Role,
                    personDto.Password
                );
                var request = new UpdatePersonCommandRequest(personEntity);
                var response = await _mediator.Send(request);
                return StatusCode(
                    statusCode: (int)HttpStatusCode.Created,
                    value: response.Person
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    value: $"Bad request message: {ex.Message}");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("Deactivate/:personId", Name = "Deactivate Person")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.Created, type: typeof(PersonDto))]
        public async Task<IActionResult> DeactivatePerson(Guid personId)
        {
            try
            {
                var request = new DeactivatePersonCommandRequest(personId);
                var response = await _mediator.Send(request);
                return StatusCode(
                    statusCode: (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    value: $"Bad request message: {ex.Message}");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("PersonList", Name = "Get Person List")]
        [Produces("application/json")]
        [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(PersonDto))]
        public async Task<IActionResult> PersonList()
        {
            try
            {
                var request = new GetPersonListQueryRequest();
                var response = await _mediator.Send(request);
                return StatusCode(
                    statusCode: (int)HttpStatusCode.OK,
                    value: response.Persons
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    value: $"Bad request message: {ex.Message}");
            }
        }
    }
}
