

using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TwoFactorAuthNet;
using TwoFactorAuthNet.Providers.Qr;

namespace ApiAuth.Services;
    public class Auth : IAuth
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _conf;
        private readonly int _accessTokenDuration;
        private readonly int _refreshTokenTokenDuration;
        private readonly ILogger<Auth> _logger;
        public Auth
        (
            IPasswordHasher<User> passwordHasher,
            IConfiguration conf ,
            ILogger<Auth> logger
        )
        {
            _conf = conf;
            _passwordHasher = passwordHasher;
            _logger = logger;


            // Duracion del Token
            _ = int.TryParse(conf["JWTSettings:AccessTokenTimeInMinutes"], out _accessTokenDuration);
            _ = int.TryParse(conf["JWTSettings:RefreshTokenTimeInHours"], out _refreshTokenTokenDuration);        
        }

        public byte[] CreateQR(ref User user)
        {        
            if(user.Email == null)
            {
                throw new ArgumentNullException(user.Email);
            }        
            var tsa = new TwoFactorAuth(_conf["JWTSettings:Issuer"],6,30,Algorithm.SHA256, new ImageChartsQrCodeProvider());
            string secret = tsa.CreateSecret(160);
            user.TwoStepSecret = secret;
            var QR = tsa.GetQrCodeImageAsDataUri(user.Email, user.TwoStepSecret); 
            string UriQR = QR.Replace("data:image/png;base64,", "");
            return Convert.FromBase64String(UriQR);        
        }

        public bool VerifyCode(string secret, string code){        
            var tsa = new TwoFactorAuth(_conf["JWTSettings:Issuer"],6,30,Algorithm.SHA256);
            return tsa.VerifyCode(secret, code, 1);
        }
    }
