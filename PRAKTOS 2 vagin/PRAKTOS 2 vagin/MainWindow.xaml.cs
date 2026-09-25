using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Lib_6;
using Microsoft.Win32;

namespace PRAKTOS_2_vagin
{
    /// <summary>
    /// Назначение: главное окно программы — работа с матрицей и расчет сумм четных столбцов.
    /// Дата создания: 17.09.2026
    /// Автор создания: Вагин В.И. 
    /// </summary>
    public partial class MainWindow : Window
    {
        private double[,]? _matrix;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Назначение: заполняет матрицу случайными значениями и выводит ее на экран.
        /// </summary>
        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            int rowCount;

            if (!int.TryParse(tbRowCount.Text, out rowCount))
            {
                MessageBox.Show("Неверно указано количество строк.", "Ошибка");
                return;
            }

            int columnCount;

            if (!int.TryParse(tbColumnCount.Text, out columnCount))
            {
                MessageBox.Show("Неверно указано количество столбцов.", "Ошибка");
                return;
            }

            if (rowCount <= 0 || columnCount <= 0)
            {
                MessageBox.Show("Размеры матрицы должны быть положительными числами.", "Ошибка");
                return;
            }

            _matrix = new double[rowCount, columnCount];
            MatrixArray.Fill(_matrix, -10, 10);
            ShowMatrix(_matrix);
            lbSums.Items.Clear();
        }

        /// <summary>
        /// Назначение: обнуляет элементы матрицы.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (_matrix == null)
            {
                MessageBox.Show("Матрица не заполнена.", "Ошибка");
                return;
            }

            MatrixArray.Clear(_matrix);
            ShowMatrix(_matrix);
            lbSums.Items.Clear();
        }

        /// <summary>
        /// Назначение: сохраняет матрицу в выбранный файл.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_matrix == null)
            {
                MessageBox.Show("Матрица не заполнена.", "Ошибка");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                MatrixArray.Save(_matrix, dialog.FileName);
            }
        }

        /// <summary>
        /// Назначение: загружает матрицу из выбранного файла и выводит ее на экран.
        /// </summary>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                _matrix = MatrixArray.Open(dialog.FileName);
                ShowMatrix(_matrix);
                lbSums.Items.Clear();
            }
        }

        /// <summary>
        /// Назначение: вычисляет суммы элементов четных столбцов и выводит результат.
        /// </summary>
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (_matrix == null)
            {
                MessageBox.Show("Матрица не заполнена.", "Ошибка");
                return;
            }

            double[] sums = MatrixCalculator.GetEvenColumnSums(_matrix);

            lbSums.Items.Clear();

            for (int i = 0; i < sums.Length; i++)
            {
                lbSums.Items.Add(string.Format("Столбец {0}: {1:F2}", (i + 1) * 2, sums[i]));
            }
        }

        /// <summary>
        /// Назначение: выводит сведения о разработчике и задании.
        /// </summary>
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            string about = "Разработчик: Вагин В.И.\n" +
                           "Группа: ИСП-31\n" +
                           "Работа № 6, вариант 6\n\n" +
                           "Задание: дана матрица размера M x N. Для каждого столбца " +
                           "матрицы с четным номером (2, 4, ...) найти сумму его элементов. " +
                           "Условный оператор не использовать.";

            MessageBox.Show(about, "О программе");
        }

        /// <summary>
        /// Назначение: закрывает окно программы.
        /// </summary>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Назначение: выводит матрицу в таблицу, заголовки столбцов — их номера.
        /// Входные параметры: matrix - матрица для отображения.
        /// </summary>
        private void ShowMatrix(double[,] matrix)
        {
            int columnCount = matrix.GetLength(1);

            dgMatrix.Columns.Clear();
            dgMatrix.ItemsSource = null;

            for (int column = 0; column < columnCount; column++)
            {
                dgMatrix.Columns.Add(new DataGridTextColumn
                {
                    Header = string.Format("Столбец {0}", column + 1),
                    Binding = new Binding(string.Format("[{0}]", column))
                });
            }

            dgMatrix.ItemsSource = ToRows(matrix);
        }

        /// <summary>
        /// Назначение: преобразует матрицу в список строк для отображения в таблице.
        /// Входные параметры: matrix - матрица для преобразования.
        /// Выходные параметры: список строк матрицы.
        /// </summary>
        private List<double[]> ToRows(double[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);
            List<double[]> rows = new List<double[]>();

            for (int row = 0; row < rowCount; row++)
            {
                double[] values = new double[columnCount];

                for (int column = 0; column < columnCount; column++)
                {
                    values[column] = matrix[row, column];
                }

                rows.Add(values);
            }

            return rows;
        }
    }
}