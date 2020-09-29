using System;
using System.Threading;
using System.Threading.Tasks;
using Employees.Application.Accounts.Commands.ConfirmationEmail;
using Employees.Application.Accounts.Commands.RegisterAccount;
using Employees.Application.Accounts.Queries.GetAccountsDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Employees.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Возвращает информацию о аккаунте пользователя по ID.
        /// </summary>
        /// <param name="id">ID аккаунта</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public Task<AccountDetailsDto> GetAccountDetail(Guid id, CancellationToken ct = default)
        {
            return _mediator.Send(new GetAccountDetailsQuery { AccountId = id}, ct);
        }
        
        /// <summary>
        /// Регистрирует новый аккаунт в системе.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public Task RegisterAccount(
            [FromBody] RegisterAccountCommand request, 
            CancellationToken ct = default)
        {
            return _mediator.Send(request, ct);
        }

        /// <summary>
        /// Подтвердить Email аккаунта. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [Route("confirm-email")]
        [HttpPut]
        public Task ConfirmEmail(
            [FromBody] ConfirmationEmailCommand request,
            CancellationToken ct = default
        )
        {
            return _mediator.Send(request, ct);
        }
    }
}