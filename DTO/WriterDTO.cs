using Project.Models;


using FluentValidation;

namespace Project.DTO
{
    public class WriterValidator : AbstractValidator<DTO.WriterDTO>
    {
        public WriterValidator()
        {
            RuleFor(writer => writer.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(writer => writer.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

        }
    }
    public class WriterDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ArticleDTO> Articles { get; set; }
    }
}
