using System;
using System.Text;

namespace MatrixCipher.Core
{
    /// <summary>
    /// Реализация матричного перестановочного шифра (Вариант 4).
    /// Текст записывается в матрицу по строкам, считывается по столбцам.
    /// </summary>
    public static class MatrixCipher
    {
        /// <summary>
        /// Шифрует исходный текст методом матричной перестановки.
        /// </summary>
        public static string MatrixEncrypt(string? text, int rows, int cols)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text), "Текст не может быть null");

            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Количество строк и столбцов должно быть больше 0");

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Текст не может быть пустым");

            int matrixSize = rows * cols;
            string paddedText = text.PadRight(matrixSize, ' ');
            char[,] matrix = new char[rows, cols];

            int index = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r, c] = paddedText[index++];

            var result = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    result.Append(matrix[r, c]);

            return result.ToString();
        }

        /// <summary>
        /// Дешифрует текст, полученный методом <see cref="MatrixEncrypt(string, int, int)"/>.
        /// </summary>
        public static string MatrixDecrypt(string? encryptedText, int rows, int cols)
        {
            if (encryptedText is null)
                throw new ArgumentNullException(nameof(encryptedText), "Зашифрованный текст не может быть null");

            if (rows <= 0 || cols <= 0)
                throw new ArgumentException("Количество строк и столбцов должно быть больше 0");

            if (encryptedText.Length != rows * cols)
                throw new ArgumentException($"Длина зашифрованного текста должна быть равна {rows * cols}");

            char[,] matrix = new char[rows, cols];
            int index = 0;
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    matrix[r, c] = encryptedText[index++];

            var result = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    result.Append(matrix[r, c]);

            return result.ToString().TrimEnd();
        }
    }
}