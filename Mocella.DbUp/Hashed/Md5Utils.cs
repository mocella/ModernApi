using System;

namespace Mocella.DbUp.Hashed;

using System.Security.Cryptography;
using System.Text;

public class Md5Utils
{
    private const string CryptoType = "MD5";

    public static string Md5EncodeString(string input)
    {
        var encodedInput = new UTF8Encoding().GetBytes(input);
        var hash = ((HashAlgorithm)CryptoConfig.CreateFromName(CryptoType)).ComputeHash(encodedInput);
        var encoded = BitConverter.ToString(hash)
            .ToLower();

        return encoded;
    }
}