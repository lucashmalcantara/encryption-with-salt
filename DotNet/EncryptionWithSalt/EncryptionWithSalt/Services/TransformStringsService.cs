using EncryptionWithSalt.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EncryptionWithSalt.Services
{
    public class TransformStringsService : ITransformStringsService
    {
        private readonly Random _random = new Random();

        public uint Key { get; private set; }
        public IEnumerable<int> SaltPositions { get; private set; }

        public TransformStringsService(uint key, IEnumerable<int> saltPositions)
        {
            Key = key;
            SaltPositions = saltPositions;
        }
        public string Decode(string text, string salt)
        {
            var fromBase64 = Convert.FromBase64String(text);
            var unescaped = Unescape(fromBase64);

            var flipped = Flip(unescaped, salt);
            return Encoding.UTF8.GetString(flipped, 0, flipped.Length);
        }

        public string Encode(string text, string salt)
        {
            var flipped = Flip(text, salt);
            return Convert.ToBase64String(flipped);
        }

        public string GenerateRandomSalt(int length = 15)
        {
            var randomSalt = new StringBuilder();

            for (int i = 0; i < length; i++)
                randomSalt.Append(
                    _random.Next(maxValue: 10).ToString());

            return randomSalt.ToString();
        }

        private uint CalculateFinalKey(string salt)
        {
            var finalKey = Key;

            foreach (var position in SaltPositions)
            {
                var saltItem = uint.Parse(salt[position].ToString());
                finalKey += saltItem;
            }

            return finalKey;
        }

        private string Unescape(byte[] fromBase64)
        {
            string textFromBase64 = Encoding.UTF8.GetString(fromBase64);
            return Regex.Unescape(textFromBase64.Replace(@"\", @"\\"));
        }

        private byte[] Flip(string text, string salt)
        {
            var finalKey = CalculateFinalKey(salt);

            var buffer = Encoding.UTF8.GetBytes(text);
            var k1 = finalKey & 0xff;
            var k2 = (finalKey >> 8) & 0xff;
            var k3 = (finalKey >> 16) & 0xff;
            var k4 = (finalKey >> 24) & 0xff;
            var flipped = new byte[buffer.Length];

            for (var i = 0; i < buffer.Length; i++)
            {
                var c = buffer[i];
                var temp = c ^ k1;
                temp ^= k2;
                temp ^= k3;
                temp ^= k4;
                flipped[i] = (byte)temp;
            }

            return flipped;
        }
    }
}
