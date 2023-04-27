using Poemify.DAL.Entities;


namespace Poemify.BLL.Interfaces
{
    public interface IAdminService
    {
        Task<bool> ToggleUserStatusAsync(string userName);
        Task<IList<AppUser>> GetAllUsersAsync();
        Task GetUserAsync(string id);
        Task DeleteUserAsync (string id);
        Task UpdateUserAsync(AppUser user);
        Task AddAdminAsync (AppUser admin);
       
    }
}
