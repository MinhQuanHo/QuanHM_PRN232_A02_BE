namespace QuanHM_PRN232_A02_BE.DTOs
{
    public class AccountDto
    {
        public short AccountId { get; set; }
        public string AccountEmail { get; set; } = string.Empty;
        public string AccountPassword { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public short AccountRole { get; set; }
    }
}
