using System;
using System.Threading;
using System.Threading.Tasks;
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
        /// Возвращает информацию о аккаунте пользователя.
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
    }
}