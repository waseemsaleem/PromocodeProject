namespace PromoCodeProject.Models
{
    public partial class PromoCodes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PromoCode { get; set; }
        public bool Activated { get; set; }
    }
}
