using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MathGame.Core
{

    //public interface ICanMakeSound
    //{
    //    void MakeSound();
    //}

    //public class Animal : ICanMakeSound
    //{
    //    public void MakeSound()
    //    {
    //        Console.WriteLine("haf haf");
    //    }

    //    public void Eat()
    //    {

    //    }
    //}

    //public class Car : ICanMakeSound
    //{
    //    public void MakeSound()
    //    {
    //        Console.WriteLine("bvrrrm");
    //    }

    //    public void OpenDoors()
    //    {

    //    }
    //}

    //public class ObjectInteractor
    //{
    //    public void AcceptAndMakeSound(ICanMakeSound objectCanMakeSound)
    //    {
    //        objectCanMakeSound.MakeSound();
    //    }
    //}
    public class Map : IMap
    {
        int width;
        int height;
        int maxValue;
        int[,] map;
        int minValue = 1;

        public Map(int width, int heigth, int maxValue)
        {
            this.width = width;
            this.height = heigth;
            this.maxValue = maxValue;
            map = new int[width, height];
        }

        public void Generate()
        {
            map = new int[width, height];
            int numbersLeft = width * height - 1;

            int min, max = 0;
            HashSet<int> usedNumbers = new HashSet<int>();

            Random random = new Random();


            for (int x = 0; x < map.GetLength(0); x++)
            {

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    min = GetMin(x, y);
                    max = maxValue - numbersLeft;

                    int newNumber = 0;
                    do
                    {
                        newNumber = random.Next(min, max + 1);
                    } while (usedNumbers.Contains(newNumber));

                    map[x, y] = newNumber;
                    usedNumbers.Add(newNumber);

                    numbersLeft--;
                }
            }


        }

        public int GetMin(int x, int y)
        {
            if (x == 0 && y == 0) return minValue;

            int left = x - 1;
            int up = y - 1;

            int highest = map[x, y];

            if (left >= 0) highest = map[left, y];

            if (up >= 0 && map[x, up] > highest) highest = map[x, up];

            return highest + 1;
        }

        public int[,] GetMap()
        {
            return map;
        }

        public void SetMap(int[,] map)
        {
            this.map = map;
        }

        public int GetPartOfMap(int X, int Y)
        {
            return map[X, Y];
        }

        public int GetSumRow(int y)
        {
            int sum = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                sum += map[x, y];
            }

            return sum;
        }

        public int GetSumColumn(int x)
        {
            int sum = 0;
            for (int y = 0; y < map.GetLength(1); y++)
            {
                sum += map[x, y];
            }

            return sum;
        }

        public bool CheckCorrectness()
        {
            bool isCorrect = true;
            HashSet<int> numbers = new HashSet<int>();

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int number = map[x, y];

                    if (numbers.Contains(number))
                    {
                        Console.WriteLine($"Number {number} already exists!");
                        isCorrect = false;
                    }

                    numbers.Add(number);

                    int leftIndex = x - 1;
                    if (leftIndex >= 0 && number <= map[leftIndex, y])
                    {
                        Console.WriteLine($"Number {number} is lower than {map[leftIndex, y]}. Coords: [{x},{y}]");
                        isCorrect = false;
                    }

                    int upIndex = y - 1;
                    if (upIndex >= 0 && number <= map[x, upIndex])
                    {
                        Console.WriteLine($"Number {number} is lower than {map[x, upIndex]}. Coords: [{x},{y}]");
                        isCorrect = false;
                    }
                }
            }

            return isCorrect;
        }

        public void PrintMap()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write($"{map[x, y],2} ");
                }
                Console.WriteLine();
            }
        }
    }

    public class MapBetter : IMap
    {
        int width;
        int height;
        int maxValue;
        int[,] map;
        int minValue = 1;

        Random random = new Random();
        HashSet<int> usedNumbers = new HashSet<int>();


        public MapBetter(int width, int heigth, int maxValue)
        {
            this.width = width;
            this.height = heigth;
            this.maxValue = maxValue;
            map = new int[width, height];
        }

        public void Generate()
        {
            int lineGenNum;
            int currentLineNum = 1;
            map = new int[width, height];
            int numbersLeft = width * height;
            usedNumbers.Clear();

            // Clear map
            for (int X = 0; X < width; X++)
            {
                for (int Y = 0; Y < height; Y++)
                {
                    map[X, Y] = 0;
                }
            }

            int startpositionX = 0;
            int startpositionY = 0;

            int firstNumber = GenerateNewNum(GetMin(startpositionX, startpositionY), numbersLeft);

            map[startpositionX, startpositionY] = firstNumber;
            numbersLeft--;

            for (int i = 1; i < width * height; i++)
            {
                // FIX: bug


                //do
                //{
                // vyber nahodny sloupec
                do
                {
                    int minPossibleValue = Math.Max(1, currentLineNum - 1); // Min 1
                    int maxPossibleValue = Math.Min(3, currentLineNum + 2); // Max 3
                    lineGenNum = random.Next(minPossibleValue, maxPossibleValue);
                } while (numbersLeft != 1 ? (NextX(lineGenNum) == 3 && lineGenNum == 3) : false);

                //} while (lineGenNum != 0 && lineGenNum != 4);

                if (currentLineNum > 1)
                {
                    while (NextX(lineGenNum - 1) <= NextX(lineGenNum))
                    {
                        lineGenNum--;
                        if (lineGenNum == 1) break;
                    }
                }


                int nextX = NextX(lineGenNum);
                //if (nextX >= 4 || nextX <= 0) Debugger.Break();

                int newNum = GenerateNewNum(GetMin(nextX, lineGenNum), numbersLeft);
                currentLineNum = lineGenNum;
                map[nextX, lineGenNum] = newNum;
                numbersLeft--;
            }
        }

        private int GenerateNewNum(int min, int numbersLeft)
        {
            int newNum;
            do
            {
                newNum = random.Next(min, 1 + maxValue - numbersLeft);
            } while (usedNumbers.Contains(newNum));

            usedNumbers.Add(newNum);

            return newNum;
        }

        private int NextX(int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (map[i, y] == 0) return i;
            }
            return 0;
        }

        public int GetMin(int x, int y)
        {
            if (x == 0 && y == 0) return minValue;

            int left = x - 1;
            int up = y - 1;

            int highest = map[x, y];

            if (left >= 0) highest = map[left, y];

            if (up >= 0 && map[x, up] > highest) highest = map[x, up];

            return highest + 1;
        }

        public int[,] GetMap()
        {
            return map;
        }

        public void SetMap(int[,] map)
        {
            this.map = map;
        }

        public int GetPartOfMap(int X, int Y)
        {
            return map[X, Y];
        }

        public int GetSumRow(int y)
        {
            int sum = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                sum += map[x, y];
            }

            return sum;
        }

        public int GetSumColumn(int x)
        {
            int sum = 0;
            for (int y = 0; y < map.GetLength(1); y++)
            {
                sum += map[x, y];
            }

            return sum;
        }

        public bool CheckCorrectness()
        {
            bool isCorrect = true;
            HashSet<int> numbers = new HashSet<int>();

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int number = map[x, y];

                    if (numbers.Contains(number))
                    {
                        Console.WriteLine($"Number {number} already exists!");
                        isCorrect = false;
                    }

                    numbers.Add(number);

                    int leftIndex = x - 1;
                    if (leftIndex >= 0 && number <= map[leftIndex, y])
                    {
                        Console.WriteLine($"Number {number} is lower than {map[leftIndex, y]}. Coords: [{x},{y}]");
                        isCorrect = false;
                    }

                    int upIndex = y - 1;
                    if (upIndex >= 0 && number <= map[x, upIndex])
                    {
                        Console.WriteLine($"Number {number} is lower than {map[x, upIndex]}. Coords: [{x},{y}]");
                        isCorrect = false;
                    }
                }
            }

            return isCorrect;
        }

        public void PrintMap()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write($"{map[x, y],2} ");
                }
                Console.WriteLine();
            }
        }
    }

}

