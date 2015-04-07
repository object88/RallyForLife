namespace LifeLogic
{
    public interface IStateEngine
    {
        IState Iterate(IState state);
    }
}
