using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands
{
    public class UpdateItemOrderCommand : Command 
    {
        public Guid ClientId { get; private set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public UpdateItemOrderCommand(Guid clientId, Guid productId, int quantity)
        {
            ClientId = clientId;
            ProductId = productId;
            Quantity = quantity;
        }
        public override bool ItsValid()
        {
            ValidationResult = new UpdateItemOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateItemOrderValidation : AbstractValidator<UpdateItemOrderCommand>
    {
        public UpdateItemOrderValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client id");
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product id");
            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be at least 1");
            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("Quantity can not be more than 15");
        }
    }
}
