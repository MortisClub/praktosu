// Назначение: окно с информацией о программе и разработчике.
// Дата создания: 14.09.2026
// Автор создания: Вагин Владислав Игоревич

using System.Windows;

namespace PRATKT1_vagin
{
    /// <summary>
    /// Окно «О программе» — выводит сведения о задании и разработчике.
    /// </summary>
    public partial class AboutWindow : Window
    {
        #region Конструктор

        public AboutWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Обработчики событий

        /// <summary>
        /// Закрывает окно «О программе».
        /// </summary>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}