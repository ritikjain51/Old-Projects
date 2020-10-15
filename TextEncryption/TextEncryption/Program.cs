using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\tWelcome to Dencryptor");
            for (int i = 0; i < 60; i++)
                Console.Write("--");
            L1:Console.WriteLine("\n\n1. Encryption\n2. Decryption");
            Console.WriteLine("Enter your Choice: ");
            int choice = Int32.Parse(Console.ReadLine());
            Console.Write("Enter the key: ");
            string key = Console.ReadLine();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the Plain Text: ");
                    string text = Console.ReadLine();
                    byte[] plainText = Encoding.GetEncoding("UTF-8").GetBytes(text.ToCharArray());
                    Idea id = new Idea(key, true);
                    id.crypt(plainText);
                    string cArray = System.Text.Encoding.ASCII.GetString(plainText).ToString();
                    Console.WriteLine("\n\n{0}", cArray);
                    break;
                case 2:
                    Console.WriteLine("Enter the cipher text:");
                    string ciphertext = Console.ReadLine();
                    byte[] cipher = Encoding.GetEncoding("UTF-8").GetBytes(ciphertext.ToCharArray());
                    Idea id1 = new Idea(key, false);
                    id1.crypt(cipher);
                    string c = System.Text.Encoding.ASCII.GetString(cipher).ToString();
                    Console.WriteLine("\n\n{0}", c);
                    break;
                default:
                    Console.WriteLine("\nSorry! You Choose a wrong option\nPlease try again.....\n\n");
                    goto L1;
            }

            Console.ReadKey();
        }
    }

    public class Idea
    {
        static int rounds = 8;
        int[] subKey;

        public Idea(string charKey, bool encrypt)
        {
            byte[] key = generateUserKeyFromCharKey(charKey);
            int[] tempSubKey = expandUserKey(key);
            if (encrypt)
            {
                subKey = tempSubKey;
            }
            else
            {
                subKey = invertSubKey(tempSubKey);
            }
        }
        public void crypt(byte[] data)
        {
            crypt(data, 0);
        }

        public void crypt(byte[] data, int dataPos)
        {
            int x0 = ((data[dataPos + 0] & 0xFF) << 8) | (data[dataPos + 1] & 0xFF);
            int x1 = ((data[dataPos + 2] & 0xFF) << 8) | (data[dataPos + 3] & 0xFF);
            int x2 = ((data[dataPos + 4] & 0xFF) << 8) | (data[dataPos + 5] & 0xFF);
            int x3 = ((data[dataPos + 6] & 0xFF) << 8) | (data[dataPos + 7] & 0xFF);
            int p = 0;
            for (int round = 0; round < rounds; round++)
            {
                int y0 = mul(x0, subKey[p++]);
                int y1 = add(x1, subKey[p++]);
                int y2 = add(x2, subKey[p++]);
                int y3 = mul(x3, subKey[p++]);

                int t0 = mul(y0 ^ y2, subKey[p++]);
                int t1 = add(y1 ^ y3, t0);
                int t2 = mul(t1, subKey[p++]);
                int t3 = add(t0, t2);

                x0 = y0 ^ t2;
                x1 = y2 ^ t2;
                x2 = y1 ^ t3;
                x3 = y3 ^ t3;
            }

            int r0 = mul(x0, subKey[p++]);
            int r1 = add(x2, subKey[p++]);
            int r2 = add(x1, subKey[p++]);
            int r3 = mul(x3, subKey[p++]);

            data[dataPos + 0] = (byte)(r0 >> 8);
            data[dataPos + 1] = (byte)r0;
            data[dataPos + 2] = (byte)(r1 >> 8);
            data[dataPos + 3] = (byte)r1;
            data[dataPos + 4] = (byte)(r2 >> 8);
            data[dataPos + 5] = (byte)r2;
            data[dataPos + 6] = (byte)(r3 >> 8);
            data[dataPos + 7] = (byte)r3;
        }


        private static int[] expandUserKey(byte[] userKey)
        {
            if (userKey.Length != 16)
            {
                throw new ArgumentException("Key length must be 128 bit", "key");
            }
            int[] key = new int[rounds * 6 + 4];
            for (int i = 0; i < userKey.Length / 2; i++)
            {
                key[i] = ((userKey[2 * i] & 0xFF) << 8) | (userKey[2 * i + 1] & 0xFF);
            }
            for (int i = userKey.Length / 2; i < key.Length; i++)
            {
                key[i] = ((key[(i + 1) % 8 != 0 ? i - 7 : i - 15] << 9) | (key[(i + 2) % 8 < 2 ? i - 14 : i - 6] >> 7)) & 0xFFFF;
            }
            return key;
        }

        private static int[] invertSubKey(int[] key)
        {
            int[] invKey = new int[key.Length];
            int p = 0;
            int i = rounds * 6;
            invKey[i + 0] = mulInv(key[p++]);
            invKey[i + 1] = addInv(key[p++]);
            invKey[i + 2] = addInv(key[p++]);
            invKey[i + 3] = mulInv(key[p++]);
            for (int r = rounds - 1; r >= 0; r--)
            {
                i = r * 6;
                int m = r > 0 ? 2 : 1;
                int n = r > 0 ? 1 : 2;
                invKey[i + 4] = key[p++];
                invKey[i + 5] = key[p++];
                invKey[i + 0] = mulInv(key[p++]);
                invKey[i + m] = addInv(key[p++]);
                invKey[i + n] = addInv(key[p++]);
                invKey[i + 3] = mulInv(key[p++]);
            }
            return invKey;
        }

        private static int add(int a, int b)
        {
            return (a + b) & 0xFFFF;
        }

        private static int mul(int a, int b)
        {
            long r = (long)a * b;
            if (r != 0)
            {
                return (int)(r % 0x10001) & 0xFFFF;
            }
            else
            {
                return (1 - a - b) & 0xFFFF;
            }
        }

        private static int addInv(int x)
        {
            return (0x10000 - x) & 0xFFFF;
        }

        private static int mulInv(int x)
        {
            if (x <= 1)
            {
                return x;
            }
            int y = 0x10001;
            int t0 = 1;
            int t1 = 0;
            while (true)
            {
                t1 += y / x * t0;
                y %= x;
                if (y == 1)
                {
                    return 0x10001 - t1;
                }
                t0 += x / y * t1;
                x %= y;
                if (x == 1)
                {
                    return t0;
                }
            }
        }
        
        private static byte[] generateUserKeyFromCharKey(String charKey)
        {
            int nofChar = 0x7E - 0x21 + 1;
            int[] a = new int[8];
            for (int p = 0; p < charKey.Length; p++)
            {
                int c = charKey[p];

                for (int i = a.Length - 1; i >= 0; i--)
                {
                    c += a[i] * nofChar;
                    a[i] = c & 0xFFFF;
                    c >>= 16;
                }
            }
            byte[] key = new byte[16];
            for (int i = 0; i < 8; i++)
            {
                key[i * 2] = (byte)(a[i] >> 8);
                key[i * 2 + 1] = (byte)a[i];
            }
            return key;
        }
    }
}
