using KairanTool.Models;

namespace KairanTool.Data.Interfaces
{
    public interface IUnitOfWork
     {
         IRepository<Kairan> KairanRepository { get; }
         IRepository<Role> RoleRepository { get; }
         void Save();
     }
    
}