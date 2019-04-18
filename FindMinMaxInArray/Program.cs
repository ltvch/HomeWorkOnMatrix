using System;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// CASE реализовать несколько типов сортировки при этом БЫСТРАЯ сортировка обязательна
/// Показать количество перемещений и количество сравнений елементов массива.
/// 
/// Проверить на массиве с случайными числами , на массиве с частично отсортированным по возрастающей и по убывающей от начала массива (3 шт).
/// </summary>

namespace SortInArray
{
    internal class Program
    {
        static int CountMove;
        static int CountCompare;

        const int MINVALUE = 0;
        const int MAXVALUE = 50;
        const int SIZE = 10;

        /// <summary>
        /// Создаем массив целых чисел с случайными данными
        /// </summary>
        /// <returns>Return fill array of integers</returns>
        private static int[] RandomArray()
        {
            Random randNum = new Random();
            return Enumerable
                .Repeat(0, SIZE)
                .Select(i => randNum.Next(MINVALUE, MAXVALUE))
                .ToArray();
        }
       
        /// <summary>
        /// Swap two values use XOR
        /// </summary>
        /// <param name="x">First changed value</param>
        /// <param name="y">Second changed value</param>
        private static void Swap(ref int x, ref int y)
        {
            //x ^= y; y ^= x; x ^= y;//not work in QuickSort
            int tmp = x;
            x = y;
            y = tmp;
            CountMove++;
        }

