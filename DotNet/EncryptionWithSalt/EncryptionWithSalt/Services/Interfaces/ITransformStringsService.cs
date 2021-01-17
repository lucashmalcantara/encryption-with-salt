using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionWithSalt.Services.Interfaces
{
    public interface ITransformStringsService
    {
        string Decode(string text, string salt);
        string Encode(string text, string salt);
        string GenerateRandomSalt(int length = 15);
        
    }
}
