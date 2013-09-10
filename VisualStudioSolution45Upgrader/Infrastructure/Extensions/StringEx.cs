using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioSolution45Upgrader.Infrastructure.Extensions
{
    public static class StringEx
    {
        public static string GetMd5(this string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static byte[] GetHash(this string input)
        {
            //Create a new instance of the UnicodeEncoding class to  
            //convert the string into an array of Unicode bytes.
            UnicodeEncoding UE = new UnicodeEncoding();

            //Convert the string into an array of bytes. 
            byte[] MessageBytes = UE.GetBytes(input);

            //Create a new instance of the SHA1Managed class to create  
            //the hash value.
            SHA1Managed SHhash = new SHA1Managed();

            //Create the hash value from the array of bytes.
            var hashValue = SHhash.ComputeHash(MessageBytes);
            return hashValue;
        }
    }
}
