using System;
using LinearAlgebra;

namespace MatrixProConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] _2DArray =
            {
                {1,56,12345,1,56,12345 },
                {10,47,1,10,47,1 },
                {154,1,15,154,1,15 },
                {456,85,185,456,85,185 },
                {1456,47,257,1456,47,257 },
                {1,45645,0,1,45645,0 },
                {1,56,12345,1,56,12345 },
                {10,47,1,10,47,1 },
                {154,1,15,154,1,15 },
                {456,85,185,456,85,185 },
                {1456,47,257,1456,47,257 },
                {1,45645,0,1,45645,0 }
            };

            Matrix Matrix_1 = new Matrix(_2DArray);
            Console.WriteLine(Matrix_1);
            Console.WriteLine();

            Matrix_1[3, 0] = 500;
            Console.WriteLine("After Modifying Matrix Value '456' To '500' =");
            Console.WriteLine(Matrix_1);
            
            double[] _1DArray = { 1, 2, 4, 8 };

            Matrix Matrix_2 = Matrix.SetColumnMatrix(_1DArray);
            Console.WriteLine(Matrix_2);

            double[] _1DArray_2 = { 5, 4, 7, 9 };
            Matrix Matrix_3 = Matrix.SetRowMatrix(_1DArray_2);
            Console.WriteLine(Matrix_3);

            double[,] _2DArray_2 =
            {
                {14,56,12,13 },
                {10,47,19,10 },
                {15,12,15,15 },
                {45,85,18,45 }
            };

            double[,] _2DArray_3 =
            {
                {8,56,4,28 },
                {10,11,20,15 },
                {1,3,7,10 },
                {4,8,3,5 }
            };

            Matrix Matrix_4 = new Matrix(_2DArray_2);

            Matrix Matrix_5 = new Matrix(_2DArray_3);

            Matrix Matrix_6 = Matrix_4 * Matrix_5;
            Console.WriteLine(Matrix_4 + " * " + Matrix_5 + " = " + Matrix_6);  // Çıktı hatalarını Düzelt. (DÜZELTİLDİ: Her durumda altlata çıktı olacaktır)
                                                                                //  Yanyana yazması için ToString metodunda sonda yer alan
                                                                                //  /n ifadesini çıkar.

            Matrix Matrix_7 = Matrix_4 + Matrix_5;
            Console.WriteLine(Matrix_7);
            try
            {
                Console.WriteLine("Matrix_3:");
                Console.WriteLine(Matrix_3);
                Console.WriteLine("Matrix_2:");
                Console.WriteLine(Matrix_2);
                Matrix Matrix_8 = Matrix_3 * Matrix_2;
                Console.WriteLine(Matrix_8);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            




            Console.ReadKey();
        }
    }
}
