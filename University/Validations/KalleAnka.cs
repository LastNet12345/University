using System.ComponentModel.DataAnnotations;
using University.Models;

namespace University.Validations
{
    public class KalleAnka : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            //var errorMessage1 = "hej hej";
            //var errorMessage2 = "hej då";

            if (value is string input)
            {
                //var input = value as string;
                var viewModel = validationContext.ObjectInstance as StudentCreateViewModel;

                if (viewModel is not null) 
                {
                    if (viewModel.FirstName == "Kalle" && input == "Anka")
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return new ValidationResult(ErrorMessage);

        }
    }
}
