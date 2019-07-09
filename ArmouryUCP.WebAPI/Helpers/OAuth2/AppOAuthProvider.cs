using ArmouryUCP.WebAPI.Models;
using ArmouryUCP.WebAPI.Models.Dtos;
using ArmouryUCP.WebAPI.Services;
using AutoMapper;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ArmouryUCP.WebAPI.Helpers.OAuth2
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        #region Private Properties  

        /// <summary>  
        /// Public client ID property.  
        /// </summary>  
        private readonly string _publicClientId;

        private PlayerService _playerService;
        private PlayerService playerService
        {
            get
            {
                if (_playerService == null)
                    _playerService = new PlayerService();

                return _playerService;
            }
        }

        #endregion

        #region Default Constructor method.  

        /// <summary>  
        /// Default Constructor method.  
        /// </summary>  
        /// <param name="publicClientId">Public client ID parameter</param>  
        public AppOAuthProvider(string publicClientId)
        {
            // Settings.  
            _publicClientId = publicClientId ?? throw new ArgumentNullException(nameof(publicClientId));
        }

        #endregion

        #region Grant resource owner credentials override method.  

        /// <summary>  
        /// Grant resource owner credentials overload method.  
        /// </summary>  
        /// <param name="context">Context parameter</param>  
        /// <returns>Returns when task is completed</returns>  
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Initialization.  
            string usernameVal = context.UserName;
            string passwordVal = context.Password;
            var user = playerService.LoginPlayer(usernameVal, passwordVal);

            // Verification.  
            if (user == null)
            {
                // Settings.  
                context.SetError("invalid_grant", "The user name or password is incorrect.");

                // Return info.  
                return;
            }

            // Initialization.  
            var claims = new List<Claim>();
            var additionalClaims = CreateProperties(user);

            // Setting  
            claims.Add(new Claim(ClaimTypes.Name, user.Name));

            foreach (var property in additionalClaims.Dictionary)
            {
                claims.Add(new Claim(ClaimTypes.Name.Substring(0, ClaimTypes.Name.LastIndexOf('/') + 1) + property.Key, property.Value, ClaimValueTypes.String));
            }

            // Setting Claim Identities for OAUTH 2 protocol.  
            ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

            // Setting user authentication.  
            AuthenticationProperties properties = additionalClaims;
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);


            // Grant access to authorize user.  
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);
        }

        #endregion

        #region Token endpoint override method.  

        /// <summary>  
        /// Token endpoint override method  
        /// </summary>  
        /// <param name="context">Context parameter</param>  
        /// <returns>Returns when task is completed</returns>  
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                // Adding.  
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            // Return info.  
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate Client authntication override method  

        /// <summary>  
        /// Validate Client authntication override method  
        /// </summary>  
        /// <param name="context">Contect parameter</param>  
        /// <returns>Returns validation of client authentication</returns>  
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.  
            if (context.ClientId == null)
            {
                // Validate Authoorization.  
                context.Validated();
            }

            // Return info.  
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate client redirect URI override method  

        /// <summary>  
        /// Validate client redirect URI override method  
        /// </summary>  
        /// <param name="context">Context parmeter</param>  
        /// <returns>Returns validation of client redirect URI</returns>  
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            // Verification.  
            if (context.ClientId == _publicClientId)
            {
                // Initialization.  
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                // Verification.  
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    // Validating.  
                    context.Validated();
                }
            }

            // Return info.  
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Create Authentication properties method.  

        /// <summary>  
        /// Create Authentication properties method.  
        /// </summary>  
        /// <param name="userName">User name parameter</param>  
        /// <returns>Returns authenticated properties.</returns>  
        public static AuthenticationProperties CreateProperties(Player player)
        {
            var playerDto = Mapper.Map<PlayerDto>(player);
            // Settings.  
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "PlayerID", playerDto.ID.ToString() },
                { "Username", playerDto.Name },
                { "Model", playerDto.Model.ToString() },
                { "Level", playerDto.Level.ToString() },
                { "LevelProgress", playerDto.LevelProgress.ToString() }
            };

            // Return info.  
            return new AuthenticationProperties(data);
        }

        #endregion
    }
}