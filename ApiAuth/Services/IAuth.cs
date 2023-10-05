

using Domain.Entities;

namespace ApiAuth.Services;
    public interface IAuth
    {
        byte[] CreateQR(ref User user);
        bool VerifyCode(string secret, string code);
    }
