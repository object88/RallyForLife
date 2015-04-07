using System.IO;
using LifeLogic.IO;

namespace LifeLogic.Factories
{
    /// <summary>
    /// Factory for state, state engine, and reader / writer.
    /// </summary>
    public interface IFactory
    {
        IStateEngine CreateEngine();
        IStateReader CreateReader(TextReader textReader);
        IState CreateState(int height, int width);
        IStateWriter CreateWriter(TextWriter textWriter);
    }
}
