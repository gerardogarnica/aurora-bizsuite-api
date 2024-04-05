using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Aurora.BizSuite.Security.Infrastructure.Cryptography;

public sealed class PasswordProvider : IPasswordProvider, IDisposable
{
    private const int IterationCount = 10000;
    private const int NumBytesRequested = 256 / 8;
    private const KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA512;
    private const int SaltSize = 128 / 8;

    private readonly RandomNumberGenerator _rng;

    public PasswordProvider() => _rng = RandomNumberGenerator.Create();

    public void Dispose() => _rng.Dispose();

    public string HashPassword(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        return Convert.ToBase64String(Encrypt(password));
    }

    public bool VerifyPassword(string userPassword, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(userPassword);
        ArgumentNullException.ThrowIfNull(providedPassword);

        byte[] decodedUserPassword = Convert.FromBase64String(userPassword);
        if (decodedUserPassword.Length == 0) return false;

        return Verify(decodedUserPassword, providedPassword);
    }

    private byte[] Encrypt(string password)
    {
        byte[] salt = GetRandomSalt();
        byte[] key = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, NumBytesRequested);
        byte[] outputBytes = new byte[salt.Length + key.Length];

        Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);
        Buffer.BlockCopy(key, 0, outputBytes, salt.Length, key.Length);

        return outputBytes;
    }

    private byte[] GetRandomSalt()
    {
        byte[] salt = new byte[SaltSize];
        _rng.GetBytes(salt);

        return salt;
    }

    private static bool Verify(byte[] hashedPassword, string providedPassword)
    {
        try
        {
            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPassword, 0, salt, 0, salt.Length);

            var subKeyLength = hashedPassword.Length - salt.Length;
            if (subKeyLength < SaltSize) return false;

            byte[] expectedSubKey = new byte[subKeyLength];
            Buffer.BlockCopy(hashedPassword, salt.Length, expectedSubKey, 0, expectedSubKey.Length);

            byte[] key = KeyDerivation.Pbkdf2(providedPassword, salt, Prf, IterationCount, subKeyLength);

            return ByteArraysEqual(key, expectedSubKey);
        }
        catch
        {
            return false;
        }
    }

    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null && b == null)
        {
            return true;
        }

        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }

        bool areSame = true;

        for (int i = 0; i < a.Length; i++)
        {
            areSame &= a[i] == b[i];
        }

        return areSame;
    }
}