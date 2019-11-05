using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PROJETO.Models;

namespace EventShareBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        EventShareContext context = new EventShareContext();

        private IConfiguration config;
        public LoginController(IConfiguration mconfig){
            config = mconfig;
        }   
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UsuarioTbl login){
            IActionResult resposta = Unauthorized();

            var usuario = autenticarUsuario(login);

            if(usuario != null){
                var tokenString = gerarJsonWebToken(usuario);
                resposta = Ok(new { token = tokenString });
            }

            return resposta;
        }

        private UsuarioTbl autenticarUsuario(UsuarioTbl login){
            var usuario = context.UsuarioTbl.FirstOrDefault(user => user.UsuarioEmail == login.UsuarioEmail && user.UsuarioSenha == login.UsuarioSenha);


            return usuario;
        }

        private string gerarJsonWebToken(UsuarioTbl infousuario){
            var chaveDeSegurança = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])); 
            var credencial = new SigningCredentials(chaveDeSegurança, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.NameId, infousuario.UsuarioNome),
                new Claim(JwtRegisteredClaimNames.Email, infousuario.UsuarioEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], 
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120), 
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        
        }
    }
}