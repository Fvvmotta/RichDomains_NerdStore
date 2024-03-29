﻿
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }

        // EF Relation
        public ICollection<Product> Products { get; set; }

        protected Category() { }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;

            Validate();
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentIfEmpty(Name, "The Product Name field can not be empty");
            AssertionConcern.AssertArgumentIfEquals(Code, 0, "The Product Code field can not be 0");
        }
    }
}
