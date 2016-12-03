using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra.MatrixExceptions
{
    public class ImproperMatricesException:Exception
    {
        //  Spesifik işlemler için uygun olmama durumunu tanımlar.
        //  Örneğin iki matrisin çarpıma uygun olmaması durumunda
        //  gerçeklenecek bir istisnai durumdur.

        public ImproperMatricesException(string message) : base(message) { }
    }
}
