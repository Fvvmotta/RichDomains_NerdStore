﻿using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalog.Application.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime RegisterDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have a minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int InventoryQuantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have a minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Height { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have a minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Width { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The field {0} needs to have a minimum value of {1}")]
        [Required(ErrorMessage = "The field {0} is required")]
        public int Depth { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
