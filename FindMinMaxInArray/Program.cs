using System;
using System.Collections.Generic;
using System.Linq;

namespace FindMinMaxElementInArray
{
    internal class Program
    {
        /// <summary>
        /// Находим максимальную величину в массиве
        /// </summary>
        /// <param name="array">Array parameter</param>
        /// <returns> Вернем значение максимального елемента в массиве.</returns>
        private static int FindMax(int[] array)
        {
            int max = array[0];//подразумеваем, что первый елемент может соответствовать условию
            foreach (int item in array)//и проверяем утверждение в цикле
            {
                if (item >= max)
                {
                    max = item;
                }
            }
            return max;
        }

        /// <summary>
        /// Находим минимальную величину в массиве
        /// </summary>
        /// <param name="array">Array parameter</param>
        /// <returns> Вернем значение минимального елемента в массиве.</returns>
        private static int FindMin(int[] array)
        {
            int min = array[0];
            foreach (int item in array)
            {
                if (item <= min)
                {
                    min = item;
                }
            }
            return min;
        }

        /// <summary>
        /// Заменяем по значению
        /// все максимальные величины в массиве на минимальные
        /// и минимальные на максимальные соответственно.
        /// </summary>
        /// <param name="array">Array parameter</param>
        private static void ChangeMaxMin(int[] array)
        {
#if DEBUG
            int max = FindMax(array);
            int min = FindMin(array);
#else
            int max = array.Max();//standart method for max
            int min = array.Min();//standart method for min
#endif

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == min)
                {
                    array[i] = max;
                }
                else if (array[i] == max)
                {
                    array[i] = min;
                }
            }
        }

        /// <summary>
        /// Сдвинуть циклически вправо на n позиций 
        /// </summary>
        /// <param name="array">Array parameter</param>
        /// <param name="positions">n позиций для сдвига вправо</param>
        public static void RotateRight(int[] array, int positions)
        {
            int size = array.Length;// загоням в переменную для снижения ко-ва пересчетов величины массива

            for (int i = 0; i < positions; i++)
            {
                int temp = array[size - 1];//первым (на итерацию по позициям) берем крайний справа

                for (int j = size - 1; j > 0; j--)
                {
                    array[j] = array[j - 1];//бежим от конца переставляя предыдущий и текущий елементы (ибо справа)
                }

                array[0] = temp; //крайний слева (на итерацию по позициям) устанавливаем с временной переменной
            }
        }

        /// <summary>
        /// Сдвинуть циклически влево на n позиций 
        /// </summary>
        /// <param name="array">Array parameter</param>
        /// <param name="positions">n позиций для сдвига влево</param>
        public static void RotateLeft(int[] array, int positions)
        {
            int size = array.Length;

            for (int i = 0; i < positions; i++)
            {
                int temp = array[0];//первым (на итерацию по позициям) берем крайний слева

                for (int j = 0; j < size - 1; j++)//бежим по массиву с его начала
                {
                    array[j] = array[j + 1];// переставляя следующий и текущий елементы (ибо слева)
                }

                array[size - 1] = temp;// крайний справа(на итерацию по позициям) устанавливаем с временной переменной
            }
        }

        /// <summary>
        /// Циклический сдвиг слева направо на некоторую величину используя Linq
        /// </summary>
        /// <param name="array">Array parameter</param>
        /// <param name="size"> Size of removal</param>
        /// <returns> Array as Enumerable with offset on some position</returns>
        private static IEnumerable<int> RoteteArrayUseLinq(int[] array, int size)
        {
            //будем пропускать все елементы до начала смещения включительно (фактически отрежем кусок массива)
            //и затем добавим к нему кусок елементов из маcсива равное смещению  
            //отрезание и добавление кусков фактичеcки сделают смещение массива
            return array.Skip(size).Concat(array.Take(size));
        }

        /// <summary>
        /// Является ли массив зеркально обратным самому себе - т.е. равно ли значение елемента массива его величине
        /// </summary>
        /// <param name="array">Array parameter</param>
        /// <returns>Истино если массив зеркален</returns>
        private static bool IsMirrorInverse(int[] array)
        {
            for (int i = 0; i < array.Length-1; i++)
            {
                //если значение хоть одного елемента не равено индексу
                if (array[array[i]] != i)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Является ли массив зеркально обратным самому себе
        /// </summary>
        /// <param name="array">Array parameter</param>
        /// <returns>Истино если массив зеркален</returns>
        private static bool IsMirror(int[] array)
        {
            //Простой подход заключается в создании нового массива путем добавления индекса в его значение 
            int[] arr = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == i)
                    arr[i] = array[i];
            }
            //и проверки равен ли новый массив исходному или нет.
            return array.SequenceEqual(arr);
        }
        
        private static void Main(string[] args)
        {
            // int[] array = { 1, 2, 3, 0 };
             int[] array = new int[] { 10, 3, 2, 1, 8, 3, 9, 0, 5, 5, 10 };
            // int[] array = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine("Array -> [{0}]", string.Join(", ", array));

            if (IsMirrorInverse(array))
                Console.WriteLine("Is array mirror - inverse  == Yes");
            else
                Console.WriteLine("Is array mirror - inverse == No");

            Console.WriteLine($"Easy way check array is mirror or not - {IsMirror(array)}");

            Console.WriteLine("\nMax value in array is {0}", FindMax(array));
            Console.WriteLine("\nMin value in array is {0}", FindMin(array));

            Console.WriteLine("\nMin value in array is {0} (we use linq)", array.Min());

            //ChangeMaxMin(array);
            // var array1 = RoteteArrayUseLinq(array, 3);

            //RotateLeft(array, 5);//true and work
            //RotateRight(array, 5);//true and work           

            Console.WriteLine("Array -> [{0}]", string.Join(", ", array/*array1*/));


            Console.WriteLine("\nPress any key to continue..");
            Console.ReadKey();
        }
    }
}
