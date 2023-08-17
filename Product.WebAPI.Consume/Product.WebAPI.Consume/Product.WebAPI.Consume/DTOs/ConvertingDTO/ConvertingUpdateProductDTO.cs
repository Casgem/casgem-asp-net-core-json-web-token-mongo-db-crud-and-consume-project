namespace Product.WebAPI.Consume.DTOs.ConvertingDTO
{
    public class ConvertingUpdateProductDTO
    {
        public Data data { get; set; }
        public int statusCode { get; set; }
        public bool isSuccessful { get; set; }
        public object errors { get; set; }

        public class Data
        {
            public string productId { get; set; }
            public string productName { get; set; }
            public decimal productPrice { get; set; }
            public int productStock { get; set; }
            public string productDescripiton { get; set; }
            public string productImage { get; set; }
            public string categoryId { get; set; }
        }
    }
}
