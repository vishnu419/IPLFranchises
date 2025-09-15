using FranchiseRepository.Dtos;
using FranchiseRepository.IRepos;
using FranchisService.Helpers;
using FranchisService.IService;
using FranchisService.Models.Request;
using FranchisService.Models.Response;

namespace FranchisService.Service
{
    /// <summary>
    /// Service implementation for user-related operations.
    /// </summary>
    public class UserService(IUserRepository userRepository, JwtTokenHelper jwtTokenHelper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JwtTokenHelper _jwtTokenHelper = jwtTokenHelper;

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            // Check if user exists
            var existing = await _userRepository.GetByUsernameAsync(registerRequest.Username);
            if (existing != null) return false;

            // Hash password (simple demo, use a real hasher in prod)
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            var userDto = new UserDto
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PasswordHash = passwordHash
            };
            await _userRepository.AddAsync(userDto);
            return true;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<AuthResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
            if (user == null)
                return null;

            var valid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);
            if (!valid)
                return null;

            var userResponse = MapToResponse(user);
            var accessToken = _jwtTokenHelper.GenerateToken(userResponse);
            var refreshToken = RefreshTokenService.Generate(user.Id.ToString());

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                Username = userResponse.Username,
                Email = userResponse.Email,
                UserId = userResponse.Id.ToString()
            };
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            return MapToResponse(user);
        }

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
           
            return MapToResponse(user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            
            return users.Select(MapToResponse);
        }

        private UserResponse MapToResponse(UserDto user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}