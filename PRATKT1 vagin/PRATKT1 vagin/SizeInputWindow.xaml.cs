// Назначение: диалоговое окно для ввода размера массива.
// Дата создания: 14.09.2026
// Автор создания: Вагин Владислав Игоревич

using System.Windows;
using System.Windows.Input;

namespace PRATKT1_vagin
{
    /// <summary>
    /// Диалоговое окно, запрашивающее у пользователя размер массива.
    /// </summary>
    public partial class SizeInputWindow : Window
    {
        #region Свойства

        /// <summary>
        /// Введённый пользователем размер массива.
        /// </summary>
        public int Size { get; private set; }

        #endregion

        #region Конструктор

        public SizeInputWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Обработчики событий

        /// <summary>
        /// Проверяет корректность ввода и подтверждает размер массива.
        /// </summary>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbSize.Text, out int size) && size > 0)
            {
                Size = size;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show(
                    "Введите целое число больше нуля.",
                    "Размер массива",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Закрывает окно без сохранения введённого значения.
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// При нажатии Enter в поле ввода подтверждает введённый размер.
        /// </summary>
        private void tbSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOk_Click(sender, e);
            }
        }

        #endregion
    }
}