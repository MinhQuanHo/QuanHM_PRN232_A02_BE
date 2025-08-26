namespace QuanHM_PRN232_A02_BE.DTOs
{
    public class DashboardDto
    {
        public int TotalNews { get; set; }
        public int PublishedNews { get; set; }
        public int DraftNews { get; set; }
        public int TotalUsers { get; set; }
        public int StaffCount { get; set; }
        public int LecturerCount { get; set; }

        public List<string>? TopCategories { get; set; }
        public List<string>? LatestNews { get; set; }
    }
}
