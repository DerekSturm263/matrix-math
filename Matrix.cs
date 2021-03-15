using System;
using System.Collections;

namespace MathClass
{
    /// <summary>
    /// Represents a matrix of floats that can be operated upon using scalars and matrices.
    /// </summary>
    public class Matrix : IEnumerable
    {
        public const int DefaultSize = 4;
        private readonly float[,] _slots;

        public float this[int row, int column]
        {
            get => _slots[row, column];
            set => _slots[row, column] = value;
        }

        /// <summary> 
        /// Returns the number of rows in a Matrix.
        /// </summary>
        public int Rows
        {
            get => _slots.GetLength(0);
        }

        /// <summary> 
        /// Returns the number of columns in a Matrix.
        /// </summary>
        public int Columns
        {
            get => _slots.GetLength(1);
        }

        /// <summary> 
        /// Returns the total number of slots in a Matrix.
        /// </summary>
        public int Size
        {
            get => _slots.GetLength(0) * _slots.GetLength(1);
        }

        public Matrix(int rows, int columns)
        {
            this._slots = new float[rows, columns];
        }

        public Matrix(int size = DefaultSize)
        {
            this._slots = new float[size, size];
        }

        /// <summary> 
        /// Sets the values of a certain row in a Matrix. The number of values must be equal to the length of the row.
        /// </summary>
        public void SetRow(int row, float[] values)
        {
            if (values.Length != this.GetRow(row).Length)
            {
                throw new ArgumentException("The number of arguments passed as values must be equal to the number of values in that row");
            }

            for (int i = 0; i < values.Length; ++i)
            {
                this[row, i] = values[i];
            }
        }

        /// <summary> 
        /// Sets the values of a certain column in a Matrix. The number of values must be equal to the length of the column.
        /// </summary>
        public void SetColumn(int column, float[] values)
        {
            if (values.Length != this.GetColumn(column).Length)
            {
                throw new ArgumentException("The number of arguments passed as values must be equal to the number of values in that column");
            }

            for (int i = 0; i < values.Length; ++i)
            {
                this[i, column] = values[i];
            }
        }

        /// <summary> 
        /// Returns an array of all the values in a row.
        /// </summary>
        public float[] GetRow(int row)
        {
            float[] rowValues = new float[this.Columns];

            for (int i = 0; i < this.Columns; ++i)
            {
                rowValues[i] = this[row, i];
            }

            return rowValues;
        }

        /// <summary> 
        /// Returns an array of all the values in a column.
        /// </summary>
        public float[] GetColumn(int column)
        {
            float[] columnValues = new float[this.Rows];

            for (int i = 0; i < this.Rows; ++i)
            {
                columnValues[i] = this[i, column];
            }

            return columnValues;
        }

