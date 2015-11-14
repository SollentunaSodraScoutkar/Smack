/* =========================================================================
 * Copyright (C) 2007, Anokha AB - www.anokha.se
 * The program may be used and/or copied only with the written permission 
 * from ANOKHA AB, or in accordance with the terms and conditions stipulated 
 * in the agreement/contract under which the program has been supplied.
 * ========================================================================= */
using Smack.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Smack
{
    /// <summary>
    /// Class for simplifying the creation of a hash (message digest) from a message.
    /// (a one way function of the message, producing a small result)
    /// Currently this class only supports hashing of text strings.
    ///
    /// Note - not suitable for producing a hash of a large file.
    /// (for that we need to work with a file stream instead).
    /// </summary>
    /// <remarks>Author: Johan Parmar (2007-02-19)</remarks>
    public class OldSecurity : IPasswordHasher
    {

        #region private fields
        private static readonly Encoding defaultEncoding = Encoding.GetEncoding("ISO-8859-1");
        private static readonly HashAlgorithm defaultAlgorithm = new SHA256Managed();
        private Encoding _encoding;
        private HashAlgorithm _hashAlgorithm;
        #endregion

        #region properties
        public string AlgorithmName
        {
            get
            {
                if (_hashAlgorithm == null)
                {
                    return "null";
                }
                else
                {
                    return _hashAlgorithm.GetType().Name;
                }
            }
        }
        #endregion

        /// <summary>
        /// Creates a Hash instance with default HashAlgorithm of SHA-256 and the default encoding:ISO-8859-1
        /// </summary>
        public OldSecurity() : this(defaultAlgorithm, defaultEncoding) { }

        /// <summary>
        /// Creates a Hash instance with the default encoding:ISO-8859-1
        /// </summary>
        /// <param name="hashAlgorithm">The hashAlgorithm to use - dictates the cryptographical strength and size of the hash</param>
        public OldSecurity(HashAlgorithm hashAlgorithm) : this(hashAlgorithm, defaultEncoding) { }

        /// <summary>
        /// Uses specified HashAlgorithm and encoding.
        /// </summary>
        /// <param name="hashAlgorithm">The hashAlgorithm to use - dictates the cryptographical strength and size of the hash</param>
        /// <param name="encoding">The encoding for the message, salt and hash</param>
        public OldSecurity(HashAlgorithm hashAlgorithm, Encoding encoding)
        {
            _hashAlgorithm = hashAlgorithm;
            _encoding = encoding;
        }


        /// <summary>
        /// Computes a hash from the message and a randomly generated salt.
        /// </summary>
        /// <param name="message">The message to generate a hash from</param>
        /// <param name="salt">the randomly generated salt used to mix with the message before computing the hash</param>
        /// <returns>A hash of the specified message</returns>
        public string Compute(string message, out string salt)
        {
            salt = GenerateSalt();
            byte[] hash = Compute(ToByteArray(message), ToByteArray(salt));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Generates a hash from the message and the specified salt.
        /// </summary>
        /// <param name="message">The message to compute a hash from</param>
        /// <param name="salt">the salt used to mix with the message before computing the hash</param>
        /// <returns></returns>
        public string Compute(string message, string salt)
        {
            if (salt == null || "".Equals(salt))
            {
                throw new ArgumentException("EncryptPassword: Empty password salt is not allowed!");
            }
            byte[] hash = Compute(ToByteArray(message), ToByteArray(salt));
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Generates a hash from the message and the specified salt.
        /// </summary>
        /// <param name="message">The message to compute a hash from</param>
        /// <param name="salt">the salt used to mix with the message before computing the hash</param>
        /// <returns></returns>
        private byte[] Compute(byte[] message, byte[] salt)
        {
            byte[] saltExtendedMessage = new byte[message.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, saltExtendedMessage, 0, salt.Length);
            Buffer.BlockCopy(message, 0, saltExtendedMessage, salt.Length, message.Length);
            byte[] hash = _hashAlgorithm.ComputeHash(saltExtendedMessage);
            return hash;
        }

        private byte[] ToByteArray(string s)
        {
            return _encoding.GetBytes(s);
        }

        /// <summary>
        /// Generates a random salt for mixing with the password before hashing to 
        /// protect against from dictionary- and brute force attacks
        /// </summary>
        /// <returns></returns>
        private string GenerateSalt()
        {
            byte[] buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        public string CreateHash(string password)
        {
            //Mapping to new interface 
            throw new NotImplementedException();
        }

        public bool ValidatePassword(User unsecureUser, User realUser)
        {

            string passwordSalt = realUser.VarPwdSalt;
            string unsecureEncryptedPassword = Compute(unsecureUser.VarPassword, passwordSalt);
            if (realUser.VarPassword.Equals(unsecureEncryptedPassword))
            {
                return true;
            }

            return false;
        }

        public string CreateToken()
        {
            //Mapping to new interface
            throw new NotImplementedException();
        }
    }
}
