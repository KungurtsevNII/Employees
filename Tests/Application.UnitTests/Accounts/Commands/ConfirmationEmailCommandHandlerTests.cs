using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Configuration;
using AutoFixture;
using Employees.Application.Accounts.Commands.ConfirmationEmail;
using Employees.Application.Configuration.Exceptions;
using Employees.Domain.Accounts;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Application.UnitTests.Accounts.Commands
{
    public class ConfirmationEmailCommandHandlerTests : TypedTestBase<ConfirmationEmailCommandHandler>
    {
        [Fact]
        public async Task Handle_EmailIsNotExists_Throws()
        {
            // Arrange
            var command = _fixture.Create<ConfirmationEmailCommand>();

            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.IsExistsByEmailAsync(command.Email))
                .ReturnsAsync(false);
            
            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => Sut.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal(new NotFoundException(Account.EntityName, command.Email).Message, ex.Message);
        }
        
        [Fact]
        public async Task Handle_AllRight_ConfirmEmail()
        {
            // Arrange
            var command = _fixture.Create<ConfirmationEmailCommand>();
            var account = _fixture.Create<Account>();

            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.IsExistsByEmailAsync(command.Email))
                .ReturnsAsync(true);
            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.GetByEmailAsync(command.Email))
                .ReturnsAsync(account);
            
            // Act
            await Sut.Handle(command, CancellationToken.None);

            // Assert
            _mock
                .Mock<IAccountsRepository>()
                .Verify(x => x.SetIsEmailConfirm(account.IsEmailConfirm, account.Id), Times.Once);

            LoggerMessageVerify<ConfirmationEmailCommandHandler>(LogLevel.Information, $"Email - {account.Email} подтвержден.");
        } 
    }
}