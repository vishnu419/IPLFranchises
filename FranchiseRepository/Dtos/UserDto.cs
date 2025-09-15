using System.ComponentModel.DataAnnotations;

namespace FranchiseRepository.Dtos
{
    /// <summary>
    /// Data Transfer Object for User entity.
    /// </summary>
    public class UserDto
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; } = default!;

        public string PasswordHash { get; set; } = default!;

        public string Email { get; set; } = default!;
    }
}
