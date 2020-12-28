using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItAcademyTest.SomeLogic
{
    public class Logic
    {
        public static int[] ArrIntCreate(int size, int minValue, int maxValue)     //инициализация массива Int длинны size случайными числами от minValu до maxValue 
        {
            int[] tempArr = new int[size];
            var rand = new Random();
            for (int i = 0; i < size; i++)
            {
                tempArr[i] = rand.Next(minValue, maxValue + 1);
            }
            return tempArr;
        }



        public static void MySort(int[] arr)    //сортировка массива вставками 
        {
            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }



        public static bool IsThereAnyAnswer(int[] arr, int x)    //проверяем есть ли в масиве хотя бы один элемент >  заданного значения X
        {
            bool answer = false;

            for (int i = 0; i < arr.Length && answer == false; i++)
            {
                if (arr[i] > x)
                {
                    answer = true;
                }
            }

            return answer;
        }



        public static int bsearch(int[] arr, int x)    //поиск номера первого элемента значение которого > заданного X
        {
            int numberOfFirstElementAboveX = 0;
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > x)
                {
                   numberOfFirstElementAboveX = i + 1;
                   break;
                }
            }

            return numberOfFirstElementAboveX;
        }
    }

}