namespace LifeLogic
{
    public interface IState
    {
        bool this[int row, int column] { get; }

        int Width { get; }
        int Height { get; }

        void Reset(bool[,] values);
        bool ValidateLocation(int row, int column);
    }
}