        /// <summary>
        /// Гномья сортировка - "простая и очевидная".
        /// Смотрим на следующий и предыдущий елементы массива: если они в правильном порядке(предыдущий меньше следующего), шаг на один елемент вперёд,
        /// иначе меняем их местами и шагает на один елемент назад. 
        /// Граничные условия: если нет предыдущего елемента, шаг вперёд; если нет следующего елемента - закончили.
        /// </summary>
        /// <param name="array">Sorted array</param>
        private static void GnomeSort(int[] array)
        {
            for (int i = 1; i < array.Length;)
            {
                if (array[i - 1] <= array[i])
                {
                    CountCompare++;//only counter compare not need base code;
                    i++;
                }
                else
                {
                    Swap(ref array[i], ref array[i - 1]);
                    --i;
                    if (i == 0)
                    {
                        i = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Booble Sort
        /// Если элемент слева больше, чем элемент справа, (array[i] > array[i+1]), такие элементы надо поменять местами.
        /// </summary>
        /// <param name="array">Sorted array</param>
        private static void BoobleSort(int[] array)
        {
            bool isSorted = true;
            while (isSorted)//we think array is sorted by check it;
            {
                isSorted = false;//if insert think array is not sorted
                for (int i = 0; i < array.Length - 1; ++i)
                {
                    if (array[i] > array[i + 1])
                    {
                        CountCompare++;//only counter compare not need base code;
                        Swap(ref array[i], ref array[i + 1]);
                        isSorted = true;//think now array is sorted
                    }
                }
            }
        }

        /// <summary>
        /// Шейкерная или челночная или перемешиванием сортировка
        /// Проходим слева направо, при этом при выполнении swap элементов проверяем все оставшиеся позади елементы, не нужно ли повторить перемену.
        /// Т.е. Границы рабочей части массива (то есть части массива, где происходит движение) устанавливаются в месте последнего обмена на каждой итерации. 
        /// Массив просматривается поочередно справа налево и слева направо.
        /// </summary>
        /// <param name="array">Sorted array</param>
        private static void CocktailSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[i - 1])
                {
                    Swap(ref array[i], ref array[i - 1]);//right to left see
                    for (int z = i - 1; (z - 1) >= 0; z--)//местo последнего обмена на каждой итерации
                    {
                        if (array[z] < array[z - 1])
                        {
                            CountCompare++;//only counter compare not need base code;
                            Swap(ref array[z], ref array[z - 1]);//see left to right
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Сортировка выбором 
        /// Каждый проход выбирать самый минимальный элемент и смещать его в начало.
        /// При этом каждый новый проход начинать сдвигаясь вправо, то есть первый проход — с первого элемента, второй проход — со второго. 
        /// </summary>
        /// <param name="array">Sorted array</param>
        private static void SelectedSort(int[] array)
        {
            for (int left = 0; left < array.Length; left++)
            {
                int min = left;
                for (int i = left; i < array.Length; i++)
                {
                    if (array[i] < array[min])
                    {
                        CountCompare++;//only counter compare not need base code;
                        min = i;
                    }
                }
                Swap(ref array[left], ref array[min]);
            }
        }


        /// <summary>
        /// Сортировка вставками.
        /// Берем буфер в который кладем первое не оказавшееся на свом месте значение(в нашем случае крайнее слева в неотсортированной части).
        /// В массиве, начиная с правого края, просматриваются элементы и сравниваются с ключевым.
        /// Если больше ключевого, то очередной элемент сдвигается на одну позицию вправо, освобождая место для последующих элементов.
        /// Если попадается элемент, меньший или равный ключевому, то значит в текущую свободную ячейку массива можно вставить ключевой элемент.
        /// </summary>
        /// <param name="array">Sorted array</param>
        private static void InsertionSort(int[] array)
        {
            for (int left = 0; left < array.Length; left++)
            {
                int value = array[left];
                int i = left - 1;
                for (; i >= 0; i--)
                {
                    if (value < array[i])
                    {
                        CountCompare++;//only counter compare not need base code;
                        array[i + 1] = array[i];
                    }
                    else
                    {
                        break;
                    }
                }
                array[i + 1] = value;
            }
        }

        /// <summary>
        /// Быстрая сортировка.  Хоара
        /// Выбирается опорный элемент (например, посередине массива).
        /// Массив просматривается слева-направо и производится поиск ближайшего элемента, больший чем опорный.
        /// Массив просматривается справа-налево и производится поиск ближайшего элемента, меньший чем опорный.
        /// Найденные элементы меняются местами.
        /// Продолжается одновременный двухсторонний просмотр по массиву с последующими обменами в соответствии с пунктами 2-4.
        /// В конце концов, просмотры слева-напрво и справа-налево сходятся в одной точке, которая делит массив на два подмассива.
        /// К каждому из двух подмассивов рекурсивно применяется "Быстрая сортировка"
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void QuickSort(int[] array, int left, int right)
        {
            ///
            int partition(int[] arr, int l, int r)
            {
                if (l > r) return -1;

                int end = l;

                int pivot = arr[r];    // choose last one to pivot, easy to code
                for (int i = l; i < r; i++)
                {
                    if (arr[i] < pivot)
                    {
                        CountCompare++;//only counter compare not need base code;
                        Swap(ref arr[i], ref arr[end]);
                        end++;
                    }
                }

                Swap(ref arr[end], ref arr[r]);

                return end;
            }


        /// Выбрать элемент из массива. Назовём его опорным.
        /// Разбиение: перераспределение элементов в массиве таким образом, что элементы меньше опорного помещаются перед ним, а больше или равные после.
        /// Рекурсивно применить первые два шага к двум подмассивам слева и справа от опорного элемента. Рекурсия не применяется к массиву, в котором только один элемент или отсутствуют элементы.

            int index = partition(array, left, right);

            if (index != -1)
            {
                QuickSort(array, left, index - 1);
                QuickSort(array, index + 1, right);
            }
        }

        private static void Main(string[] args)
        {
            //int[] array = new int[] { 10, 3, 2, 1, 8, 3, 9, 0, 5, 5, 1 };

            int[] array = RandomArray();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Unmodified Unsorted array -> [{0}]", string.Join(", ", array));

            Stopwatch stopwatch = Stopwatch.StartNew();

            // BoobleSort(array);
              SelectedSort(array);
            //InsertionSort(array);
            //GnomeSort(array);
            // CocktailSort(array);

            //Array.Sort(array);//default standart

            //QuickSort(array, 0, array.Length - 1);

            stopwatch.Stop();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Modified Sorted array     -> [{0}]", string.Join(", ", array));


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nTicks of CPU during array sorting is " + stopwatch.ElapsedTicks);
            Console.WriteLine("\nMiliseconds during array sorting is " + stopwatch.Elapsed);
            Console.WriteLine($"\ncount compare = {CountCompare} and count move = {CountMove}");

            Console.WriteLine("\nPress any key to continue..");
            Console.ReadKey();
        }
    }
}
