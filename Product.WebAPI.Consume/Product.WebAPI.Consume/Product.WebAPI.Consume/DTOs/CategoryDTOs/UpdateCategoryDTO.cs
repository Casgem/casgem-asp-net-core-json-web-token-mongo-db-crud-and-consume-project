namespace Product.WebAPI.Consume.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public UpdateCategoryDTO()
        {

        }

        public UpdateCategoryDTO(string categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}