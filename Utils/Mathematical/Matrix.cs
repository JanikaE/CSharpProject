using System;

namespace Utils.Mathematical
{
    /// <summary>
    /// 矩阵
    /// </summary>
    public class Matrix : IEquatable<Matrix>
    {
        private float[,] Values { get; set; }

        /// <summary>行数</summary>
        public int Height => Values.GetLength(0);

        /// <summary>列数</summary>
        public int Width => Values.GetLength(1);

        /// <summary>是否为方阵</summary>
        public bool IsSquare => Height == Width;

        /// <summary>是否为对称矩阵</summary>
        public bool IsSymmetric => TransposedMatrix() == this;

        /// <summary>是否为正交矩阵</summary>
        public bool IsOrthogonal => IsSquare && this * TransposedMatrix() == IdentityMatrix(Width);

        public Matrix(int height, int width, float value = 0)
        {
            if (height <= 0 || width <= 0)
                throw new ArgumentException("宽高不能为负数或0");
            Values = new float[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Values[i, j] = value;
                }
            }
        }

        public Matrix(float[,] values)
        {
            Values = values;
        }

        public float this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Height || j < 0 || j >= Width)
                    throw new IndexOutOfRangeException("数组越界");
                return Values[i, j];
            }
            set
            {
                if (i < 0 || i >= Height || j < 0 || j >= Width)
                    throw new IndexOutOfRangeException("数组越界");
                Values[i, j] = value;
            }
        }

        /// <summary>
        /// 单位矩阵
        /// </summary>
        public static Matrix IdentityMatrix(int range)
        {
            Matrix matrix = new(range, range);
            for (int i = 0; i < range; i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }

        public static Matrix operator -(Matrix me)
        {
            Matrix result = new(me.Width, me.Height);
            for (int i = 0; i < me.Height; i++)
            {
                for (int j = 0; j < me.Width; j++)
                {
                    result[i, j] = -me[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left.Width != right.Width || right.Height != left.Height)
                throw new InvalidOperationException("运算只能用于同型矩阵");
            int width = left.Width;
            int height = left.Height;
            Matrix result = new(width, height);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = left[i, j] - right[i, j];
                }
            }
            return result;
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left.Width != right.Width || right.Height != left.Height)
                throw new InvalidOperationException("运算只能用于同型矩阵");
            int width = left.Width;
            int height = left.Height;
            Matrix result = new(width, height);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = left[i, j] + right[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.Width != right.Height)
                throw new InvalidOperationException("左矩阵的列数需要等于右矩阵的行数");
            int width = right.Width;
            int height = left.Height;
            Matrix result = new(width, height);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float[] line1 = left.Line(i);
                    float[] line2 = right.Column(j);
                    for (int k = 0; k < line1.Length; k++)
                    {
                        result[i, j] += line1[k] * line2[k];
                    }
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix matrix, float scale)
        {
            Matrix result = new(matrix.Width, matrix.Height);
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    result[i, j] = matrix[i, j] * scale;
                }
            }
            return result;
        }

        public static Matrix operator *(float scale, Matrix matrix)
        {
            return matrix * scale;
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            if (left.Width != right.Width || left.Height != right.Height)
                return false;
            int height = left.Height;
            int width = left.Width;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (left[i, j] != right[i, j])
                        return false;
                }
            }
            return true;
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 哈达玛积（对应元素相乘）
        /// </summary>
        public static Matrix HadamardProduct(Matrix left, Matrix right)
        {
            if (left.Width != right.Width || right.Height != left.Height)
                throw new InvalidOperationException("运算只能用于同型矩阵");
            int width = left.Width;
            int height = left.Height;
            Matrix result = new(width, height);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = left[i, j] * right[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// 获得特定的行
        /// </summary>
        public float[] Line(int num)
        {
            if (num < 0 || num >= Height)
                throw new IndexOutOfRangeException("数组越界");
            float[] values = new float[Width];
            for (int i = 0; i < Width; i++)
            {
                values[i] = Values[num, i];
            }
            return values;
        }

        /// <summary>
        /// 获得特定的列
        /// </summary>
        public float[] Column(int num)
        {
            if (num < 0 || num >= Width)
                throw new IndexOutOfRangeException("数组越界");
            float[] values = new float[Height];
            for (int i = 0; i < Height; i++)
            {
                values[i] = Values[i, num];
            }
            return values;
        }

        /// <summary>
        /// 迹
        /// </summary>
        public float Trace()
        {
            if (!IsSquare)
                throw new InvalidOperationException("该方法只能用于方阵");
            float result = 0;
            for (int i = 0; i < Width; i++)
            {
                result += Values[i, i];
            }
            return result;
        }

        /// <summary>
        /// 行列式
        /// </summary>
        public float Determinant()
        {
            if (!IsSquare)
                throw new InvalidOperationException("该方法只能用于方阵");
            return CalculateDeterminant(Values);
        }

        /// <summary>
        /// 递归计算行列式
        /// </summary>
        private float CalculateDeterminant(float[,] matrix)
        {
            int n = matrix.GetLength(0);
            float determinant = 0;

            if (n == 2)
            {
                determinant = (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    float[,] subMatrix = new float[n - 1, n - 1];
                    for (int j = 1; j < n; j++)
                    {
                        for (int k = 0, l = 0; k < n; k++)
                        {
                            if (k != i)
                            {
                                subMatrix[j - 1, l] = matrix[j, k];
                                l++;
                            }
                        }
                    }
                    determinant += (float)Math.Pow(-1, i) * matrix[0, i] * CalculateDeterminant(subMatrix);
                }
            }
            return determinant;
        }

        /// <summary>
        /// 伴随矩阵
        /// </summary>
        public Matrix AdjointMatrix()
        {
            if (!IsSquare)
                throw new InvalidOperationException("该方法只能用于方阵");
            int n = Width;
            Matrix result = new(n, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = AlgebraicCofactor(i, j);
                }
            }
            return result;
        }

        /// <summary>
        /// 逆矩阵
        /// </summary>
        public Matrix InverseMatrix()
        {
            if (!IsSquare)
                throw new InvalidOperationException("该方法只能用于方阵");
            float determinant = Determinant();
            if (Math.Abs(determinant) < 1E-6)
                throw new InvalidOperationException("矩阵不可逆");
            int n = Width;
            Matrix result = AdjointMatrix();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] /= determinant;
                }
            }
            return result;
        }

        /// <summary>
        /// 第a行第b列的余子式
        /// </summary>
        public float Cofactor(int a, int b)
        {
            if (!IsSquare)
                throw new InvalidOperationException("该方法只能用于方阵");
            if (a < 0 || a >= Height || b < 0 || b >= Width)
                throw new IndexOutOfRangeException("数组越界");
            int n = Width;
            Matrix matrix = new(n - 1, n - 1);

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    matrix[i, j] = Values[i < a ? i : i + 1, j < b ? j : j + 1];
                }
            }
            return matrix.Determinant();
        }

        /// <summary>
        /// 第a行第b列的代数余子式
        /// </summary>
        public float AlgebraicCofactor(int a, int b)
        {
            return ((a + b) % 2 == 0 ? 1 : -1) * Cofactor(a, b);
        }

        /// <summary>
        /// 转置
        /// </summary>
        public void Transpose()
        {
            int width = Height, height = Width;
            float[,] newValues = new float[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    newValues[i, j] = Values[j, i];
                }
            }
            Values = newValues;
        }

        /// <summary>
        /// 转置矩阵
        /// </summary>
        public Matrix TransposedMatrix()
        {
            Matrix result = new(Values);
            result.Transpose();
            return result;
        }

        public bool Equals(Matrix? other)
        {
            return other is not null && this == other;
        }

        public override bool Equals(object? obj)
        {
            return obj is Matrix matrix && Equals(matrix);
        }

        public override int GetHashCode()
        {
            return Values.GetHashCode();
        }

        public override string ToString()
        {
            string result = string.Empty;
            result += "[ ";
            for (int i = 0; i < Height; i++)
            {
                result += "[";
                for (int j = 0; j < Width; j++)
                {
                    result += Values[i, j];
                    if (j != Width - 1)
                    {
                        result += ", ";
                    }
                }
                result += "]";
                if (i != Height - 1)
                {
                    result += " ";
                }
            }
            result += "]";
            return result;
        }
    }
}