        /// <summary> 
        /// Sets the values of each slot in a Matrix. The number of arguments passed must be equal to the size of the Matrix.
        /// </summary>
        public void SetValues(params float[] slots)
        {
            if (slots.Length != this.Size)
            {
                throw new ArgumentException("The number of arguments passed must be equal to the number of elements in the Matrix");
            }

            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    this._slots[i, j] = slots[i * this.Columns + j];
                }
            }
        }

        /// <summary> 
        /// Sets the values of each slot in a Matrix. The dimensions of values must be equal to the dimensions of the Matrix.
        /// </summary>
        public void SetValues(float[,] values)
        {
            if (values.GetLength(0) != this.Rows && values.GetLength(1) != this.Columns)
            {
                throw new ArgumentException("The dimensions of values must be equal to the dimensions of the Matrix");
            }

            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    this._slots[i, j] = values[i, j];
                }
            }
        }

        /// <summary> 
        /// Returns a single-dimensional array of all the values in the matrix.
        /// </summary>
        public float[] GetValues()
        {
            float[] values = new float[this.Size];

            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    values[i * this.Columns + j] = _slots[i, j];
                }
            }

            return values;
        }

        /// <summary> 
        /// Sets all the values of the matrix back to their default value.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    this[i, j] = 0f;
                }
            }
        }

        /// <summary>
        /// Determines whether the specified Matrix contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        public static bool Exists(Matrix m, Predicate<float> predicate)
        {
            for (int i = 0; i < m.Rows; ++i)
            {
                for (int j = 0; j < m.Columns; ++j)
                {
                    if (predicate.Invoke(m[i, j]))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether every element in the Matrix matches the conditions defined by the specified predicate.
        /// </summary>
        public static bool TrueForAll(Matrix m, Predicate<float> predicate)
        {
            for (int i = 0; i < m.Rows; ++i)
            {
                for (int j = 0; j < m.Columns; ++j)
                {
                    if (!predicate.Invoke(m[i, j]))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire MathClass.Matrix.
        /// </summary>
        public static float? Find(Matrix m, Predicate<float> predicate)
        {
            for (int i = 0; i < m.Rows; ++i)
            {
                for (int j = 0; j < m.Columns; ++j)
                {
                    if (predicate.Invoke(m[i, j]))
                        return m[i, j];
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieves all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        public static float[] FindAll(Matrix m, Predicate<float> predicate)
        {
            float[] returnValue = new float[m.Size];
            int valIndex = 0;

            for (int i = 0; i < m.Rows; ++i)
            {
                for (int j = 0; j < m.Columns; ++j)
                {
                    if (predicate.Invoke(m[i, j]))
                    {
                        returnValue[valIndex++] = m[i, j];
                    }
                }
            }

            Array.Resize(ref returnValue, valIndex);

            return returnValue;
        }

        /// <summary>
        /// Searches for the specified value and returns the index of its first occurrence in a Matrix.
        /// </summary>
        public static int[] IndexOf(Matrix m, float value)
        {
            for (int i = 0; i < m.Rows; ++i)
            {
                for (int j = 0; j < m.Columns; ++j)
                {
                    if (m[i, j] == value)
                        return new int[] { i, j };
                }
            }

            return null;
        }

        public void ForEach(Action<float> action)
        {
            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    action.Invoke(this[i, j]);
                }
            }
        }

        /// <summary>
        /// Creates a shallow copy of the Matrix.
        /// </summary>
        public Matrix Clone()
        {
            Matrix newMatrix = new(this.Rows, this.Columns);

            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    newMatrix[i, j] = this[i, j];
                }
            }

            return newMatrix;
        }

        /// <summary> 
        /// Returns a new Matrix with the current Matrix mapped to a Func.
        /// </summary>
        public Matrix Map(Func<float, float> mapMethod)
        {
            Matrix newMatrix = new(this.Rows, this.Columns);

            for (int i = 0; i < newMatrix.Rows; ++i)
            {
                for (int j = 0; j < newMatrix.Columns; ++j)
                {
                    newMatrix[i, j] = mapMethod.Invoke(this[i, j]);
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// Swaps two rows in the matrix.
        /// </summary>
        public void SwapRows(int row1, int row2)
        {
            if (row1 < 0 || row1 >= this.Rows)
            {
                throw new ArgumentException("The row " + row1 + " does not exist in the matrix");
            }
            else if (row2 < 0 || row2 >= this.Rows)
            {
                throw new ArgumentException("The row " + row2 + " does not exist in the matrix");
            }

            float[] r1 = this.GetRow(row1);
            float[] r2 = this.GetRow(row2);

            this.SetRow(row1, r2);
            this.SetRow(row2, r1);
        }
        
        /// <summary>
        /// Multiplies a row by a value.
        /// </summary>
        public void MultiplyRow(int row, float scaleValue)
        {
            if (row < 0 || row >= this.Rows)
            {
                throw new ArgumentException("The row " + row + " does not exist in the matrix");
            }

            for (int i = 0; i < this.Rows; ++i)
            {
                this[row, i] *= scaleValue;
            }
        }

        /// <summary>
        /// Adds the values from fromRow into toRow.
        /// </summary>
        public void AddRows(int fromRow, int toRow)
        {
            if (fromRow < 0 || fromRow >= this.Rows)
            {
                throw new ArgumentException("The row " + fromRow + " does not exist in the matrix");
            }
            else if (toRow < 0 || toRow >= this.Rows)
            {
                throw new ArgumentException("The row " + toRow + " does not exist in the matrix");
            }

            for (int i = 0; i < this.Rows; ++i)
            {
                this[toRow, i] += this[fromRow, i];
            }
        }

        /// <summary>
        /// Returns a transposed matrix with the rows and columns swapped.
        /// </summary>
        public Matrix GetTransposed()
        {
            Matrix newMatrix = new(this.Columns, this.Rows);

            for (int i = 0; i < this.Columns; ++i)
            {
                for (int j = 0; j < this.Rows; ++j)
                {
                    newMatrix[i, j] = this[j, i];
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// Returns an inverted version of the matrix. Returns null if the matrix does not have square dimensions.
        /// </summary>
        public Matrix GetInverse()
        {
            if (!this.IsSquare() || this.IsSingular())
            {
                return null;
            }

            Matrix newMatrix = new(this.Rows, this.Columns);

            if (newMatrix.Rows == 2)
            {
                newMatrix[0, 0] = this[1, 1];
                newMatrix[0, 1] = -1 * this[0, 1];
                newMatrix[1, 0] = -1 * this[1, 0];
                newMatrix[1, 1] = this[0, 0];

                return (float) (1f / this.GetDeterminant()) * newMatrix;
            }
            else
            {
                // Convert the matrix into a Matrix of Minors.
                for (int i = 0; i < this.Rows; ++i)
                {
                    for (int j = 0; j < this.Columns; ++j)
                    {
                        Matrix determinantMatrix = new(this.Rows - 1);

                        int rowShift = 0;

                        for (int k = 0; k < this.Rows; ++k)
                        {
                            if (i == k)
                            {
                                --rowShift;
                                continue;
                            }

                            int columnShift = 0;

                            for (int l = 0; l < this.Columns; ++l)
                            {
                                if (j == l)
                                {
                                    --columnShift;
                                    continue;
                                }

                                determinantMatrix[k + rowShift, l + columnShift] = this[k, l];
                            }
                        }

                        newMatrix[i, j] = (float) determinantMatrix.GetDeterminant();
                    }
                }

                // Add a checkerboard pattern of -1.
                for (int i = 0; i < newMatrix.Rows; ++i)
                {
                    for (int j = 0; j < newMatrix.Columns; ++j)
                    {
                        if ((i * newMatrix.Rows + j) % 2 != 0)
                        {
                            newMatrix[i, j] *= -1;
                        }
                    }
                }

                // Transpose the matrix.
                newMatrix = newMatrix.GetTransposed();

                // Multiply by 1 over the determinant.
                return (float) (1f / this.GetDeterminant()) * newMatrix;
            }
        }

        /// <summary>
        /// Returns the indentity Matrix with the square dimensions dimensions.
        /// </summary>
        public static Matrix Identity(int dimensions = DefaultSize)
        {
            Matrix newMatrix = new(dimensions);

            for (int i = 0; i < newMatrix.Rows; ++i)
            {
                newMatrix[i, i] = 1;
            }

            return newMatrix;
        }

        /// <summary>
        /// Returns a submatrix of another Matrix, with rows and columns removed. The order of the rows and columns must be sequential.
        /// </summary>
        public static Matrix SubMatrix(Matrix m, int[] rowsToRemove, int[] columnsToRemove)
        {
            Matrix newMatrix = new(m.Rows - rowsToRemove.Length, m.Columns - columnsToRemove.Length);

            int rowIndex = 0;
            int rowShift = 0;
            for (int i = 0; i < m.Rows; ++i)
            {
                if (i == rowsToRemove[rowIndex])
                {
                    --rowShift;

                    if (rowIndex < rowsToRemove.Length - 1)
                    {
                        ++rowIndex;
                    }
                    continue;
                }

                int columnIndex = 0;
                int columnShift = 0;
                for (int j = 0; j < m.Columns; ++j)
                {
                    if (j == columnsToRemove[columnIndex])
                    {
                        --columnShift;

                        if (columnIndex < columnsToRemove.Length - 1)
                        {
                            ++columnIndex;
                        }
                        continue;
                    }

                    Console.WriteLine("[" + (i + rowShift) + ", " + (j + columnShift) + "] = " + m[i, j]);
                    newMatrix[i + rowShift, j + columnShift] = m[i, j];
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// Returns an indentity Matrix with the dimensions of the Matrix. Returns null if the matrix does not have square dimensions.
        /// </summary>
        public Matrix GetIdentity()
        {
            if (!this.IsSquare())
            {
                return null;
            }

            Matrix newMatrix = new(this.Rows);

            for (int i = 0; i < newMatrix.Rows; ++i)
            {
                newMatrix[i, i] = 1;
            }

            return newMatrix;
        }

        /// <summary>
        /// Returns the determinant of a matrix.
        /// </summary>
        public float? GetDeterminant()
        {
            if (!this.IsSquare())
            {
                return null;
            }

            if (this.Rows == 2)
            {
                return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            }
            else
            {
                float? determinant = 0f;

                for (int i = 0; i < this.Columns; ++i)
                {
                    Matrix newMatrix = new(this.Rows - 1);

                    for (int j = 0; j < this.Rows - 1; ++j)
                    {
                        int columnShift = 0;

                        for (int k = 0; k < this.Columns; ++k)
                        {
                            if (k == i)
                            {
                                --columnShift;
                                continue;
                            }

                            newMatrix[j, k + columnShift] = this[j + 1, k];
                        }
                    }

                    if (i % 2 == 0)
                    {
                        determinant += this[0, i] * newMatrix.GetDeterminant();
                    }
                    else
                    {
                        determinant -= this[0, i] * newMatrix.GetDeterminant();
                    }
                }

                return determinant;
            }
        }

        /// <summary>
        /// Returns a boolean value for whether or not the Matrix is singular.
        /// </summary>
        public bool IsSingular()
        {
            return GetDeterminant() == 0f;
        }

        /// <summary>
        /// Returns a boolean value for whether or not the Matrix is square.
        /// </summary>
        public bool IsSquare()
        {
            return this.Rows == this.Columns;
        }

        #region Overrides

        public override string ToString()
        {
            string returnValue = "";

            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    returnValue += _slots[i, j];

                    if (i < this.Rows - 1 || j < this.Columns - 1)
                    {
                        returnValue += ',';
                    }
                }
                if (i < this.Rows - 1)
                {
                    returnValue += "\n";
                }
            }

            return returnValue;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Matrix))
            {
                throw new ArgumentException("Cannot compare Matrix and " + obj.GetType());
            }

            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    if (this[i, j] != (obj as Matrix)[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashNum = 0;

            for (int i = 0; i < this.Rows; ++i)
            {
                for (int j = 0; j < this.Columns; ++j)
                {
                    hashNum += (int)this[i, j];
                }
            }

            return hashNum;
        }

        #endregion

        #region Iterators

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private MatrixEnumerator GetEnumerator()
        {
            return new MatrixEnumerator(_slots);
        }

        #endregion

        #region Operator Overloads

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new ArgumentException("Matrices with incompatible dimensions cannot be operated upon.");
            }

            Matrix newMatrix = new(m1.Rows, m1.Columns);

            for (int i = 0; i < m1.Rows; ++i)
            {
                for (int j = 0; j < m1.Columns; ++j)
                {
                    newMatrix[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return newMatrix;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new ArgumentException("Matrices with incompatible dimensions cannot be operated upon.");
            }

            Matrix newMatrix = new(m1.Rows, m1.Columns);

            for (int i = 0; i < m1.Rows; ++i)
            {
                for (int j = 0; j < m1.Columns; ++j)
                {
                    newMatrix[i, j] = m1[i, j] - m2[i, j];
                }
            }

            return newMatrix;
        }

        // Scalar Multiplication.
        public static Matrix operator *(float scalar, Matrix m1)
        {
            Matrix newMatrix = new(m1.Rows, m1.Columns);

            for (int i = 0; i < m1.Rows; ++i)
            {
                for (int j = 0; j < m1.Columns; ++j)
                {
                    newMatrix[i, j] = m1[i, j] * scalar;
                }
            }

            return newMatrix;
        }

        // Matrix Multiplication.
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
            {
                throw new ArgumentException("Matrices with incompatible dimensions cannot be operated upon.");
            }

            Matrix newMatrix = new(m1.Rows, m2.Columns);

            for (int i = 0; i < newMatrix.Rows; ++i)
            {
                for (int j = 0; j < newMatrix.Columns; ++j)
                {
                    float value = 0f;
                    float[] m1Values = m1.GetRow(i);
                    float[] m2Values = m2.GetColumn(j);

                    for (int k = 0; k < m1.Columns; ++k)
                    {
                        value += m1Values[k] * m2Values[k];
                    }

                    newMatrix[i, j] = value;
                }
            }

            return newMatrix;
        }

        #endregion
    }
}
