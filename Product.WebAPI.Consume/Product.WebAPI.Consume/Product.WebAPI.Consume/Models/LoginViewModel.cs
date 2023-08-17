namespace Product.WebAPI.Consume.Models
{
    public class LoginViewModel
    {
        public string Token { get; set; }

        public LoginViewModel()
        {
        }

        public LoginViewModel(string token)
        {
            Token = token;
        }
    }
}
