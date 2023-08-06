namespace Poemify.BLL.Interfaces
{
    public interface IAccountSettingsService
    {
        public Task ChangePassword();
        public Task UpdateProfilePicture();
        
    }
}
