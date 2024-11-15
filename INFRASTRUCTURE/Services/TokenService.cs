using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Identity;
using CORE.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using SharedLib;

namespace INFRASTRUCTURE.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        
        public TokenService(IConfiguration comfig)
        {
            _config = comfig;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:key"]));

        }

        public string CreateToken(AppUser user)
        {
           var claims = new List<Claim>
           {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Displayname)

           };

           var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

           var tokenDescriptor = new SecurityTokenDescriptor
           {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
           };

           var tokenHandler = new JwtSecurityTokenHandler();

           var token = tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }


        

        bool ITokenService.SendEmail(EmailNotificationVM emailNotificationVM)
        {
            bool isSent = false;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("LeftHanded", "john.onwuegbuzie@osoftit.com"));
            message.To.Add(new MailboxAddress("New User", emailNotificationVM.To));
            message.Subject = emailNotificationVM.Subject;
            message.Body = new TextPart("html")
            {
                Text = emailNotificationVM.Body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("mercury-plesk.hkdns.host", 587, false);
                    client.Authenticate("john.onwuegbuzie@osoftit.com", "Feragamo@13");
                    client.Send(message);
                    client.Disconnect(true);
                    isSent = true;
                }
                catch (Exception ex)
                {
                    var msg = ex.StackTrace;
                    throw;
                }

            }

            return isSent;
        }
    }

   
}