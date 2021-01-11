using System;

namespace PromoCodeProject.Models
{
    public partial class ClientSecrets
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AppKey { get; set; }
        public string AppValue { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}