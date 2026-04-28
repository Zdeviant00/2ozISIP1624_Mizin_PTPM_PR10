using System;
using System.Text;

namespace MatrixCipher.Core
{
    /// <summary>
    /// Модуль дешифрования методом матричной перестановки.
    /// </summary>
    /// <remarks>
    /// Алгоритм: шифротекст записывается в матрицу по столбцам, 
    /// считывается по строкам. Замыкающие пробелы удаляются.
    /// </remarks>
    public static class MatrixDecryptor
    {
        /// <summary>
        /// Выполняет дешифрование строки, полученной методом <see cref="MatrixEncryptor.Encrypt"/>.
        /// </summary>
        /// <param name="encryptedText">Зашифрованная строка.</param>
        /// <param name="rows">Количество строк исходной матрицы (> 0).</param>
        /// <param name="cols">Количество столбцов исходной матрицы (> 0).</param>
        /// <returns>Исходный текст без замыкающих пробелов.</returns>
        /// <exception cref="ArgumentNullException">Если <paramref name="encryptedText"/> равен null.</exception>
        /// <exception cref="ArgumentException">Если длина строки не равна rows × cols.</exception>
        public static string Decrypt(string? encryptedText, int rows, int cols)
        {
            if (encryptedText is null) throw new ArgumentNullException(nameof(encryptedText));
            if (rows <= 0 || cols <= 0) throw new ArgumentException("Размеры матрицы должны быть > 0");
            if (encryptedText.Length != rows * cols) throw new ArgumentException("Длина шифротекста не соответствует размеру матрицы");

            char[,] matrix = new char[rows, cols];
            int idx = 0;

            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    matrix[r, c] = encryptedText[idx++];

            var sb = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    sb.Append(matrix[r, c]);

            return sb.ToString().TrimEnd();
        }
    }
}