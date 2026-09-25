namespace Lib_6
{
    /// <summary>
    /// Назначение: вычисление суммы элементов столбцов массива с четными номерами.
    /// Дата создания: 17.09.2026
    /// Автор создания: Вагин В.И. 
    /// </summary>
    public static class MatrixCalculator
    {
        /// <summary>
        /// Назначение: находит сумму элементов каждого столбца с четным номером (2, 4, ...).
        /// Номера столбцов считаются с единицы. Перебор выполняется с шагом 2,
        /// поэтому условный оператор не требуется.
        /// Входные параметры: matrix - массив.
        /// Выходные параметры: суммы по столбцам в порядке их следования.
        /// </summary>
        public static double[] GetEvenColumnSums(double[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);
            double[] sums = new double[columnCount / 2];

            for (int column = 1; column < columnCount; column += 2)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    sums[column / 2] += matrix[row, column];
                }
            }

            return sums;
        }
    }
}