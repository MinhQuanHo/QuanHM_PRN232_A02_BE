namespace QuanHM_PRN232_A02_BE.DTOs
{
    public class CategoryDto
    {
        public short CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public string CategoryDesciption { get; set; } = "";
        public bool? IsActive { get; set; }
    }
}