namespace Product.WebAPI.Consume.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescripiton { get; set; }
        public string ProductImage { get; set; }
        public string CategoryId { get; set; }

        public UpdateProductDTO()
        {
        }

        public UpdateProductDTO(string productId, string productName, decimal productPrice, int productStock, string productDescripiton, string productImage, string categoryId)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductStock = productStock;
            ProductDescripiton = productDescripiton;
            ProductImage = productImage;
            CategoryId = categoryId;
        }
    }
}
