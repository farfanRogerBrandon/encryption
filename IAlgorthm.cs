using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos
{
    public interface IAlgorthm
    {
        public void ToEncrypt(string data);

        public string GetName();
    }
}
