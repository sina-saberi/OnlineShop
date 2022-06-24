namespace Shop.Infrastructure.Model
{
    public class Configs
    {
        public string TokenKey { get; set; } = "";
        public int TokenTimeout { get; set; }
        public int RefreshTokenTimeout { get; set; }
        public string MediaPath { get; set; } = "";
        public string FileEncriptionKey { set; get; } = "";
        public string? CurrentDirectory { get; set; }

    }
}
