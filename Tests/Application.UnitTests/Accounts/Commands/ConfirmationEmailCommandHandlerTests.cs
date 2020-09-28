﻿using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Configuration;
using Autofac.Extras.Moq;
using AutoFixture;
using AutoFixture.AutoMoq;
using Employees.Application.Accounts.Commands.ConfirmationEmail;
using Employees.Application.Configuration.Exceptions;
using Employees.Domain.Accounts;
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
    }
}