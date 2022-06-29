using BLL.Models;
using DAL.Interfaces;
using FluentValidation;

namespace BLL.Validators
{
    public class ProductValidator : AbstractValidator<ProductModel>
    {
        public ProductValidator(ICategoryRepository categoryRepository)
        {
            RuleFor(p => p.CategoryId)
                .Must(x => categoryRepository.GetById(x) != null).WithMessage("Category not found");

            RuleFor(p => p.Name)
                .NotEmpty().Length(3, 30).WithMessage("{PropertyName} length error")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Incorrect name");

            RuleFor(p => p.Description)
                .NotEmpty().Length(5, 200).WithMessage("{PropertyName} length error");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("{PropertyName} error");
        }
    }
}
