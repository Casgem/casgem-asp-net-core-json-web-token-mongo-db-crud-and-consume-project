namespace Product.WebAPI.Consume.Models
{
    public class TokenViewModel
    {
        public string Token { get; set; }

        public TokenViewModel(string token)
        {
            Token = token;
        }
    }
}
