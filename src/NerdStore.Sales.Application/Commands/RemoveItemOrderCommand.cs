using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands
{
    public class RemoveItemOrderCommand : Command 
    {
        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }

        public RemoveItemOrderCommand(Guid clienteId, Guid produtoId)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
        }

        public override bool ItsValid()
        {
            ValidationResult = new RemoveItemOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveItemOrderValidation : AbstractValidator<RemoveItemOrderCommand>
    {
        public RemoveItemOrderValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client id");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product id");
        }
    }
}

