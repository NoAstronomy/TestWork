using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TestWork.Db;
using TestWork.Services.Services.EmailSender;

namespace TestWork.Api.Features.Employees.Actions
{
    /// <summary>
    /// Рассылает прикреляемый текст письма сотрудникам, кроме группы Руководство
    /// </summary>
    public class SendMailToEmployees
    {
        public class Command : IRequest
        {
            [FromBody]
            [Required]
            public Body Body { get; set; }
        }

        public class Body
        {
            [Required]
            public string MessageText { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Body)
                    .NotEmpty()
                    .Must(x => !string.IsNullOrWhiteSpace(x.MessageText))
                    .WithMessage("Некорректно указано текстовое сообщение для рассылки.");
            }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly TestWorkDbContext _dbContext;
            private readonly IEmailSenderService _emailSenderService;
            private const string LeadershipGroupName = "Руководство";

            public Handler(TestWorkDbContext dbContext, IEmailSenderService emailSenderService)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _emailSenderService = emailSenderService ?? throw new ArgumentNullException(nameof(emailSenderService));
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken = default)
            {
                var emails = await _dbContext.Employees
                    .AsNoTracking()
                    .Where(x => !x.Groups
                        .Any(y => y.Group.Name == LeadershipGroupName))
                    .Select(x => x.Email)
                    .ToArrayAsync(cancellationToken);

                //Преобразуем сообщение в обычный текст, чтобы устранить уязвимость по передачи html тэгов в рассылках
                _emailSenderService.SendTextMessage(
                    HttpUtility.HtmlEncode(request.Body.MessageText),
                    "Рассылка текстовых сообщений",
                    cancellationToken,
                    emails);
            }
        }
    }
}