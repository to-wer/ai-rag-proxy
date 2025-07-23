using System.Security.Claims;
using System.Text.Encodings.Web;
using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.PersonalAccessToken;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace AiRagProxy.Api.Middlewares;

public class PatAuthenticationHandler(
    ITokenValidationService tokenValidationService,
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory loggerFactory, 
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, loggerFactory, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? authHeader = Request.Headers["Authorization"];
        if(string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return AuthenticateResult.NoResult();
        }
        
        var token = authHeader.Substring("Bearer ".Length).Trim();
        if (string.IsNullOrWhiteSpace(token))
        {
            return AuthenticateResult.NoResult();
        }
        
        ValidatedToken? validatedToken = tokenValidationService.ValidateToken(token);
        
        if (validatedToken == null)
        {
            return AuthenticateResult.Fail("Invalid or expired PAT");
        }

        var claims = new List<Claim>();
        // {
        //     new(ClaimTypes.NameIdentifier, validatedToken.ExternalId),
        //     new(ClaimTypes.Name, validatedToken.DisplayName ?? validatedToken.Email),
        //     new("pat_id", validatedToken.TokenId.ToString())
        // };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

		
        return AuthenticateResult.Success(ticket);
    }
}