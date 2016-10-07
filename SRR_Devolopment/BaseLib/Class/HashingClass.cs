using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace SRR_Devolopment.BaseLib.Class
{
    class HashingClass
    {
        /// <summary>
        /// Static Function for MD5 Hashed
        /// </summary>
        /// <param name="toBeHashed">Plain Password</param>
        /// <returns>Hashed String</returns>
        public static String getHash(string toBeHashed)
        {
           String ret;
           using (MD5 hash = MD5.Create())
            {
                
                ret = getHashed(hash, toBeHashed);
              
            }
           return ret;
        }

        private static String getHashed(MD5 objectMD,String dataInsert)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = objectMD.ComputeHash(Encoding.UTF8.GetBytes(dataInsert));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        
    }
}
