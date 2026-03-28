using ASC.Model.Constants;
using ASC.Web.Configuration;
using ASC.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ASC.Web.Seed;

public class IdentitySeed : IIdentitySeed
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOptions<SeedDataOptions> _seedDataOptions;

    public IdentitySeed(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IOptions<SeedDataOptions> seedDataOptions)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _seedDataOptions = seedDataOptions;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        foreach (var role in AppConstants.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var options = _seedDataOptions.Value;
        await EnsureUserAsync(
            options.DefaultAdmin.Email,
            options.DefaultAdmin.Password,
            options.DefaultAdmin.FullName,
            options.DefaultAdmin.PhoneNumber,
            [AppConstants.RoleAdmin],
            cancellationToken);

        foreach (var sampleUser in options.SampleUsers)
        {
            await EnsureUserAsync(
                sampleUser.Email,
                sampleUser.Password,
                sampleUser.FullName,
                sampleUser.PhoneNumber,
                sampleUser.Roles,
                cancellationToken);
        }
    }

    private async Task EnsureUserAsync(
        string email,
        string password,
        string fullName,
        string? phoneNumber,
        IReadOnlyCollection<string> roles,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser is null)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsActive = true
            };

            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join("; ", createResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException($"Unable to create seed user '{email}': {errors}");
            }

            existingUser = user;
        }


        var shouldUpdateUser = false;

        if (!string.Equals(existingUser.FullName, fullName, StringComparison.Ordinal))
        {
            existingUser.FullName = fullName;
            shouldUpdateUser = true;
        }

        if (!string.Equals(existingUser.PhoneNumber, phoneNumber, StringComparison.Ordinal))
        {
            existingUser.PhoneNumber = phoneNumber;
            shouldUpdateUser = true;
        }

        if (!existingUser.EmailConfirmed)
        {
            existingUser.EmailConfirmed = true;
            shouldUpdateUser = true;
        }

        if (!existingUser.IsActive)
        {
            existingUser.IsActive = true;
            shouldUpdateUser = true;
        }

        if (shouldUpdateUser)
        {
            var updateResult = await _userManager.UpdateAsync(existingUser);
            if (!updateResult.Succeeded)
            {
                var updateErrors = string.Join("; ", updateResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException($"Unable to update seed user '{email}': {updateErrors}");
            }
        }

        foreach (var role in roles)
        {
            if (!await _userManager.IsInRoleAsync(existingUser, role))
            {
                await _userManager.AddToRoleAsync(existingUser, role);
            }
        }
    }
}
