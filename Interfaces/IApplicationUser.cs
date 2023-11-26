namespace mandiri_project.Interfaces
{
    public interface IApplicationUser
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string enteredPassword, string storedHash);
    }
}
