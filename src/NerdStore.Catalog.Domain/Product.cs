using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Value { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int InventoryQuantity { get; private set; }
        public Dimensions Dimensions { get; private set; }
        public Category Category { get; private set; }

        protected Product(){}
        public Product(string name, string description, bool active, decimal value, Guid categoryId, DateTime registerDate, string image, Dimensions dimensions)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Active = active;
            Value = value;
            RegisterDate = registerDate;
            Image = image;
            Dimensions = dimensions;

            Validate();
        }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            AssertionConcern.AssertArgumentIfEmpty(description, "Description Field of the product can not be empty");
            Description = description;
        }

        public void DebitInventory(int quantity)
        {
            if (quantity < 0) quantity *= -1;
            if (!HasInventory(quantity)) throw new DomainException("Insuficient stock");
            InventoryQuantity -= quantity;
        }

        public void ReplenishInventory(int quantity)
        {
            InventoryQuantity += quantity;
        }
        public bool HasInventory(int quantity)
        {
            return InventoryQuantity >= quantity;
        }
        public void Validate()
        {
            AssertionConcern.AssertArgumentIfEmpty(Name, "Name of the product can not be empty");
            AssertionConcern.AssertArgumentIfEmpty(Description, "Description of the product can not be empty");
            AssertionConcern.AssertArgumentIfEquals(CategoryId, Guid.Empty, "Category Id of the product can not be empty");
            AssertionConcern.AssertArgumentIfLessThanOrEqualsMinimun(Value, 1, "Value of the product can not be less than or equals to 0");
            AssertionConcern.AssertArgumentIfEmpty(Image, "Image field of the product can not be empty");
        }
    }
}
