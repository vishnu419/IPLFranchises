using System.ComponentModel.DataAnnotations;

namespace FranchiseRepository.Dtos
{
    public class ProductDto
    {
        [Key]
        public Guid Id { get; set; }
       
        public string Name { get; set; } = default!;
       
        public decimal Price { get; set; }
      
        public string ImageUrl { get; set; } = default!;
     
        public string Description { get; set; } = default!;

        public Guid FranchiseId { get; set; }

        public Guid CategoryId { get; set; }

        // Navigation properties
        public FranchiseDto? Franchise { get; set; }

        public CategoryDto? Category { get; set; }
    }
}
