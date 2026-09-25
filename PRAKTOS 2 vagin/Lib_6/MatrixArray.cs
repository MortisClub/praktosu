using System.Globalization;

namespace Lib_6
{
    /// <summary>
    /// Назначение: базовые операции над массивом (заполнение, очистка, сохранение и загрузка).
    /// Дата создания: 17.09.2026
    /// Автор создания: Вагин В.И. 
    /// </summary>
    public static class MatrixArray
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Назначение: заполняет массив случайными значениями из заданного диапазона.
        /// Входные параметры: matrix - массив, minValue и maxValue - границы диапазона.
        /// </summary>
        public static void Fill(double[,] matrix, double minValue, double maxValue)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    matrix[row, column] = minValue + _random.NextDouble() * (maxValue - minValue);
                }
            }
        }

        /// <summary>
        /// Назначение: обнуляет все элементы массива.
        /// Входные параметры: matrix - массив.
        /// </summary>
        public static void Clear(double[,] matrix)
        {
            Array.Clear(matrix);
        }

        /// <summary>
        /// Назначение: сохраняет массив в текстовый файл.
        /// Первая строка файла содержит размеры массива, далее следуют значения по строкам.
        /// Входные параметры: matrix - массив, fileName - имя файла.
        /// </summary>
        public static void Save(double[,] matrix, string fileName)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(string.Format("{0} {1}", rowCount, columnCount));

                for (int row = 0; row < rowCount; row++)
                {
                    string line = "";

                    for (int column = 0; column < columnCount; column++)
                    {
                        line += matrix[row, column].ToString(CultureInfo.InvariantCulture) + " ";
                    }

                    writer.WriteLine(line.TrimEnd());
                }
            }
        }

        /// <summary>
        /// Назначение: загружает массив из текстового файла, сформированного методом Save.
        /// Входные параметры: fileName - имя файла.
        /// Выходные параметры: загруженный массив.
        /// </summary>
        public static double[,] Open(string fileName)
        {
            double[,] matrix;
            string[] lines = File.ReadAllLines(fileName);
            string[] sizes = lines[0].Split(' ');

            int rowCount = int.Parse(sizes[0]);
            int columnCount = int.Parse(sizes[1]);
            matrix = new double[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                string[] values = lines[row + 1].Split(' ');

                for (int column = 0; column < columnCount; column++)
                {
                    matrix[row, column] = double.Parse(values[column], CultureInfo.InvariantCulture);
                }
            }

            return matrix;
        }
    }
}