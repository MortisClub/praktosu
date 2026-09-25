// Назначение: вспомогательные модули для работы с массивами целых чисел:
//              ввод, вывод, заполнение, сохранение, загрузка, сортировка и очистка.
// Дата создания: 14.09.2026
// Автор создания: Вагин Владислав Игоревич

namespace LibMas
{
    /// <summary>
    /// Базовые модули для работы с массивами: ввод, вывод,
    /// заполнение, сохранение, загрузка, сортировка и очистка.
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Заполняет массив случайными числами в заданном диапазоне.
        /// </summary>
        /// <param name="size">Размер массива (должен быть > 0).</param>
        /// <param name="minValue">Минимальное значение (включительно).</param>
        /// <param name="maxValue">Максимальное значение (включительно).</param>
        /// <returns>Новый массив, заполненный случайными числами.</returns>
        public static int[] FillRandom(int size, int minValue = 0, int maxValue = 100)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Размер массива должен быть положительным.");
            }

            var rng = Random.Shared;
            var arr = new int[size];

            for (int i = 0; i < size; i++)
            {
                arr[i] = rng.Next(minValue, maxValue + 1);
            }

            return arr;
        }

        /// <summary>
        /// Заполняет массив нулями (очищает содержимое).
        /// </summary>
        /// <param name="arr">Массив для очистки.</param>
        public static void Clear(int[] arr)
        {
            if (arr == null)
            {
                return;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
        }

        /// <summary>
        /// Сортирует массив по возрастанию (алгоритм пузырьком).
        /// </summary>
        /// <param name="arr">Массив для сортировки.</param>
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Разворачивает массив в обратном порядке.
        /// </summary>
        /// <param name="arr">Массив для разворота.</param>
        public static void Reverse(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            int left = 0;
            int right = arr.Length - 1;

            while (left < right)
            {
                (arr[left], arr[right]) = (arr[right], arr[left]);
                left++;
                right--;
            }
        }

        /// <summary>
        /// Возвращает строковое представление массива (элементы через запятую).
        /// </summary>
        /// <param name="arr">Массив.</param>
        /// <returns>Строка вида "1, 2, 3" или "(пусто)".</returns>
        public static string ArrayToString(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                return "(пусто)";
            }

            return string.Join(", ", arr);
        }

        /// <summary>
        /// Сохраняет массив в текстовый файл: по одному числу на строку.
        /// </summary>
        /// <param name="arr">Массив для сохранения.</param>
        /// <param name="path">Путь к файлу.</param>
        public static void SaveToFile(int[] arr, string path)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr));
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Путь к файлу не может быть пустым.", nameof(path));
            }

            File.WriteAllLines(path, arr.Select(x => x.ToString()));
        }

        /// <summary>
        /// Загружает массив из текстового файла (одно число на строку).
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Прочитанный массив целых чисел.</returns>
        public static int[] LoadFromFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Путь к файлу не может быть пустым.", nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Файл не найден.", path);
            }

            var lines = File.ReadAllLines(path);
            var result = new List<int>();

            foreach (var line in lines)
            {
                if (int.TryParse(line.Trim(), out int n))
                {
                    result.Add(n);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Парсит строку с числами в массив. Разделители: запятая, точка,
        /// точка с запятой или пробел.
        /// </summary>
        /// <param name="input">Строка с числами.</param>
        /// <returns>Распознанный массив; если ни одно число не распознано — пустой массив.</returns>
        public static int[] ParseFromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Array.Empty<int>();
            }

            var tokens = input.Split(new[] { ',', ';', '.', ' ', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            var result = new List<int>();

            foreach (var token in tokens)
            {
                if (int.TryParse(token.Trim(), out int n))
                {
                    result.Add(n);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Находит сумму целых чисел, которые меньше 15.
        /// </summary>
        /// <param name="numbers">Последовательность целых чисел.</param>
        /// <returns>Сумма чисел, значение которых меньше 15.</returns>
        public static int SumLessThan15(IEnumerable<int> numbers)
        {
            int sum = 0;

            foreach (int n in numbers)
            {
                if (n < 15)
                {
                    sum += n;
                }
            }

            return sum;
        }

        /// <summary>
        /// Создаёт новый массив заданного размера, заполненный нулями.
        /// </summary>
        /// <param name="size">Размер массива (должен быть > 0).</param>
        /// <returns>Новый массив нулей.</returns>
        public static int[] CreateEmpty(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Размер массива должен быть положительным.");
            }

            return new int[size];
        }
    }
}