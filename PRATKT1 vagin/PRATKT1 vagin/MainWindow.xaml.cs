// Назначение: главное окно приложения для ввода массива целых чисел
//              и вычисления суммы элементов, меньших 15.
// Дата создания: 14.09.2026
// Автор создания: Вагин Владислав Игоревич

using System.Windows;
using Microsoft.Win32;
using LibMas;

namespace PRATKT1_vagin
{
    /// <summary>
    /// Главное окно приложения: ввод, загрузка, сохранение массива
    /// и вычисление суммы чисел меньше 15.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Поля

        // Текущий массив целых чисел, с которым работают обработчики.
        private int[] _numbers = Array.Empty<int>();

        // Фильтр файловых диалогов для текстовых файлов.
        private const string FileFilter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

        #endregion

        #region Конструктор

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Обработчики событий

        /// <summary>
        /// Запрашивает размер массива и заполняет его случайными числами от 0 до 100.
        /// </summary>
        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            var sizeWindow = new SizeInputWindow { Owner = this };

            if (sizeWindow.ShowDialog() != true)
            {
                return;
            }

            _numbers = ArrayHelper.FillRandom(sizeWindow.Size, 0, 100);
            tbInput.Text = ArrayHelper.ArrayToString(_numbers);
            lResult.Text = $"Сформировано {_numbers.Length} случайных чисел";
        }

        /// <summary>
        /// Очищает текущий массив и поле ввода.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ArrayHelper.Clear(_numbers);
            tbInput.Text = string.Empty;
            lResult.Text = "Массив очищен";
        }

        /// <summary>
        /// Открывает диалог выбора файла и загружает массив из текстового файла.
        /// </summary>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Открыть массив",
                Filter = FileFilter,
                DefaultExt = ".txt"
            };

            if (dialog.ShowDialog(this) != true)
            {
                return;
            }

            try
            {
                _numbers = ArrayHelper.LoadFromFile(dialog.FileName);
                tbInput.Text = ArrayHelper.ArrayToString(_numbers);
                lResult.Text = $"Загружено чисел: {_numbers.Length}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось открыть файл:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Сохраняет текущий массив в текстовый файл через диалог сохранения.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _numbers = ArrayHelper.ParseFromString(tbInput.Text);

            if (_numbers.Length == 0)
            {
                MessageBox.Show(
                    "Нет чисел для сохранения.",
                    "Сохранить",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var dialog = new SaveFileDialog
            {
                Title = "Сохранить массив",
                Filter = FileFilter,
                DefaultExt = ".txt",
                FileName = "massiv.txt"
            };

            if (dialog.ShowDialog(this) != true)
            {
                return;
            }

            try
            {
                ArrayHelper.SaveToFile(_numbers, dialog.FileName);
                lResult.Text = $"Сохранено чисел: {_numbers.Length}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось сохранить файл:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Считывает числа из текстового поля и выводит сумму тех, что меньше 15.
        /// </summary>
        private void btnCal_Click(object sender, RoutedEventArgs e)
        {
            _numbers = ArrayHelper.ParseFromString(tbInput.Text);

            if (_numbers.Length == 0)
            {
                lResult.Text = "Не удалось распознать ни одного числа";
                return;
            }

            int sum = ArrayHelper.SumLessThan15(_numbers);
            lResult.Text = $"Введено чисел: {_numbers.Length}\nСумма чисел меньше 15: {sum}";
        }

        /// <summary>
        /// Открывает окно с информацией о программе.
        /// </summary>
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutWindow { Owner = this };
            about.ShowDialog();
        }

        /// <summary>
        /// Закрывает главное окно приложения.
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}