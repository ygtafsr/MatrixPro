using System;
using LinearAlgebra.MatrixExceptions;
using System.Collections.Generic;
using System.Globalization;


namespace LinearAlgebra
{

    //   _               _
    //  |                 |  
    //  |  1   2   3   4  |
    //  |                 |
    //  |  5   6   7   8  | 
    //  |                 |
    //  |  3   5   1   7  |
    //  |_               _|

    //   _           _
    //  |             |  
    //  |  1   2   3  |
    //  |             |
    //  |  5   6   7  | 
    //  |             |
    //  |  3   5   1  |
    //  |_           _|

    //   _           _  T
    //  |             |  
    //  |  *   2   3  |
    //  |             |
    //  |  5   *   7  | 
    //  |             |
    //  |  3   5   *  |
    //  |_           _|

    //   _       _  -1
    //  |         |  
    //  |  1   2  |
    //  |         |
    //  |  5   6  | 
    //  |_       _|

    public class Matrix : IFormattable
    {
        //  Base Double Array for Matrix<T> class
        double[,] _baseMatrix;

        #region Public Properties
        public int RowLength
        {
            get
            {
                return _baseMatrix.GetLength(0);
            }
        }

        public int ColumnLength
        {
            get
            {
                return _baseMatrix.GetLength(1);
            }
        }
        #endregion

        #region Matrix Query Methods
        //  Bu sorgunun yapısını tanımlama için bir arayüz oluşturulabilir.
        //	Matrixin kare matris olup olmadığını sorgular.
        public bool IsMatrixSquare
        {
            get
            {
                if (RowLength == ColumnLength)
                    return true;
                else return false;
            }
        }

        //  Bu sorgunun yapısını tanımlama için bir arayüz oluşturulabilir.
        //  Index'in matris boyutları içerisinde olup olmadığını sorgular.
        public bool IsIndexValid(int a, int b)
        {
            if ((a > RowLength && a < 0) || (b > ColumnLength && b < 0))
            {
                return false;
            }

            return true;
        }

        //  Bu sorgunun yapısını tanımlama için bir arayüz oluşturulabilir.
        //	Matrixlerin toplanıp çıkarılabilir olduğunu doğrulamak için
        //	iki matrisin satır ve sütun sayılarının eşitliğini sorgular.
        public bool IsSameDimension(Matrix matrix_2)
        {
            if ((this.RowLength != matrix_2.RowLength) ||
                (this.ColumnLength != matrix_2.ColumnLength))
                return false;
            else return true;
        }

        //  Bu sorgunun yapısını tanımlama için bir arayüz oluşturulabilir.
        //	İki matrisin çarpılır olup olmadığı kontrol edilir.
        public bool IsComformable(Matrix mtrx2)
        {
            if (ColumnLength == mtrx2.RowLength)
                return true;
            else return false;
        }
        #endregion

        #region Constructors
        public Matrix() : this(0, 0) { }
        public Matrix(int i, int j)
        {
            _baseMatrix = new double[i, j];
        }

        public Matrix(double[,] mtrx)
        {
            _baseMatrix = mtrx;
        }
        #endregion

