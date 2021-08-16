using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExecutionsViewer.App.Controllers;
using ExecutionsViewer.App.Database;
using ExecutionsViewer.App.Database.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExecutionsViewer.App.Services.Token
{
    // public class TokenUserStore : IUserStore<User>, IUserEmailStore<User>, IUserPasswordStore<User>
    // {
    //     readonly ExecutionsViewerDbContext db;
    //     readonly ILogger<UserController> log;
    //
    //     public TokenUserStore(ExecutionsViewerDbContext db, ILogger<UserController> log)
    //     {
    //         this.db = db;
    //         this.log = log;
    //     }
    //
    //     public void Dispose()
    //     {
    //         // Nothing to dispose
    //     }
    //
    //     public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(user.Id.ToString());
    //     }
    //
    //     public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(user.Username);
    //     }
    //
    //     public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    //     {
    //         user.Username = userName;
    //         return Task.FromResult(0);
    //     }
    //
    //     public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(user.NormalizedUsername);
    //     }
    //
    //     public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    //     {
    //         user.NormalizedUsername = normalizedName;
    //         return Task.FromResult(0);
    //     }
    //
    //     public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    //     {
    //         try
    //         {
    //             db.Users.Add(user);
    //             await db.SaveChangesAsync(cancellationToken);
    //             return IdentityResult.Success;
    //         }
    //         catch (DbUpdateException e)
    //         {
    //             log.LogError($"Failed to create user. Exception: {e.Message}");
    //         }
    //
    //         return IdentityResult.Failed();
    //     }
    //
    //     public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    //     {
    //         return await db.Users
    //             .FirstOrDefaultAsync(u => u.NormalizedUsername == normalizedUserName,
    //                 cancellationToken: cancellationToken);
    //     }
    //
    //     public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
    //     {
    //         user.Email = user.Email;
    //         return Task.FromResult(0);
    //     }
    //
    //     public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(user.Email);
    //     }
    //
    //     public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(true);
    //     }
    //
    //     public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(0);
    //     }
    //
    //     public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(user.NormalizedEmail);
    //     }
    //
    //     public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
    //     {
    //         user.NormalizedEmail = normalizedEmail;
    //         return Task.FromResult(0);
    //     }
    //
    //     public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    //     {
    //         user.PasswordHash = passwordHash;
    //         return Task.FromResult(0);
    //     }
    //
    //     public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(user.PasswordHash);
    //     }
    //
    //     public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    //     {
    //         return Task.FromResult(user.PasswordHash != null);
    //     }
    // }
}