using System;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Configuration;
using AutoFixture;
using Employees.Application.Accounts.Commands.RegisterAccount;
using Employees.Application.Configuration.Exceptions;
using Employees.Domain.Accounts;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Application.UnitTests.Accounts.Commands
{
    public class RegisterAccountCommandHandlerTests : TypedTestBase<RegisterAccountCommandHandler>
    {
        [Fact]
        public async Task Handle_EmailIsExists_Throws()
        {
            // Arrange
            var command = _fixture.Create<RegisterAccountCommand>();

            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.IsExistsByEmailAsync(command.Email))
                .ReturnsAsync(true);

            // Act
            var ex = await Assert.ThrowsAsync<BadRequestException>(() => Sut.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal($"Email - {command.Email} уже есть в системе.", ex.Message);
        }

        [Fact]
        public async Task Handle_PhoneNumberIsExists_Throws()
        {
            // Arrange
            var command = _fixture.Create<RegisterAccountCommand>();

            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.IsExistsByEmailAsync(command.Email))
                .ReturnsAsync(false);
            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.IsExistsByPhoneAsync(command.PhoneNumber))
                .ReturnsAsync(true);

            // Act
            var ex = await Assert.ThrowsAsync<BadRequestException>(() => Sut.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal($"Телефон - {command.PhoneNumber} уже есть в системе.", ex.Message);
        }

        [Fact]
        public async Task Handle_AccountIsNotExists_CreateAccountAndLogInfo()
        {
            // Arrange
            var command = _fixture.Create<RegisterAccountCommand>();
            var account = _fixture.Build<Account>()
                .With(x => x.PhoneNumber, command.PhoneNumber)
                .With(x => x.Email, command.Email)
                .With(x => x.IsEmailConfirm, false)
                .With(x => x.IsPhoneConfirm, false)
                .Create();

            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.IsExistsByEmailAsync(command.Email))
                .ReturnsAsync(false);
            _mock.Mock<IAccountsRepository>()
                .Setup(x => x.IsExistsByPhoneAsync(command.PhoneNumber))
                .ReturnsAsync(false);
            
            // Act
            var ex = await Sut.Handle(command, CancellationToken.None);

            // Assert
            LoggerMessageVerify<RegisterAccountCommandHandler>(LogLevel.Information, $"Успешно создан пользователь с Email - {command.Email} и номером телефона - {command.PhoneNumber}");

            _mock.Mock<IAccountsRepository>()
                .Verify(x => x.CreateAsync(
                    It.Is<Account>(y => 
                        y.IsEmailConfirm == account.IsEmailConfirm &&
                        y.IsPhoneConfirm == account.IsPhoneConfirm &&
                        y.PhoneNumber == account.PhoneNumber &&
                        y.Email == account.Email &&
                        y.Id == Guid.Empty)
                    ), Times.Once);
        }
    }
}
