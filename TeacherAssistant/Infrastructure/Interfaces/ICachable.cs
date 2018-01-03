using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherAssistanct.Infrastructure.Caching
{
    public interface ICachable<T>
    {
        T GetObject(string key);
        void Cache(T data);
    }
}
