using Moq;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Features.Admin.Fee.Queries.GetFeeById;
using Parking.FindingSlotManagement.Application.Features.Customer.Account.AccountManagement.Queries.GetCustomerProfileById;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.UnitTests.HandlerTesting.Customer.Account
{
    public class GetCustomerProfileByIdQueryHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly GetCustomerProfileByIdQueryHandler _handler;
        public GetCustomerProfileByIdQueryHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new GetCustomerProfileByIdQueryHandler(_userRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_WhenFeeDoesNotExist_ReturnsNotFoundResponse()
        {
            // Arrange
            var request = new GetCustomerProfileByIdQuery { UserId = 1 };
            _userRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync((Domain.Entities.User)null);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldNotBeNull();
            response.Success.ShouldBeTrue();
            response.StatusCode.ShouldBe(200);
            response.Message.ShouldBe("Không tìm thấy tài khoản.");
        }
        [Fact]
        public async Task Handle_WhenFeeExists_ReturnsSuccessResponse()
        {
            // Arrange
            var request = new GetCustomerProfileByIdQuery { UserId = 1 };
            var user = new Domain.Entities.User 
            { 
                UserId = 1, 
                Name = "Test User",
                Email = "test@example.com",
                Phone = "0123456789",
                Avatar = "avatar_url",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Nam",
                Address = "123 Test Street"
            };

            _userRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(user);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldNotBeNull();
            response.Success.ShouldBeTrue();
            response.StatusCode.ShouldBe(200);
            response.Message.ShouldBe("Thành công");
            response.Data.ShouldNotBeNull();
            response.Data.UserId.ShouldBe(1);
            response.Data.Name.ShouldBe("Test User");
            response.Data.Email.ShouldBe("test@example.com");
            response.Data.Phone.ShouldBe("0123456789");
        }
    }
}
