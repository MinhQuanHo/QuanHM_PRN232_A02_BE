namespace QuanHM_PRN232_A02_BE.DTOs
{
    public class NewsDto
    {
        public string NewsArticleId { get; set; } = "";
        public string? NewsTitle { get; set; }
        public string Headline { get; set; } = "";
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public short? CategoryId { get; set; }
        public bool? NewsStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}