        #region Indexer
        //  Bir matrix listesi yapısı içerisindeki verilere, bir indeks ile
        //  erişmemizi sağlayan indeksleyici
        public double this[int a, int b]
        {
            get
            {
                if (IsIndexValid(a, b))
                    return _baseMatrix[a, b];
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            set
            {
                if (IsIndexValid(a, b))
                {
                    _baseMatrix[a, b] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
        #endregion

        #region Enumerator
        /*public IEnumerable<T> GetValues
        {
            get
            {
                for (int i = 0; i < _baseMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < _baseMatrix.GetLength(1); j++)
                    {
                        yield return _baseMatrix[i, j];
                    }
                }
            }
        }*/
        #endregion

        #region Static Methods
        //  Bir diziden bir sütun matrisi oluşturur.
        public static Matrix SetColumnMatrix(double[] mtrx)
        {
            Matrix _sMatrix = new Matrix(mtrx.Length, 1);

            for (int i = 0; i < mtrx.Length; i++)
            {
                _sMatrix[i, 0] = mtrx[i];
            }

            return _sMatrix;
        }

        //  Bir diziden bir satır matrisi oluşturur.
        public static Matrix SetRowMatrix(double[] mtrx)
        {
            Matrix _sMatrix = new Matrix(1, mtrx.Length);

            for (int i = 0; i < mtrx.Length; i++)
            {
                _sMatrix[0, i] = mtrx[i];
            }

            return _sMatrix;
        }
        #endregion

        #region Algebra
        //	Matrixlerde toplama işlemi için + operatörünün 
        //	aşırı yüklenmiş bir versiyonu
        public static Matrix operator +(Matrix mtrx1, Matrix mtrx2)
        {
            int Row = mtrx1.RowLength;
            int Col = mtrx1.ColumnLength;

            if (mtrx1.IsSameDimension(mtrx2))
            {
                for (int i = 0; i < Row; i++)
                    for (int j = 0; j < Col; j++)
                    {
                        mtrx1[i, j] += mtrx2[i, j];
                    }

                return mtrx1;
            }
            else
            {
                throw new ImproperMatricesException("Matrislerin boyutları toplama işlemi için uygun değil.");
            }
        }

        //	Matrixlerde toplama işlemi için - operatörünün 
        //	aşırı yüklenmiş bir versiyonu
        public static Matrix operator -(Matrix mtrx1, Matrix mtrx2)
        {
            int Row = mtrx1.RowLength;
            int Col = mtrx1.ColumnLength;

            if (mtrx1.IsSameDimension(mtrx2))
            {
                for (int i = 0; i < Row; i++)
                    for (int j = 0; j < Col; j++)
                    {
                        mtrx1[i, j] -= mtrx2[i, j];
                    }

                return mtrx1;
            }
            else
            {
                throw new ImproperMatricesException("Matrisler çıkarma işlemi için uygun değil.");
            }
        }

        //	Matrixlerde çarpma işlemi için * operatörünün
        //	aşırı yüklenmiş bir versiyonu
        public static Matrix operator *(Matrix mtrx1, Matrix mtrx2)
        {
            if (!mtrx1.IsComformable(mtrx2))
            {
                throw new ImproperMatricesException("Matrisler çarpma işlemi için uygun değil.");

            }
            else
            {
                int Row = mtrx1.RowLength;
                int Col = mtrx1.ColumnLength;
                int Col2 = mtrx2.ColumnLength;

                Matrix matrix_3 = new Matrix(Row, Col2);

                for (int i = 0; i < Row; i++)
                    for (int j = 0; j < Col; j++)
                        for (int k = 0; k < Col2; k++)
                            matrix_3[i, k] += mtrx1[i, j] * mtrx2[j, k];

                return matrix_3;
            }
        }
        #endregion

        #region ToString Methods
        private string WhiteSpaceBuilder(int count)
        {
            string _WhiteSpace = "";
            for (int i = 0; i < count; i++)
                _WhiteSpace += " ";

            return _WhiteSpace;
        }


        public override string ToString()
        {
            return this.ToString("G", null);
        }

        public string ToString(string formatString)
        {
            return this.ToString(formatString, null);
        }

        public string ToString(string formatString, IFormatProvider provider)
        {
            int RowCount = _baseMatrix.GetLength(0);
            int ColumnCount = _baseMatrix.GetLength(1);
            string[,] StringMatrix = new string[RowCount, ColumnCount];
            int[,] CharachterCount = new int[RowCount, ColumnCount];
            int[,] WhiteSpaceCount = new int[RowCount, ColumnCount];
            string value;   // value To String
            int StringLength;
            int MaxStringLength = 0;
            int StringLengthDiff;

            string PostFix = "";    //  Matris sonuna Traspoze:T, Tersi:-1 gibi son ekler eklenebilir.
            provider = provider != null ? provider : NumberFormatInfo.InvariantInfo;

            //  Calculate StringMatrix and CharachterCount values
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    value = String.Format(provider, "{0:G}", _baseMatrix[i, j]);
                    StringMatrix[i, j] = value;

                    StringLength = value.Length;
                    CharachterCount[i, j] = StringLength;

                    //  Find Max Charachter Count
                    if (StringLength > MaxStringLength)
                    {
                        MaxStringLength = StringLength;
                    }
                }
            }

            //  Calculate WhiteSpaceMatrix values
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    if (CharachterCount[i, j] < MaxStringLength)
                    {
                        StringLengthDiff = MaxStringLength - CharachterCount[i, j];
                        WhiteSpaceCount[i, j] = StringLengthDiff;
                    }
                    else
                    {
                        WhiteSpaceCount[i, j] = 0;
                    }
                }
            }

            //  Build Result Matrix String 
            int MatrixLength = RowCount + (RowCount - 1) + 2;
            string ResultString = "\n    _  " + WhiteSpaceBuilder(ColumnCount * (MaxStringLength + 3) - 3) + " _  " + PostFix + "\n";
            ResultString += "   |  " + WhiteSpaceBuilder(ColumnCount * (MaxStringLength + 3) - 3) + "   |\n";

            for (int r = 1; r < MatrixLength; r++)
            {
                if (r == MatrixLength - 1)  //  Son satıra ulaşılmışsa işletilecek
                {
                    ResultString += "   |_ " + WhiteSpaceBuilder(ColumnCount * (MaxStringLength + 3) - 3) + "  _|\n\n";
                }
                else if (r % 2 == 0)        //  Çift sayı indeskli çıktı satırlarında işletilecek
                    ResultString += "   |  " + WhiteSpaceBuilder(ColumnCount * (MaxStringLength + 3) - 3) + "   |\n";
                else                        //  Tek sayı indeksli çıktı satırlarında işletilecek
                {
                    ResultString += "   |  ";

                    for (int c = 0; c < ColumnCount; c++)
                    {
                        ResultString += WhiteSpaceBuilder(WhiteSpaceCount[(r - 1) / 2, c]) + StringMatrix[(r - 1) / 2, c] + "   ";
                    }

                    ResultString += "|\n";
                }
            }

            return ResultString;
        }
        #endregion
    }
}
