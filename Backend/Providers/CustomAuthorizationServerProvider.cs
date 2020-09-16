using Domain.Models;
using LinqKit;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Repository;
using Services.Rules.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Providers
{
    public class CustomAuthorizationServerProvider : OAuthAuthorizationServerProvider, IDisposable
    {
        protected IRepository Repository { get; set; }

        public CustomAuthorizationServerProvider()
        {
            Repository = new LivrariaRepository(new LivrariaContext());
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new string[] { "*" }); //apenas para uso em dev
            var usuario = Autenticar(context.UserName, context.Password);

            if (usuario != default)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                //se fosse utilizar várias roles
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Permissao.ToString()));

                var authenticationData = GetAuthenticationData(identity, usuario);
                var authenticationProperty = new AuthenticationProperties(authenticationData);
                var ticket = new AuthenticationTicket(identity, authenticationProperty);

                context.Validated(ticket);
            }
            else
            {
                context.SetError("invalid_grant", "Login ou senha incorreto.");
            }

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        private Usuario Autenticar(string email, string senha)
        {
            var senhaRule = new SenhaRule(senha).Predicate;

            var usuario = Repository
                .Recuperar<Usuario>()
                .AsExpandable()
                .FirstOrDefault(o => o.Email == email && senhaRule.Invoke(o));
            return usuario;
        }

        private Dictionary<string, string> GetAuthenticationData(ClaimsIdentity identity, Usuario usuario)
        {
            var dados = JsonConvert.SerializeObject(new
            {
                Autenticado = true,
                usuario.Id,
                usuario.Email,
                usuario.Permissao,
                //se fosse utilizar várias roles
                //Roles = identity.Claims
                //    .Where(o => o.Type == ClaimTypes.Role)
                //    .Select(o => new
                //    {
                //        Tipo = o.Type.ToLower(),
                //        Valor = o.Value.ToLower()
                //    })
            }, Startup.JsonSerializerSettings);

            return new Dictionary<string, string>()
            {
                { "usuario", dados }
            };
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}