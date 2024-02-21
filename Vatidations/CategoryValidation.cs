namespace quipquick_api.Vatidations
{
    using FluentValidation;
    using quipquick_api.Models;

    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation() 
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Debe Ingresar el nombre de la categoría")
                .Length(3,30)
                .WithMessage("El nombre debe tener una longitud entre {MinLength} y {MaxLength} letras");
        }
    }
}
