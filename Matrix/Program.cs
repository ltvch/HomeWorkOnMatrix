using System;

namespace TwoDimensionalArray
{ 

    internal class Program
    {
        const int HEIGTH = 5;
        const int WIDTH = 5;
        const int RANDOM_MAX = 10;

        #region SERVICE METHODS
        /// <summary>
        /// Построить и заполнить массив рандомными числами.
        /// 
        /// </summary>
        /// <param name="rows">Кол-во строк</param>
        /// <param name="columns">Кол-во столбцов</param>
        /// <param name="range">Максимальное допустимое число в рандоме.</param>
        /// <returns>Заполненный рандомными числами массив.</returns>
        /// <remarks>Закоментировать вывод в консоль для унификации</remarks>
        private static int[,] RandomArray(int rows, int columns, int range)
        {
            //todo Закоментировать вывод в консоль для унификации
            int[,] matrix = new int[rows, columns];
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rnd.Next(range);
                    Console.Write("{0,4}", matrix[i, j]);
                }
                Console.WriteLine();
            }

            return matrix;
        }

        /// <summary>
        /// Показать двумерный массив в консоли.
        /// </summary>
        /// <param name="matrix">Двумерный массив для демонстрации</param>
        private static void ShowMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write("{0,4}", matrix[i, j]);
                Console.WriteLine();
            }
        }
        #endregion

        /// <summary>
        /// Заполняем двумерный массив "улиткой" считая посещенные ячейки
        /// </summary>
        /// <param name="rows">"Количество строк" в двумерном массиве </param>
        /// <param name="columns">"Количество столбцов" в двумерном массиве</param>
        /// <returns>Массив с заполненными значениями</returns>
        public static int[,] Helix(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];

            int row = 0;
            int column = 0;
            int dx = 1;
            int dy = 0;
            int dirChanges = 0;
            int visits = columns;

            for (int i = 0; i < rows * columns; i++)
            {
                matrix[row,column] = i + 1;
                if (--visits == 0)
                {
                    visits = columns * (dirChanges % 2) +
                        rows * ((dirChanges + 1) % 2) -
                        (dirChanges / 2 - 1) - 2;
                    int temp = dx;
                    dx = -dy;
                    dy = temp;
                    dirChanges++;
                }
                column += dx;
                row += dy;
            }

            return matrix;
        }

        /// <summary>
        /// Заполняем двумерный массив "улиткой" примитивным способом
        /// то есть считаем количество элементов и поворотов наглядным алгоритмом.
        /// </summary>
        /// <param name="rows">"Количество строк" в двумерном массиве </param>
        /// <param name="columns">"Количество столбцов" в двумерном массиве</param>
        /// <returns>Массив с заполненными значениями</returns>
        private static int[,] HelixEasy(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];

            int row = 0;
            int col = 0;
            string direction = "right";
            int maxRotations = rows * columns;

            for (int i = 1; i <= maxRotations; i++)
            {
                if (direction == "right" && (col > columns - 1 || matrix[row, col] != 0))
                {
                    direction = "down";
                    col--;
                    row++;
                }
                if (direction == "down" && (row > rows - 1 || matrix[row, col] != 0))
                {
                    direction = "left";
                    row--;
                    col--;
                }
                if (direction == "left" && (col < 0 || matrix[row, col] != 0))
                {
                    direction = "up";
                    col++;
                    row--;
                }

                if (direction == "up" && row < 0 || matrix[row, col] != 0)
                {
                    direction = "right";
                    row++;
                    col++;
                }

                matrix[row, col] = i;

                switch (direction)
                {
                    case "right":
                        col++;
                        break;

                    case "down":
                        row++;
                        break;
                    case "left":
                        col--;
                        break;
                    case "up":
                        row--;
                        break;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Заполняем двумерный массив "зигзагом" с левого верхнего угла.
        /// </summary>
        /// <param name="rows">"Количество строк" в двумерном массиве </param>
        /// <param name="columns">"Количество столбцов" в двумерном массиве</param>
        /// <returns>Масив с заполненными значениями</returns>
        public static int[,] ZigZag(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];
            int i = 0,
                j = 0,
                d = -1; // -1 for top-right move, +1 for bottom-left move
            int start = 0, 
                end = rows * columns - 1;

            do
            {
                matrix[i, j] = start++;// увеличим значение стартовой точки на единицу
                matrix[rows - i - 1, columns - j - 1] = end--;// уменьшим значение елемента( налево вниз на один елемент) на единицу
                i += d;
                j -= d;

                if (i < 0)
                {
                    i++; //увеличим верх
                    d = -d; // перевернем значение
                }
                else if (j < 0)
                {
                    j++; //увеличим счетчик налево
                    d = -d; //перевернем значение
                }
            }
            while (start < end);

            if (start == end)
                matrix[i, j] = start;

            return matrix;
        }

        /// <summary>
        /// Заполняем двумерный массив "змейкой" с левого верхнего угла.
        /// </summary>
        /// <param name="rows">"Количество строк" в двумерном массиве </param>
        /// <param name="columns">"Количество столбцов" в двумерном массиве</param>
        /// <returns>Масив с заполненными значениями</returns>
        private static int[,] Snake(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    matrix[i, j] = i * columns + (i % 2 == 0 ? j : columns - j - 1);

            return matrix;
        }

        /// <summary>
        /// Поменять местами строки, содержащие максимальный и минимальный элемент.
        /// Если min и max элементы в одной строке, то поменять местами столбцы
        /// </summary>
        /// <param name="rows">"Количество строк" в двумерном массиве </param>
        /// <param name="columns">"Количество столбцов" в двумерном массиве</param>
        /// <returns>Масив с заполненными значениями</returns>
        private static int[,] RotateMaxMin(int rows, int columns)
        {
            int[,] array = new int[rows, columns];
            int minRow = 0, maxRow = 0;

            void MaxMin(int[,] matrix)
            {
                int min = matrix[1, 1];
                int max = matrix[1, 1];
                
                // определяем минимальную строку
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (matrix[i, j] < min)
                        {
                            min = matrix[i, j];
                            minRow = i;
                        }
                    }
                }

                // определяем максимальную строку
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (matrix[i, j] > max)
                        {
                            max = matrix[i, j];
                            maxRow = i;
                        }
                    }
                }
            }

            void ChangeRow(int[,] matrix)
            {
                // перестановка строк
                int[] matrixTemp = new int[matrix.GetLength(0)];
                
                //Заносим минимальную строку во временный массив
                for (int i = minRow, j = 0, k = 0; j < 5; j++, k++)
                {
                    matrixTemp[k] = matrix[i, j];
                }
                //Заменяем минимальную строку максимальной
                for (int j = 0; j < 5; j++)
                {
                    matrix[minRow, j] = matrix[maxRow, j];
                }
                //Заменяем максимальную строку минимальной
                for (int j = 0; j < 5; j++)
                {
                    matrix[maxRow, j] = matrixTemp[j];
                }
            }

            array = RandomArray(rows, columns, RANDOM_MAX);
            Console.WriteLine();
            MaxMin(array);
            Console.ForegroundColor = ConsoleColor.Gray;
            ChangeRow(array);

            return array;
        }

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Helix matrix is :");            
            ShowMatrix(Helix(HEIGTH, WIDTH));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Helix(Easy way) matrix is :");
            ShowMatrix(HelixEasy(HEIGTH, WIDTH));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("ZigZag matrix is :");
            ShowMatrix(ZigZag(HEIGTH, WIDTH));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Snake matrix is :");
            ShowMatrix(Snake(HEIGTH, WIDTH));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Rotate row where min and max value in matrix is :");
            ShowMatrix(RotateMaxMin(HEIGTH, WIDTH));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress any key to continue..");
            Console.ReadKey();
        }       
    }
}
