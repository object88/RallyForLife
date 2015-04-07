using System;
using System.IO;
using LifeLogic;
using LifeLogic.Exceptions;
using LifeLogic.Factories;
using LifeLogic.IO;
using NUnit.Framework;
using StructureMap;

namespace LifeLogicTest
{
    [TestFixture]
    public class StateFactoryTest
    {
        private Container _container;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _container = new Container(_ => _.For<IFactory>().Use<Factory>().Singleton());
        }

        [Test]
        public void Ensure_parse_good_input()
        {
            string input =
                "01000" + Environment.NewLine +
                "10011" + Environment.NewLine +
                "11001" + Environment.NewLine +
                "01000" + Environment.NewLine +
                "10001" + Environment.NewLine;

            IStateReader sr = _container.GetInstance<IFactory>().CreateReader(new StringReader(input));
            IState s = sr.Read();

            Assert.False(s[0, 0]);
            Assert.True(s[0, 1]);
            Assert.False(s[0, 2]);
            Assert.False(s[0, 3]);
            Assert.False(s[0, 4]);

            Assert.True(s[1, 0]);
            Assert.False(s[1, 1]);
            Assert.False(s[1, 2]);
            Assert.True(s[1, 3]);
            Assert.True(s[1, 4]);

            Assert.True(s[2, 0]);
            Assert.True(s[2, 1]);
            Assert.False(s[2, 2]);
            Assert.False(s[2, 3]);
            Assert.True(s[2, 4]);

            Assert.False(s[3, 0]);
            Assert.True(s[3, 1]);
            Assert.False(s[3, 2]);
            Assert.False(s[3, 3]);
            Assert.False(s[3, 4]);

            Assert.True(s[4, 0]);
            Assert.False(s[4, 1]);
            Assert.False(s[4, 2]);
            Assert.False(s[4, 3]);
            Assert.True(s[4, 4]);
        }

        [Test]
        [ExpectedException(typeof(EmptyInputException))]
        public void State_factory_rejects_null_input()
        {
            IStateReader sr = _container.GetInstance<IFactory>().CreateReader(null);
            sr.Read();
        }

        [Test]
        [ExpectedException(typeof(EmptyInputException))]
        public void State_factory_rejects_empty_string_input()
        {
            IStateReader sr = _container.GetInstance<IFactory>().CreateReader(new StringReader(string.Empty));
            sr.Read();
        }

        [Test]
        [ExpectedException(typeof(InvalidCharacterInputException))]
        public void State_factory_rejects_bad_characters()
        {
            IStateReader sr = _container.GetInstance<IFactory>().CreateReader(
                new StringReader(
                    "101" + Environment.NewLine +
                    "10a" + Environment.NewLine +
                    "111" + Environment.NewLine));
            sr.Read();
        }

        [Test]
        [ExpectedException(typeof(JaggedInputException))]
        public void State_factory_rejects_uneven_rows()
        {
            IStateReader sr = _container.GetInstance<IFactory>().CreateReader(
                new StringReader(
                    "101" + Environment.NewLine +
                    "10" + Environment.NewLine +
                    "111" + Environment.NewLine));
            sr.Read();
        }
    }
}
