using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command 
    {
        public Guid ClienteId { get; private set; }
        public string CodigoVoucher { get; private set; }

        public ApplyVoucherOrderCommand(Guid clienteId, string codigoVoucher)
        {
            ClienteId = clienteId;
            CodigoVoucher = codigoVoucher;
        }

        public override bool ItsValid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid client id");

            RuleFor(c => c.CodigoVoucher)
                .NotEmpty()
                .WithMessage("Voucher code cannot be empty");
        }
    }
}

