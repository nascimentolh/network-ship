using System;
using System.Text;

namespace networkShip
{
    internal class Program
    {
        public static void Main()
        {
            var encryptedMessage = "10010110 11110111 01010110 00000001 00010111 00100110 01010111 00000001 00010111 01110110 01010111 00110110 11110111 11010111 01010111 00000011";
            var binaryMessage = encryptedMessage.Replace(" ", "");

            var decryptedMessage = Decrypt(binaryMessage);
            
            Console.WriteLine($"Mensagem Recebida: {decryptedMessage}");
        }

        private static byte InvertLastTwoBits(byte input)
        {
            return (byte)(input ^ 0b00000011);
        }

        private static byte Change4Bits(byte input)
        {
            return (byte)(((input & 0b11110000) >> 4) | ((input & 0b00001111) << 4));
        }

        private static string Decrypt(string binaryMessage)
        {
            if (binaryMessage.Length % 8 != 0)
            {
                throw new ArgumentException("Invalid Encrypted String");
            }

            var cipherBytes = new byte[binaryMessage.Length / 8];
            for (var i = 0; i < cipherBytes.Length; i++)
            {
                cipherBytes[i] = Convert.ToByte(binaryMessage.Substring(i * 8, 8), 2);
            }

            var decryptBytes = new byte[cipherBytes.Length];
            for (var i = 0; i < decryptBytes.Length; i++)
            {
                decryptBytes[i] = Change4Bits(InvertLastTwoBits(cipherBytes[i]));
            }

            var decryptedString = Encoding.ASCII.GetString(decryptBytes);

            return decryptedString;
        }
    }
}