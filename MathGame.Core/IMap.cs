namespace MathGame.Core
{
    public interface IMap
    {
        bool CheckCorrectness();
        void Generate();
        int[,] GetMap();
        int GetMin(int x, int y);
        int GetPartOfMap(int X, int Y);
        int GetSumColumn(int x);
        int GetSumRow(int y);
        void PrintMap();
        void SetMap(int[,] map);
    }
}