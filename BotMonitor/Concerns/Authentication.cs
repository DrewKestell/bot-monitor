using BotMonitor.Data;
using BotMonitor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace BotMonitor.Concerns
{
    public class Authentication : IAuthentication
    {
        readonly BotContext context;
        readonly IPasswordHasher<User> passwordHasher;

        public Authentication(BotContext context, IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<int> AuthenticateUser(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new AuthenticationException("Account not found.");

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

            switch (verificationResult)
            {
                case PasswordVerificationResult.SuccessRehashNeeded:
                    user.HashedPassword = passwordHasher.HashPassword(user, password);
                    await context.SaveChangesAsync();
                    break;
                case PasswordVerificationResult.Failed:
                    throw new AuthenticationException("Invalid password.");
            }

            return user.Id;
        }
    }
}
