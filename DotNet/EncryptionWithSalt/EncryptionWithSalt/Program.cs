using EncryptionWithSalt.Services;
using System;

namespace EncryptionWithSalt
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowHeader();
            DoEncrypt();
            Console.ReadKey();
        }

        private static void DoEncrypt()
        {
            const string someText = "Hello! My name is Lucas.";
            
            const uint key = 1597534560;
            var saltPositions = new int[] { 2, 3, 5, 7, 11 };
            var transformStringsService = new TransformStringsService(key, saltPositions);

            const string randomNumbers = "9563216516215646516516518967";
            var encodedText = transformStringsService.Encode(someText, randomNumbers);
            var decodedText = transformStringsService.Decode(encodedText, randomNumbers);

            Console.WriteLine($"Text to encrypt: {someText}");
            Console.WriteLine($"Private Key used to encrypt: {key}");
            Console.WriteLine($"SALT Positions (used to build the Final Key): {string.Join(',', saltPositions)}");
            Console.WriteLine($"Encoded text: {encodedText}");
            Console.WriteLine($"Decoded text: {decodedText}");
        }

        private static void ShowHeader()
        {
            Console.WriteLine("=== Encryption With Salt ===");
            Console.WriteLine("Show how to encrypt a string with obfuscation using XOR + SALT technique.\r\n");
        }
    }
}
