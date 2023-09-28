using Newtonsoft.Json.Linq;
using System;
using Utils.MyException;
using Utils.Tool;

namespace Utils.Mathematical
{
    /// <summary>
    /// 矩阵
    /// </summary>
    public class SquaredMatrix : IEquatable<SquaredMatrix>
    {
        private float[,] Values { get; set; }

        /// <summary>行数/列数</summary>
        public int Range => Values.GetLength(0);

        /// <summary>是否为对称矩阵</summary>
        public bool IsSymmetric => TransposedMatrix() == this;

        /// <summary>是否为正交矩阵</summary>
        public bool IsOrthogonal => this * TransposedMatrix() == IdentityMatrix(Range);

        /// <summary>
        /// <br>给定宽高与默认值构造矩阵</br>
        /// <br>若宽高为0或负数则将其设为1</br>
        /// </summary>
        public SquaredMatrix(int range, float value = 0)
        {
            if (range <= 0)
            {
                range = 1;
                Logger.Warn(new ArgumentException("宽高不能为负数或0"));
            }
            Values = new float[range, range];
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    Values[i, j] = value;
                }
            }
        }

        /// <summary>
        /// 用给定的数组初始化方阵。若数组宽高不相同，将用0补足缺失部分
        /// </summary>
        public SquaredMatrix(float[,] values)
        {
            int height = values.GetLength(0);
            int width = values.GetLength(1);
            int range = Math.Max(height, width);
            Values = new float[range, range];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Values[i, j] = values[i, j];
                }
            }
        }

        /// <summary>
        /// 矩阵i行j列位置的值
        /// </summary>
        /// <param name="i">行号</param>
        /// <param name="j">列号</param>
        /// <exception cref="IndexOutOfRangeException">数组越界</exception>
        public float this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Range || j < 0 || j >= Range)
                    throw new IndexOutOfRangeException("数组越界");
                return Values[i, j];
            }
            set
            {
                if (i < 0 || i >= Range || j < 0 || j >= Range)
                    throw new IndexOutOfRangeException("数组越界");
                Values[i, j] = value;
            }
        }

        /// <summary>
        /// 单位矩阵
        /// </summary>
        public static SquaredMatrix IdentityMatrix(int range)
        {
            SquaredMatrix matrix = new(range, range);
            for (int i = 0; i < range; i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }

        public static SquaredMatrix operator -(SquaredMatrix me)
        {
            SquaredMatrix result = new(me.Range);
            for (int i = 0; i < me.Range; i++)
            {
                for (int j = 0; j < me.Range; j++)
                {
                    result[i, j] = -me[i, j];
                }
            }
            return result;
        }

        /// <exception cref="MatrixException">运算只能用于同型矩阵</exception>
        public static SquaredMatrix operator -(SquaredMatrix left, SquaredMatrix right)
        {
            if (right.Range != left.Range)
                throw new MatrixException("运算只能用于同型矩阵");
            int range = left.Range;
            SquaredMatrix result = new(range);
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    result[i, j] = left[i, j] - right[i, j];
                }
            }
            return result;
        }

        /// <exception cref="MatrixException">运算只能用于同型矩阵</exception>
        public static SquaredMatrix operator +(SquaredMatrix left, SquaredMatrix right)
        {
            if (right.Range != left.Range)
                throw new MatrixException("运算只能用于同型矩阵");
            int range = left.Range;
            SquaredMatrix result = new(range);
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    result[i, j] = left[i, j] + right[i, j];
                }
            }
            return result;
        }

        /// <exception cref="MatrixException">左矩阵的列数需要等于右矩阵的行数</exception>
        public static SquaredMatrix operator *(SquaredMatrix left, SquaredMatrix right)
        {
            if (left.Range != right.Range)
                throw new MatrixException("左矩阵的列数需要等于右矩阵的行数");
            int range = left.Range;
            SquaredMatrix result = new(range);
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
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

        public static SquaredMatrix operator *(SquaredMatrix matrix, float scale)
        {
            SquaredMatrix result = new(matrix.Range);
            for (int i = 0; i < matrix.Range; i++)
            {
                for (int j = 0; j < matrix.Range; j++)
                {
                    result[i, j] = matrix[i, j] * scale;
                }
            }
            return result;
        }

        public static SquaredMatrix operator *(float scale, SquaredMatrix matrix)
        {
            return matrix * scale;
        }

        public static bool operator ==(SquaredMatrix left, SquaredMatrix right)
        {
            if (left.Range != right.Range)
                return false;
            int range = left.Range;
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    if (left[i, j] != right[i, j])
                        return false;
                }
            }
            return true;
        }

        public static bool operator !=(SquaredMatrix left, SquaredMatrix right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 哈达玛积（对应元素相乘）
        /// </summary>
        /// <exception cref="MatrixException">运算只能用于同型矩阵</exception>
        public static SquaredMatrix HadamardProduct(SquaredMatrix left, SquaredMatrix right)
        {
            if (right.Range != left.Range)
                throw new MatrixException("运算只能用于同型矩阵");
            int range = left.Range;
            SquaredMatrix result = new(range);
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    result[i, j] = left[i, j] * right[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// 获得特定的行
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">数组越界</exception>
        public float[] Line(int num)
        {
            if (num < 0 || num >= Range)
                throw new IndexOutOfRangeException("数组越界");
            float[] values = new float[Range];
            for (int i = 0; i < Range; i++)
            {
                values[i] = Values[num, i];
            }
            return values;
        }

        /// <summary>
        /// 获得特定的列
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">数组越界</exception>
        public float[] Column(int num)
        {
            if (num < 0 || num >= Range)
                throw new IndexOutOfRangeException("数组越界");
            float[] values = new float[Range];
            for (int i = 0; i < Range; i++)
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
            float result = 0;
            for (int i = 0; i < Range; i++)
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
        public SquaredMatrix AdjointMatrix()
        {
            int n = Range;
            SquaredMatrix result = new(n, n);
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
        /// <exception cref="MatrixException">矩阵不可逆</exception>
        public SquaredMatrix InverseMatrix()
        {
            float determinant = Determinant();
            if (Math.Abs(determinant) < 1E-6)
                throw new MatrixException("矩阵不可逆");
            int n = Range;
            SquaredMatrix result = AdjointMatrix();
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
        /// <exception cref="IndexOutOfRangeException">数组越界</exception>
        public float Cofactor(int a, int b)
        {
            if (a < 0 || a >= Range || b < 0 || b >= Range)
                throw new IndexOutOfRangeException("数组越界");
            int n = Range;
            SquaredMatrix matrix = new(n - 1, n - 1);

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
            int range = Range;
            float[,] newValues = new float[range, range];
            for (int i = 0; i < range; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    newValues[i, j] = Values[j, i];
                }
            }
            Values = newValues;
        }

        /// <summary>
        /// 转置矩阵
        /// </summary>
        public SquaredMatrix TransposedMatrix()
        {
            SquaredMatrix result = new(Values);
            result.Transpose();
            return result;
        }

        public bool Equals(SquaredMatrix? other)
        {
            return other is not null && this == other;
        }

        public override bool Equals(object? obj)
        {
            return obj is SquaredMatrix matrix && Equals(matrix);
        }

        public override int GetHashCode()
        {
            return Values.GetHashCode();
        }

        public override string ToString()
        {
            string result = "";
            result += "[ ";
            for (int i = 0; i < Range; i++)
            {
                result += "[";
                for (int j = 0; j < Range; j++)
                {
                    result += Values[i, j];
                    if (j != Range - 1)
                    {
                        result += ", ";
                    }
                }
                result += "]";
                if (i != Range - 1)
                {
                    result += " ";
                }
            }
            result += "]";
            return result;
        }
    }
}
