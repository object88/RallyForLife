using System.Collections;

namespace LifeLogic.DTO
{
    /// <summary>
    /// Pure data object.  Not technically a DTO, but no business logic, just state.
    /// </summary>
    public class StateDTO
    {
        private readonly BitArray _state;

        public StateDTO(int size)
        {
            _state = new BitArray(size);
        }

        public bool this[int index]
        {
            get
            {
                return _state.Get(index);
            }
            set
            {
                _state.Set(index, value);
            }
        }

        public void Reset(bool[] values)
        {
            int size = values.GetLength(0);
            for (int index = 0; index < size; index++)
            {
                _state.Set(index, values[index]);
            }
        }
    }
}
