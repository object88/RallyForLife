using LifeLogic;
using LifeLogic.Exceptions;
using LifeLogic.Factories;
using NUnit.Framework;
using StructureMap;

namespace LifeLogicTest
{
    [TestFixture]
    public class StateTest
    {
        private Container _container;
        private IState _s;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _container = new Container(_ => _.For<IFactory>().Use<Factory>().Singleton());
        }

        [SetUp]
        public void SetUp()
        {
            // Initial state:
            // 100
            // 110
            _s = _container.GetInstance<IFactory>().CreateState(2, 3);

            _s.Reset(new[,] { { true, false, false }, { true, true, false } });
        }

        [Test]
        public void Validate_set_state()
        {
            // Test first row.
            Assert.True(_s[0, 0]);
            Assert.False(_s[0, 1]);
            Assert.False(_s[0, 2]);

            // Test second row
            Assert.True(_s[1, 0]);
            Assert.True(_s[1, 1]);
            Assert.False(_s[1, 2]);
        }

        [Test]
        public void Validate_large_set_state()
        {
            // Reset state with larger data set.
            IState s = _container.GetInstance<IFactory>().CreateState(3, 6);
            s.Reset(new [,] { { true, true, false, false, true, false }, { false, true, false, true, false, true }, { false, false, false, false, false, true } } );

            Assert.True(s[0,0]);
            Assert.True(s[0, 1]);
            Assert.False(s[0, 2]);
            Assert.False(s[0, 3]);
            Assert.True(s[0, 4]);
            Assert.False(s[0, 5]);

            Assert.False(s[1, 0]);
            Assert.True(s[1, 1]);
            Assert.False(s[1, 2]);
            Assert.True(s[1, 3]);
            Assert.False(s[1, 4]);
            Assert.True(s[1, 5]);

            Assert.False(s[2, 0]);
            Assert.False(s[2, 1]);
            Assert.False(s[2, 2]);
            Assert.False(s[2, 3]);
            Assert.False(s[2, 4]);
            Assert.True(s[2, 5]);
        }

        [Test]
        [ExpectedException(typeof(CellAccessException))]
        public void Excessive_column_should_throw_exception()
        {
#pragma warning disable 168
            bool value = _s[0, 5];
#pragma warning restore 168
        }

        [Test]
        [ExpectedException(typeof(CellAccessException))]
        public void Excessive_row_access_should_throw_exception()
        {
#pragma warning disable 168
            bool value = _s[5, 0];
#pragma warning restore 168
        }

        [Test]
        [ExpectedException(typeof(CellAccessException))]
        public void Negative_column_should_throw_exception()
        {
#pragma warning disable 168
            bool value = _s[0, -1];
#pragma warning restore 168
        }

        [Test]
        [ExpectedException(typeof(CellAccessException))]
        public void Negative_row_access_should_throw_exception()
        {
#pragma warning disable 168
            bool value = _s[-1, 0];
#pragma warning restore 168
        }
    }
}
