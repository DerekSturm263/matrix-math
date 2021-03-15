using System;
using System.Collections;

namespace MathClass
{
    class MatrixEnumerator : IEnumerator
    {
        public float[,] _slots;
        private int rowIndex = -1;
        private int columnIndex = 0;

        public MatrixEnumerator(float[,] _slots)
        {
            this._slots = _slots;
        }

        public bool MoveNext()
        {
            if (++rowIndex == _slots.GetLength(1))
            {
                rowIndex = 0;
                ++columnIndex;
            }

            return (rowIndex + columnIndex * _slots.GetLength(1) < _slots.Length);
        }

        public void Reset()
        {
            rowIndex = -1;
            columnIndex = 0;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public float Current
        {
            get
            {
                try
                {
                    return _slots[columnIndex, rowIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
