namespace Product.WebAPI.Consume.DTOs.ConvertingDTO
{
    public class ConvertingDTO
    {
        public Datum[] data { get; set; }
        public int statusCode { get; set; }
        public bool isSuccessful { get; set; }
        public object errors { get; set; }

        public class Datum
        {
            public string categoryId { get; set; }
            public string categoryName { get; set; }
        }
    }
}
