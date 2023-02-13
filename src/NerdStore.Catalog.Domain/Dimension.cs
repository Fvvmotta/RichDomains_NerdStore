using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            AssertionConcern.AssertArgumentIfLessThan(height, 1, "Height field of the product can not be less than or equals to 1");
            AssertionConcern.AssertArgumentIfLessThan(width, 1, "Width field of the product can not be less than or equals to 1");
            AssertionConcern.AssertArgumentIfLessThan(depth, 1, "Depth field of the product can not be less than or equals to 1");

            Height = height;
            Width = width;
            Depth = depth;
        }

        public string FormatedDescription()
        {
            return $"WxHxD: {Width} x {Height} x {Depth}";
        }

        public override string ToString()
        {
            return FormatedDescription();
        }
    }
}
