namespace Poemify.BLL.Interfaces
{
    public interface AccountSettingsService
    {
        public Task ChangePassword();
        public Task UpdateProfilePicture();
        
    }
}
