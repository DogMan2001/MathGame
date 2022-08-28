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
            int nextY; // V jakem radku budeme v dalsim kroku -> y - [x,y]
            int currentY = 1; // Aktualni Y
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

            if (firstNumber > 15) Debugger.Break();

            map[startpositionX, startpositionY] = firstNumber;
            numbersLeft--;

            // while(numbersLeft > 0)
            //for (int i = 1; i < width * height; i++)
            while(numbersLeft > 0)
            {
                Console.Write($"NumbersLeft: {numbersLeft} |");
                // vyber nahodny sloupec
                //do
                //{
                //    int minPossibleValue = Math.Max(1, currentY - 1); // Min 1
                //    int maxPossibleValue = Math.Min(3, currentY + 2); // Max 3
                //    nextY = random.Next(minPossibleValue, maxPossibleValue);
                //} while (numbersLeft != 1 ? (NextX(nextY) == 2 && nextY == 2) : false);
                nextY = FindNextY();

                Console.WriteLine($"Got nextY: {nextY}");
               
               
                //if (currentY > 0)
                //{
                //    while (nextY > 0 && NextX(nextY - 1) <= NextX(nextY))
                //    {
                //        nextY--;
                //    }

                //    Console.WriteLine($"CurrentY ({currentY}>0: nextY: {nextY}");
                //}

                // Generate next number
                int nextX = NextX(nextY);
                if (nextX >= 4 || nextX < 0) Debugger.Break();

                int newMin = GetMin(nextX, nextY);
                int newNum = GenerateNewNum(newMin, numbersLeft);

                Console.WriteLine($"Next X,Y: [{nextX}, {nextY}]. NewMin: {newMin}. NewNum: {newNum}");

                if (map[nextX, nextY] != 0)
                {
                    Debugger.Break();
                }

                currentY = nextY;
                map[nextX, nextY] = newNum;
                numbersLeft--;

                PrintMap();
                Console.WriteLine("------------------------------");
                Console.WriteLine();
            }
        }

        private int FindNextY()
        {
            int col0 = CheckIfColumnHasSpace(0);
            int col1 = CheckIfColumnHasSpace(1);
            int col2 = CheckIfColumnHasSpace(2);

            int[] columns = new int[] { col0, col1, col2 };
            List<int> availableColumns = new List<int>();

            foreach (int colIndex in columns)
            {
                if (colIndex != -1) availableColumns.Add(colIndex);
            }

            int index = random.Next(0, availableColumns.Count);

            return availableColumns[index];
        }

        private int CheckIfColumnHasSpace(int x)
        {
            for (int i = 0; i < 3; i++)
            {
                if (map[x, i] == 0) return i;
            }
            //no space = -1
            return -1;
        }

        /// <summary>
        /// generate a random but valid number
        /// </summary>
        /// <param name="min"></param>
        /// <param name="numbersLeft"></param>
        /// <returns></returns>
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

        /// <summary>
        /// find next avalibale slot
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private int NextX(int y)
        {
         
            for (int i = 0; i < 3; i++)
            {
                if (map[i, y] == 0) return i;
            }
            return 0;
        }

        /// <summary>
        /// get minimum number for generation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returnes map
        /// </summary>
        /// <returns></returns>
        public int[,] GetMap()
        {
            return map;
        }

        /// <summary>
        /// changes map from outside
        /// </summary>
        /// <param name="map"></param>
        public void SetMap(int[,] map)
        {
            this.map = map;
        }

        /// <summary>
        /// returnes a specific coordenate from the map
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public int GetPartOfMap(int X, int Y)
        {
            return map[X, Y];
        }

        /// <summary>
        /// finds the sum of a row
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetSumRow(int y)
        {
            int sum = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                sum += map[x, y];
            }

            return sum;
        }

        /// <summary>
        /// finds the sum of a column
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int GetSumColumn(int x)
        {
            int sum = 0;
            for (int y = 0; y < map.GetLength(1); y++)
            {
                sum += map[x, y];
            }

            return sum;
        }

        /// <summary>
        /// checks if map is valid
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// prints the map to the console
        /// </summary>
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

