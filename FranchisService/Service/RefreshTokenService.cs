using System.Security.Cryptography;
using System.Collections.Concurrent;
using FranchisService.Models.Response;

namespace FranchisService.Service
{
    /// <summary>
    /// Service for managing refresh tokens.
    /// </summary>
    public class RefreshTokenService
    {
        // In-memory store for demo; replace with DB in production
        private static readonly ConcurrentDictionary<string, RefreshToken> _store = new();

        /// <summary>
        /// Generates a new refresh token for a given user ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static RefreshToken Generate(string userId)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshToken = new RefreshToken
            {
                Token = token,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserId = userId
            };
            _store[token] = refreshToken;
            
            return refreshToken;
        }

        /// <summary>
        /// Retrieves a refresh token by its token string.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static RefreshToken? Get(string token)
        {
            _store.TryGetValue(token, out var refreshToken);
            
            return refreshToken;
        }

        /// <summary>
        /// Removes a refresh token from the store.
        /// </summary>
        /// <param name="token"></param>
        public static void Remove(string token)
        {
            _store.TryRemove(token, out _);
        }
    }
}
