using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Interfaces
{
    public interface IPoemService
    {
        Task CreatePoem();
        Task DeletePoem();
        Task UpdatePoem();
        Task GetComments();
        
    }
}
