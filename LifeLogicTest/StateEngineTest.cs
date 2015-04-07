using System;
using LifeLogic;
using LifeLogic.Factories;
using NUnit.Framework;
using StructureMap;

namespace LifeLogicTest
{
    [TestFixture]
    public class StateEngineTest
    {
        private Container _container;

        private IStateEngine _engine;
        private IFactory _factory;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            _container = new Container(_ => _.For<IFactory>().Use<Factory>().Singleton());
        }

        [SetUp]
        public void SetUp()
        {
            _factory = _container.GetInstance<IFactory>();
            _engine = _factory.CreateEngine();
        }

        [Test]
        public void Ensure_iterate_returns_object()
        {
            // Initial state:
            // 100
            // 110
            IState s0 = _container.GetInstance<IFactory>().CreateState(2, 3);
            s0.Reset(new[,] { { true, false, false }, { true, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.NotNull(s);
            Assert.True(ReferenceEquals(s0, s));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ensure_iterate_rejects_null()
        {
            _engine.Iterate(null);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_0_neighbors()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, true, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_1_neighbor_up()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, true, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_1_neighbor_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, true, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_1_neighbor_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, true, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_1_neighbor_right()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, true, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_2_neighbors_up_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, true, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_2_neighbors_up_and_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, true, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_2_neighbors_up_and_right()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, true, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_2_neighbors_left_and_right()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, true, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_2_neighbors_left_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, true, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_2_neighbors_right_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, true, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_3_neighbors_up_and_left_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, true, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_3_neighbors_up_and_right_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, true, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_3_neighbors_up_and_right_and_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, true, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_3_neighbors_down_and_right_and_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, true, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_live_cell_with_4_neighbors()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, true, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_0_neighbors()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, false, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_1_neighbor_up()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, false, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_1_neighbor_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, false, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_1_neighbor_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, false, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_1_neighbor_right()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, false, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_2_neighbors_up_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, false, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }


        [Test]
        public void Ensure_iterate_handles_dead_cell_with_2_neighbors_up_and_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, false, false }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_2_neighbors_up_and_right()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, false, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_2_neighbors_left_and_right()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, false, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_2_neighbors_left_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, false, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_2_neighbors_right_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { false, false, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_3_neighbors_up_and_left_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, false, false }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_3_neighbors_up_and_right_and_down()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { false, true, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_3_neighbors_up_and_right_and_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, false, true }, { false, false, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_3_neighbors_down_and_right_and_left()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, false, false }, { true, false, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.True(s[1, 1]);
        }

        [Test]
        public void Ensure_iterate_handles_dead_cell_with_4_neighbors()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(3, 3);
            s0.Reset(new[,] { { false, true, false }, { true, false, true }, { false, true, false } });

            IState s = _engine.Iterate(s0);
            Assert.False(s[1, 1]);
        }

        [Test]
        public void Ensure_sample_date_passes()
        {
            IState s0 = _container.GetInstance<IFactory>().CreateState(5, 5);
            s0.Reset(new[,]
                {
                    { false, true, false, false, false },
                    { true, false, false, true, true },
                    { true, true, false, false, true },
                    { false, true, false, false, false },
                    { true, false, false, false, true }
                });

            IState s = _engine.Iterate(s0);

            Assert.False(s[0, 0]);
            Assert.False(s[0, 1]);
            Assert.False(s[0, 2]);
            Assert.False(s[0, 3]);
            Assert.False(s[0, 4]);

            Assert.True(s[1, 0]);
            Assert.False(s[1, 1]);
            Assert.True(s[1, 2]);
            Assert.True(s[1, 3]);
            Assert.True(s[1, 4]);

            Assert.True(s[2, 0]);
            Assert.True(s[2, 1]);
            Assert.True(s[2, 2]);
            Assert.True(s[2, 3]);
            Assert.True(s[2, 4]);

            Assert.False(s[3, 0]);
            Assert.True(s[3, 1]);
            Assert.False(s[3, 2]);
            Assert.False(s[3, 3]);
            Assert.False(s[3, 4]);

            Assert.False(s[4, 0]);
            Assert.False(s[4, 1]);
            Assert.False(s[4, 2]);
            Assert.False(s[4, 3]);
            Assert.False(s[4, 4]);
        }
    }
}
