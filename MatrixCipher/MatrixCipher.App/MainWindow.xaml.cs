using System;
using System.Windows;
using MatrixCipher.Core;

namespace MatrixCipher.App
{
    /// <summary>
    /// Главное окно приложения для шифрования и дешифрования текста методом матричной перестановки.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Зашифровать".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события <see cref="RoutedEventArgs"/>.</param>
        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = TxtInput.Text;

                if (!int.TryParse(TxtRows.Text, out int rows) || !int.TryParse(TxtCols.Text, out int cols))
                {
                    MessageBox.Show("Параметры матрицы должны быть целыми числами!", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string result = MatrixEncryptor.Encrypt(input, rows, cols);
                TxtOutput.Text = result;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка шифрования",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Непредвиденная ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Расшифровать".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события <see cref="RoutedEventArgs"/>.</param>
        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = TxtInput.Text;

                if (!int.TryParse(TxtRows.Text, out int rows) || !int.TryParse(TxtCols.Text, out int cols))
                {
                    MessageBox.Show("Параметры матрицы должны быть целыми числами!", "Ошибка ввода",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string result = MatrixDecryptor.Decrypt(input, rows, cols);
                TxtOutput.Text = result;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка дешифрования",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить".
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события <see cref="RoutedEventArgs"/>.</param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtInput.Text = string.Empty;
            TxtOutput.Text = string.Empty;
            TxtRows.Text = "3";
            TxtCols.Text = "4";
            TxtInput.Focus();
        }
    }
}