using System.IO;
using LifeLogic.IO;

namespace LifeLogic.Factories
{
    /// <summary>
    /// Factory for state, state engine, and reader / writer.
    /// </summary>
    public class Factory
        : IFactory
    {
        private static readonly IStateEngine StateEngine;

        static Factory()
        {
            StateEngine = new StateEngine();
        }

        IState IFactory.CreateState(int height, int width)
        {
            return new State(height, width);
        }

        IStateReader IFactory.CreateReader(TextReader textReader)
        {
            return new StateReader(this, textReader);
        }

        IStateEngine IFactory.CreateEngine()
        {
            return StateEngine;
        }

        IStateWriter IFactory.CreateWriter(TextWriter textWriter)
        {
            return new StateWriter(textWriter);
        }
    }
}
