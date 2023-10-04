

namespace Domain.Entities;
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TwoStepSecret { get; set; }
        public string DateCreated { get; set; }
    }
