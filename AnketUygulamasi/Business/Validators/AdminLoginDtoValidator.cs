using Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Validators
{
	public class AdminLoginDtoValidator : AbstractValidator<AdminLoginDto>
	{
		public AdminLoginDtoValidator()
		{
			RuleFor(x => x.Email)
			.NotEmpty().WithMessage("E-posta alanı boş olamaz.")
			.Must(BeAValidEmail).WithMessage("Hatalı giriş yaptınız kontrol ediniz!");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Şifre alanı boş olamaz.")
				.MinimumLength(6).WithMessage("Hatalı giriş yaptınız kontrol ediniz!");
		}

		private bool BeAValidEmail(string email)
		{
			string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
			return Regex.IsMatch(email, emailPattern);
		}
	}
}
