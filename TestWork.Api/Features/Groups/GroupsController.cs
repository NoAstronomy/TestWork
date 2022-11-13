using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TestWork.Api.Features.Groups.Actions;

namespace TestWork.Api.Features.Groups
{
    [Route("[controller]")]
    public class GroupsController : Controller
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Рассылает прикреляемый текст письма сотрудникам, кроме указанных групп
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.OK, "Пользователи успешно получены")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Отправлены некорректные данные")]
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupEmployees(GetGroupEmployees.Query query, CancellationToken cancellationToken) => 
            Ok(await _mediator.Send(query, cancellationToken));

        /// <summary>
        /// Выдает список всех групп
        /// </summary>
        [SwaggerResponse((int)HttpStatusCode.OK, "Список групп успешно получен")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Отправлены некорректные данные")]
        [HttpGet("list")]
        public async Task<IActionResult> GetGroupList(GetGroupList.Query query, CancellationToken cancellationToken) => 
            Ok(await _mediator.Send(query, cancellationToken));
    }
}
