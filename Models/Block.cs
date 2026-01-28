namespace KURS.Models
{
    public class Block
    {
        public int index { get; set; }
        public long timestamp { get; set; }
        public string? previousHash { get; set; }
        public string[]? transactions { get; set; }
        public int nonce { get; set; }
        public string? hash { get; set; }
        public string? firma { get; set; }
    }
}
