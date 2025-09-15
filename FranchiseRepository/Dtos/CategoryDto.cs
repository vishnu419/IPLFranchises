using System.ComponentModel.DataAnnotations;

namespace FranchiseRepository.Dtos
{
    /// <summary>
    /// Data Transfer Object for Category entity.
    /// </summary>
    public class CategoryDto
    {
        [Key]
        public Guid CategoryId { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        // Navigation property
        public ICollection<ProductDto>? Products { get; set; }
    }
}
