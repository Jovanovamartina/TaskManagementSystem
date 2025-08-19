namespace TaskManagement
{
    public class AppJwtSettings
    {
        public string SecretKey { get; set; } = null!;
        public int ExpiryMinutes { get; set; }
    }
}
