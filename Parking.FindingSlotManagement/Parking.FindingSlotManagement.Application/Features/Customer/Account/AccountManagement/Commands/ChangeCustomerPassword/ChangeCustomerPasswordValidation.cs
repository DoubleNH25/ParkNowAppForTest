using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Account.AccountManagement.Commands.ChangeCustomerPassword
{
    public class ChangeCustomerPasswordValidation : AbstractValidator<ChangeCustomerPasswordCommand>
    {
        public ChangeCustomerPasswordValidation()
        {
            RuleFor(p => p.CurrentPassword)
                .NotEmpty().WithMessage("{CurrentPassword} không được để trống")
                .NotNull().WithMessage("{CurrentPassword} không được để trống");

            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("{NewPassword} không được để trống")
                .NotNull().WithMessage("{NewPassword} không được để trống")
                .MinimumLength(6).WithMessage("{NewPassword} phải có ít nhất 6 ký tự")
                .MaximumLength(50).WithMessage("{NewPassword} không được quá 50 ký tự")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$")
                .WithMessage("{NewPassword} phải chứa ít nhất 1 chữ hoa, 1 chữ thường và 1 số");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("{ConfirmPassword} không được để trống")
                .NotNull().WithMessage("{ConfirmPassword} không được để trống")
                .Equal(p => p.NewPassword).WithMessage("Xác nhận mật khẩu không khớp");

            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("{UserId} phải lớn hơn 0");
        }
    }
}
