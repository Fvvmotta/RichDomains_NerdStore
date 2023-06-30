using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands
{
    public class AddItemOrderCommand : Command
    {
        public Guid ClientId { get; private set; }
        public  Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }

        public AddItemOrderCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitValue)
        {
            ClientId = clientId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public override bool ItsValid()
        {
            ValidationResult = new AddItemToOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddItemToOrderValidation : AbstractValidator<AddItemOrderCommand>
    {
        public AddItemToOrderValidation()
        {
            RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client id");
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product id");
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name of the product was not informed");
            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be at least 1");
            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("Quantity can not be more than 15");
            RuleFor(c => c.UnitValue)
                .GreaterThan(0)
                .WithMessage("Value must be greater than 0");
        }
    }
}
