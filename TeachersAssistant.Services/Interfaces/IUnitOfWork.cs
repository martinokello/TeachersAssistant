using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  TeachersAssistant.Services.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}
