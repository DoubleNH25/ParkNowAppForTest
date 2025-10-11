using Moq;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Features.Customer.Account.AccountManagement.Commands.ChangeCustomerPassword;
using Parking.FindingSlotManagement.Domain.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Parking.FindingSlotManagement.Application.UnitTests.HandlerTesting.Customer.Account
{
    public class ChangeCustomerPasswordCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly ChangeCustomerPasswordCommandHandler _handler;
        private readonly ChangeCustomerPasswordValidation _validator;

        public ChangeCustomerPasswordCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _validator = new ChangeCustomerPasswordValidation();
            _handler = new ChangeCustomerPasswordCommandHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenUserDoesNotExist_ReturnsNotFoundResponse()
        {
            // Arrange
            var request = new ChangeCustomerPasswordCommand
            {
                UserId = 1,
                CurrentPassword = "currentPassword",
                NewPassword = "NewPass123",
                ConfirmPassword = "NewPass123"
            };

            _userRepositoryMock.Setup(x => x.GetById(request.UserId))
                .ReturnsAsync((User)null);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.StatusCode.ShouldBe(404);
            response.Success.ShouldBe(false);
            response.Message.ShouldBe("Không tìm thấy tài khoản.");
        }

        [Fact]
        public async Task Handle_WhenCurrentPasswordIsIncorrect_ReturnsBadRequestResponse()
        {
            // Arrange
            var request = new ChangeCustomerPasswordCommand
            {
                UserId = 1,
                CurrentPassword = "wrongPassword",
                NewPassword = "NewPass123",
                ConfirmPassword = "NewPass123"
            };

            var existingUser = new User
            {
                UserId = 1,
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };

            _userRepositoryMock.Setup(x => x.GetById(request.UserId))
                .ReturnsAsync(existingUser);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.StatusCode.ShouldBe(400);
            response.Success.ShouldBe(false);
            response.Message.ShouldBe("Mật khẩu hiện tại không đúng.");
        }

        [Fact]
        public void NewPassword_ShouldNotBeEmpty()
        {
            var command = new ChangeCustomerPasswordCommand
            {
                UserId = 1,
                CurrentPassword = "currentPassword",
                NewPassword = "",
                ConfirmPassword = ""
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
            result.Errors.Any(x => x.PropertyName == "NewPassword").ShouldBe(true);
        }

        [Fact]
        public void NewPassword_ShouldMeetComplexityRequirements()
        {
            var command = new ChangeCustomerPasswordCommand
            {
                UserId = 1,
                CurrentPassword = "currentPassword",
                NewPassword = "weak",
                ConfirmPassword = "weak"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
            result.Errors.Any(x => x.PropertyName == "NewPassword").ShouldBe(true);
        }

        [Fact]
        public void ConfirmPassword_ShouldMatchNewPassword()
        {
            var command = new ChangeCustomerPasswordCommand
            {
                UserId = 1,
                CurrentPassword = "currentPassword",
                NewPassword = "NewPass123",
                ConfirmPassword = "DifferentPass123"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
            result.Errors.Any(x => x.PropertyName == "ConfirmPassword").ShouldBe(true);
        }

        [Fact]
        public void UserId_ShouldBeGreaterThanZero()
        {
            var command = new ChangeCustomerPasswordCommand
            {
                UserId = 0,
                CurrentPassword = "currentPassword",
                NewPassword = "NewPass123",
                ConfirmPassword = "NewPass123"
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBe(false);
            result.Errors.Any(x => x.PropertyName == "UserId").ShouldBe(true);
        }
    }
}
