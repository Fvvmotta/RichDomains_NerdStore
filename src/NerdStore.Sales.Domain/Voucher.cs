using FluentValidation;
using FluentValidation.Results;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percent { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public VoucherDiscountType VoucherDiscountType { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        // EF Rel.
        public ICollection<Order> Orders { get; set; }

        internal ValidationResult ValidateIfAplicable()
        {
            return new ApplicableVoucherValidation().Validate(this);
        }
    }

    public class ApplicableVoucherValidation : AbstractValidator<Voucher>
    {

        public ApplicableVoucherValidation()
        {
            RuleFor(c => c.ExpirationDate)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage("This Voucher expired.");

            RuleFor(c => c.Active)
                .Equal(true)
                .WithMessage("This voucher is not active anymore.");

            RuleFor(c => c.Used)
                .Equal(false)
                .WithMessage("This voucher has already been used.");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("This voucher is not available anymore");
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime expirationDate)
        {
            return expirationDate >= DateTime.Now;
        }
    }
}
