using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MatrixCipher.Tests
{
    [TestClass]
    public class MatrixCipherTests
    {
        [TestMethod]
        public void MatrixEncryptDecrypt_RoundTrip_ReturnsOriginal()
        {
            string original = "ПРИВЕТ МИР";
            int rows = 3, cols = 4;

            string encrypted = MatrixCipher.Core.MatrixEncryptor.Encrypt(original, rows, cols);
            string decrypted = MatrixCipher.Core.MatrixDecryptor.Decrypt(encrypted, rows, cols);

            Assert.AreEqual(original, decrypted, "Дешифрованный текст должен точно совпадать с исходным.");
        }

        [TestMethod]
        public void MatrixEncrypt_ShortText_PaddsSpacesCorrectly()
        {
            string original = "АБВ";
            int rows = 2, cols = 3;

            string encrypted = MatrixCipher.Core.MatrixEncryptor.Encrypt(original, rows, cols);
            string decrypted = MatrixCipher.Core.MatrixDecryptor.Decrypt(encrypted, rows, cols);

            Assert.AreEqual(rows * cols, encrypted.Length, "Длина шифротекста должна быть строго равна rows * cols.");
            Assert.AreEqual(original, decrypted, "При дешифровании замыкающие пробелы должны быть обрезаны.");
        }

        [TestMethod]
        [DataRow(-1, 3)]
        [DataRow(0, 2)]
        [DataRow(2, -5)]
        [DataRow(0, 0)]
        public void MatrixEncrypt_InvalidMatrixSize_ThrowsArgumentException(int rows, int cols)
        {
            try
            {
                MatrixCipher.Core.MatrixEncryptor.Encrypt("ТЕСТ", rows, cols);
                Assert.Fail("Ожидалось исключение ArgumentException, но его не было.");
            }
            catch (ArgumentException)
            {
                // Успех: исключение поймано
            }
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        public void MatrixEncrypt_EmptyOrNullText_ThrowsArgumentException(string? text)
        {
            try
            {
                MatrixCipher.Core.MatrixEncryptor.Encrypt(text, 2, 2);
                Assert.Fail("Ожидалось исключение ArgumentException, но его не было.");
            }
            catch (ArgumentException)
            {
                // Успех
            }
        }

        [TestMethod]
        public void MatrixEncrypt_1xNMatrix_ReturnsOriginalText()
        {
            string original = "ПРОВЕРКА";
            int rows = 1, cols = 8;

            string encrypted = MatrixCipher.Core.MatrixEncryptor.Encrypt(original, rows, cols);
            string decrypted = MatrixCipher.Core.MatrixDecryptor.Decrypt(encrypted, rows, cols);

            Assert.AreEqual(original, decrypted);
            Assert.AreEqual(original, encrypted.TrimEnd());
        }

        [TestMethod]
        public void MatrixEncrypt_Nx1Matrix_ReturnsOriginalText()
        {
            string original = "12345";
            int rows = 5, cols = 1;

            string encrypted = MatrixCipher.Core.MatrixEncryptor.Encrypt(original, rows, cols);
            string decrypted = MatrixCipher.Core.MatrixDecryptor.Decrypt(encrypted, rows, cols);

            Assert.AreEqual(original, decrypted);
            Assert.AreEqual(original, encrypted.TrimEnd());
        }

        [TestMethod]
        public void MatrixEncrypt_SpecialCharsAndSpaces_RoundTripSuccessful()
        {
            string original = "Hello World 123!";
            int rows = 4, cols = 4;

            string encrypted = MatrixCipher.Core.MatrixEncryptor.Encrypt(original, rows, cols);
            string decrypted = MatrixCipher.Core.MatrixDecryptor.Decrypt(encrypted, rows, cols);

            Assert.AreEqual(original, decrypted, "Текст со спецсимволами и пробелами должен восстанавливаться без потерь.");
        }
    }
}