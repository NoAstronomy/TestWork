using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestWork.Api.Features.Employees.Actions;

namespace TestWork.Api.Features.Employees
{
    [Route("[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Рассылает прикреляемый текст письма сотрудникам, кроме группы Руководство
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.OK, "Письма успешно отправлены")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Отправлены некорректные данные")]
        [HttpPost("sendMail")]
        public async Task<IActionResult> SendMailToEmployees(SendMailToEmployees.Command command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}