using System;
using System.Text;

namespace MatrixCipher.Core
{
    /// <summary>
    /// Модуль шифрования методом матричной перестановки.
    /// </summary>
    /// <remarks>
    /// Алгоритм: исходный текст записывается в матрицу по строкам, 
    /// считывается по столбцам. Короткий текст дополняется пробелами.
    /// </remarks>
    public static class MatrixEncryptor
    {
        /// <summary>
        /// Выполняет шифрование текста заданным размером матрицы.
        /// </summary>
        /// <param name="text">Исходная строка для шифрования.</param>
        /// <param name="rows">Количество строк матрицы (> 0).</param>
        /// <param name="cols">Количество столбцов матрицы (> 0).</param>
        /// <returns>Зашифрованная строка длиной rows × cols.</returns>
        /// <exception cref="ArgumentNullException">Если <paramref name="text"/> равен null.</exception>
        /// <exception cref="ArgumentException">Если размеры матрицы ≤ 0 или текст пустой.</exception>
        public static string Encrypt(string? text, int rows, int cols)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));
            if (rows <= 0 || cols <= 0) throw new ArgumentException("Размеры матрицы должны быть > 0");
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Текст не может быть пустым");

            int size = rows * cols;
            char[,] matrix = new char[rows, cols];
            string padded = text.PadRight(size, ' ');
            int idx = 0;

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r, c] = padded[idx++];

            var sb = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    sb.Append(matrix[r, c]);

            return sb.ToString();
        }
    }
}