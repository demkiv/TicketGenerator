using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketGenerator.UI.Models
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class IsDateAfterNowAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime && (DateTime)value >= DateTime.Now)
			{
				return ValidationResult.Success;
			}

			return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
		}
	}
}