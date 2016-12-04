using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    // İleriye doğru eleme ve geriye doğru yerine koyma için algoritmalar.
    // Algoritmalar 3x3 Matris üzerinde denenmiştir.

    // Denklemler ölçeklendirilmemiştir, ancak pivotlama kullanıp kullanılmayacağını
    // belirlemek için elemanların ölçeklenmiş değerleri kullanılmaktadır.

    // Tekil sistemleri işaretlemek için sıfıra yakın değerleri algılamak üzere
    // pivotlama aşamasında köşegen elemanları izlenmektedir.

    // Gauss Eleme prosedürlerini işleten metotlar içerir.
    public class Gauss
    {
        // Katsayılar matrisi deklare edip değer atar.
        public static double[,] matris1 =
           {
             {70,1,0 },
             {60,-1,1},
             {40,0,-1}
        };

        // Matrisin kolon sayısını belirtir. Çıktı aşamasında gerekli.
        public static int Colmatris1 = matris1.GetLength(1);

        // Sağ taraf matrisi deklare edip değer atar.
        public static double[] matris2 = { 636, 518, 307 };

        // Bir bilinmeyen 'x' matrisi deklare eder.
        public static double[] matris3 = new double[Colmatris1];

        // Dimension metodu için matris deklare eder.
        public static double[] matris4 = new double[Colmatris1];

        // Matrislerin son satırının indeksini belirtir.
        public int sonindex = matris1.GetLength(0) - 1;

        // Çıktının 2 boyutlu matris için mi, 1 boyutlu matris için mi
        // olacağını belirtir.
        public int ch;

        // 
        public int er;

        // Kullanıcı tanımlı değer. Bu değer ?
        public double tol = 0.01;

        // Matris1'in en büyük elemanlarını saklar. Daha sonra bu metot
        // ? yapacaktır.
        public void Dimension(int n)
        {
            er = 0;

            for (int i = 0; i <= n; i++)
            {
                // Katsayılar matrisinin ilk sütun elemanlarını sırayla
                // matris4'e atar.
                matris4[i] = Math.Abs(matris1[i, 0]);

                // Sırasıyla satırlardaki en büyük katsayıları matris4'ün
                // elemanları yapar. Yani matris4 her satırdaki en büyük katsayıyı
                // saklayan bir matristir.
                for (int j = 1; j <= n; j++)
                {
                    if (Math.Abs(matris1[i, j]) > matris4[i])
                        matris4[i] = matris1[i, j];
                }
            }

            // İleriye doğru eleme prosedürünü işletir.
            Eliminate(n);

            // Tekillik veya kötü koşullanma yoksa geriye doğru yerine koyma
            // prosedürünü işletir.
            if (er != -1) Substitute(n);
        }

        public void Eliminate(int n)
        {
            // Pivot denklemin çarpanını ifade eder.
            double carpan;

            //k: Pivot denklemin satır indeksini belirtir.
            int k;

            // 
            for (k = 0; k < n; k++)
            {
                // Ölçeklendirilmiş katsayılara göre pivotlama metodunu uygular.
                Pivot(n, k);

                // Burada matris1 değerlerini geçici olarak ölçeklendirir ve
                // kabul edilebilir 'tol' değerinden küçük olup olmadığı kontrol edilir.
                // Eğer ölçeklenmiş değer kabul edilebilir değerden küçük ise
                // er=-1 değeri alır ve işlemden tamamen çıkılır.
                if (Math.Abs(matris1[k, k] / matris4[k]) < tol)
                {
                    er = -1;
                    return;
                }

                // i: Eleme yapılan denklemin satır indeksini belirtir. 
                for (int i = k + 1; i <= n; i++)
                {
                    carpan = matris1[i, k] / matris1[k, k];
                    for (int j = k + 1; j <= n; j++)
                        matris1[i, j] = matris1[i, j] - carpan * matris1[k, j];

                    matris2[i] = matris2[i] - carpan * matris2[k];
                }
            }

            //
            if (Math.Abs(matris1[k, k] / matris4[k]) < tol) er = -1;

            //
            ch = 0;
            Print(n);
        }

        // Geriye doğru yerine koyma prosedürü işletir.
        public void Substitute(int n)
        {
            // Eleme sonunda kalan köşe elemanın değeri atanır.
            matris3[n] = matris2[n] / matris1[n, n];

            // Yerine koymaya son köşe elemanın alt satırınıdan başlanır.
            for (int i = n - 1; i >= 0; i--)
            {
                // i satırındaki [i,i] indeksli bilinmeyeni bulmak için
                // i satırının n'inci elemanından i+1'inci elemanına kadar olan
                // elemanları i satırında sola doğru daha önceki i döngülerinde 
                // bulunan bilinmeyenler ile teker teker çarparak toplar.
                // Daha sonra bu toplam sağ taraf matrisinden çıkarılır.
                // Böylece [i,i] indeksli eleman bulunmuş olur.
                // Döngü katsayılar matrisinde soldan sağa i+1'e kadar
                // bilinmeyenler matrisinde yukarıdan aşağıya i+1'e kadar ilerler.
                double sum = 0;
                for (int j = n; j >= i + 1; j--)
                    sum += matris1[i, j] * matris3[j];

                // iterasyon sonucu hesaplanan x değerlerini matris3'te saklar.
                matris3[i] = (matris2[i] - sum) / matris1[i, i];
            }

            // Print() metodunun hangi metottan çağrıldığını belirtmek için
            // a değişkeni deklare edilip atama yapıldı. Böylece Print() metodu
            // iki boyutlu mu, tek boyutlu mu matris yazdıracağını algılayabiliyor.
            // ileride bunun daha kolay ve sistematik bir yolu bulunursa bu yöntem
            // değiştirilebilir.
            ch = 1;
            Print(n);
        }

        // Burada n: son satır indeksi, k: pivotlanacak satırın indeksidir.
        // Pivot() metodu Dimension metodu tarafından gönderilen k satır indeksli
        // pivot eleman için pivotlama yapar. İleriki pivot elemanların pivotlamasını
        // yapmaz.
        public void Pivot(int n, int k)
        {
            // Pivot elemanın satır numarası
            int p = k;

            // big değişkeni pivot elemanın ÖLÇEKLENMİŞ mutlak değerini saklar.
            double big = Math.Abs(matris1[k, k] / matris4[k]);

            // Döngü, pivot elemanın altındaki satırlarda, değeri pivot elemanın
            // ÖLÇEKLENMİŞ değerinden daha büyük bir ÖLÇEKLENMİŞ eleman eleman 
            // olup olmadığını kontrol eder. Eğer varsa yeni
            // pivot eleman bu olur ve bu elemanın satır numarası saklanır.
            for (int ii = k + 1; ii <= n; ii++)
            {
                // Pivot satırın altındaki satırların elemanları ölçeklendirilip
                // değerleri dummy değişkeninde saklanır.
                double dummy = Math.Abs(matris1[ii, k] / matris4[ii]);

                // Eğer pivot satır altında pivot elemanın ölçeklenmiş değeri big'den
                // daha büyük bir ölçeklenmiş satır elemanı varsa (dummy)
                // Bu elemanın ölçeklenmiş değer ve satır indeksi saklanır.
                if (dummy > big)
                {
                    big = dummy;
                    p = ii;
                }
            }

            // Eğer pivot elemandan daha büyük değerleri bir eleman bulunursa
            // p, k'ya eşit olmayacaktır. Böylece aşağıdaki prosedür işler.
            if (p != k)
            {
                // Ölçeklenmiş pivot değerden daha büyük ölçeklenmiş değer.
                double dummy;

                for (int jj = k; jj <= n; jj++)
                {
                    // Pivot satırı ile pivot elemandan daha büyük elemanlı satırın
                    // elemanlarını tek tek değiştirir. Pivot satır elemanın dummy 
                    // değişkeninde saklanır. eleman büyük sayı ile değiştirilir.
                    // büyük olan elemanın yerine dummy değişkeni yazılır.
                    // pivot satırdaki tüm sütun elemanları için aynı prosedür yapılır.    

                    // Burada dummy değişkeni matris1'in pivot elemanının ÖLÇEKLENMEMİŞ değerini
                    // alır.            
                    dummy = matris1[p, jj];
                    matris1[p, jj] = matris1[k, jj];
                    matris1[k, jj] = dummy;
                }

                // Aynı şekilde sağ taraf matrisinin elemanlarını da değiştirir.
                dummy = matris2[p];
                matris2[p] = matris2[k];
                matris2[k] = dummy;

                // Aynı şeklide matris4'ün elemanlarını da değiştirir.
                dummy = matris4[p];
                matris4[p] = matris4[k];
                matris4[k] = dummy;
            }
        }

        // Sayısal sonuçları görüntülemek için metot içerir.
        public void Print(int n)
        {
            if (ch == 0)
            {
                // İleriye doğru eleme sonucu elde edilen üst üçgen
                // matrisi gösterir.
                Console.Write("Matris1 =");
                for (int i = 0; i <= n; i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j < Colmatris1; j++)
                        Console.Write("\t" + matris1[i, j]);
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write("Sütun Matrisi =");
                Console.WriteLine();
                for (int i = 0; i <= n; i++)
                    Console.Write("\t" + matris3[i]);
            }
        }
    }
}
