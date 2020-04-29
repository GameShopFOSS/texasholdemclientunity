using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptAndDecryptAES : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       //string encryptstring = //Encrypt() 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    const string Key = "ABCDEFGHJKLMNOPQRSTUVWXYZABCDEFG";//ABCDEFGHJKLMNOPQRSTUVWXYZABCDEFG"; // must be 32 character
    const string IV = "ABCDEFGHIJKLMNOP"; // must be 16 character
    public string Encrypt(string message)
    {
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.IV = UTF8Encoding.UTF8.GetBytes(IV);
        aes.Key = UTF8Encoding.UTF8.GetBytes(Key);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        
        byte[] data = Encoding.UTF8.GetBytes(message);
        using (ICryptoTransform encrypt = aes.CreateEncryptor())
        {
            
            byte[] dest = encrypt.TransformFinalBlock(data, 0, data.Length);
            //string[] results = new string[2];
            //results[0] = Convert.ToBase64String(dest);
            //results[1] = "Hi";
            return Convert.ToBase64String(dest);
        }
    }

    public string Decrypt(string encryptedText)
    {
        string plaintext = null;
        using (AesManaged aes = new AesManaged())
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);
            byte[] aesIV = UTF8Encoding.UTF8.GetBytes(IV);
            byte[] aesKey = UTF8Encoding.UTF8.GetBytes(Key);
            ICryptoTransform decryptor = aes.CreateDecryptor(aesKey, aesIV);
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(cs))
                        plaintext = reader.ReadToEnd();
                }
            }
        }
        return plaintext;
    }
}
