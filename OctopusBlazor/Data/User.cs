namespace OctopusBlazor.Data
{
    public class User
    {
        public string UserID { get; set; }
        public DateTime DateCreate { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
