using System.ComponentModel.DataAnnotations;

namespace HouseHold.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        [Required, StringLength(50)]
        [Display(Name = "備考")]
        public string MethodName { get; set; } = string.Empty;
        public int PaymentTypeId { get; set; }                // Foreign key to PaymentType
        //public PaymentType PaymentType { get; set; } = null!; // Navigation property to PaymentType
    }
}
