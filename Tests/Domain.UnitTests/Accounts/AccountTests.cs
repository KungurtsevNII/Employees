using Application.UnitTests.Configuration;
using Employees.Domain.Accounts;
using Xunit;

namespace Domain.UnitTests.Accounts
{
    public class AccountTests : TypedTestBase<Account>
    {
        [Fact]
        public void ConfirmEmail_False_SetTrue()
        {
            // Arrange
            Sut.IsEmailConfirm = false;
            
            // Act
            Sut.ConfirmEmail();
            
            // Assert
            Assert.True(Sut.IsEmailConfirm);
        }
        
        [Fact]
        public void ConfirmEmail_True_SetTrue()
        {
            // Arrange
            Sut.IsEmailConfirm = true;
            
            // Act
            Sut.ConfirmEmail();
            
            // Assert
            Assert.True(Sut.IsEmailConfirm);
        }
    }
}