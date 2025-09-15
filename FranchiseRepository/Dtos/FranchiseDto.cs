using System.ComponentModel.DataAnnotations;

namespace FranchiseRepository.Dtos
{
    /// <summary>
    /// Data Transfer Object for Franchise entity.
    /// </summary>
    public class FranchiseDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        // Navigation property
        public ICollection<ProductDto>? Products { get; set; }
    }
}
