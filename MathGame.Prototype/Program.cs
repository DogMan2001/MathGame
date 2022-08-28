using MathGame.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MathGame.Prototype
{
    class Program
    {

        const int width = 3;
        const int height = 3;

        const int maxValue = 24;
        const int minValue = 1;

        static void Main(string[] args)
        {
            //Car car = new Car();
            //Animal animal = new Animal();

            //ObjectInteractor interactor = new ObjectInteractor();

            //interactor.AcceptAndMakeSound(car);
            //interactor.AcceptAndMakeSound(animal);

            //ICanMakeSound something = car;

            //if(something is Car itsACar)
            //{
            //    itsACar.OpenDoors();
            //}

            IMap generator = new MapBetter(width, height, maxValue);
            while (true)
            {
                generator.Generate();
                generator.PrintMap();
                bool valid = generator.CheckCorrectness();

                if (!valid)
                {
                    Debugger.Break();
                }
                Console.WriteLine();

               

            }
        }

        //static int[,] GenerateMap()
        //{
        //    int numbersLeft = width * height - 1;

        //    int min, max = 0;
        //    HashSet<int> usedNumbers = new HashSet<int>();

        //    int[,] map = new int[width, height];

        //    Random random = new Random();


        //    for (int x = 0; x < map.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < map.GetLength(1); y++)
        //        {
        //            min = GetMin(map, x, y);
        //            max = maxValue - numbersLeft;

        //            int newNumber = 0;
        //            do
        //            {
        //                newNumber = random.Next(min, max + 1);
        //            } while (usedNumbers.Contains(newNumber));

        //            map[x, y] = newNumber;
        //            usedNumbers.Add(newNumber);

        //            numbersLeft--;
        //        }
        //    }

        //    return map;
        //}

        //static int GetMin(int[,] map, int x, int y)
        //{
        //    if (x == 0 && y == 0) return minValue;

        //    int left = x - 1;
        //    int up = y - 1;

        //    int highest = map[x, y];

        //    if (left >= 0) highest = map[left, y];

        //    if (up >= 0 && map[x, up] > highest) highest = map[x, up];

        //    return highest + 1;
        //}

        //static void PrintMap(int[,] map)
        //{
        //    for (int x = 0; x < map.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < map.GetLength(1); y++)
        //        {
        //            Console.Write($"{map[x, y],2} ");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        //static bool CheckCorrectness(int[,] map)
        //{

        //    bool isCorrect = true;
        //    HashSet<int> numbers = new HashSet<int>();

        //    for (int x = 0; x < map.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < map.GetLength(1); y++)
        //        {
        //            int number = map[x, y];

        //            if (numbers.Contains(number))
        //            {
        //                Console.WriteLine($"Number {number} already exists!");
        //                isCorrect = false;
        //            }

        //            numbers.Add(number);

        //            int leftIndex = x - 1;
        //            if (leftIndex >= 0 && number <= map[leftIndex, y])
        //            {
        //                Console.WriteLine($"Number {number} is lower than {map[leftIndex, y]}. Coords: [{x},{y}]");
        //                isCorrect = false;
        //            }

        //            int upIndex = y - 1;
        //            if (upIndex >= 0 && number <= map[x, upIndex])
        //            {
        //                Console.WriteLine($"Number {number} is lower than {map[x, upIndex]}. Coords: [{x},{y}]");
        //                isCorrect = false;
        //            }
        //        }
        //    }

        //    return isCorrect;
        //}
    }
}
