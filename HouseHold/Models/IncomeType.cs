using System.ComponentModel.DataAnnotations;

namespace HouseHold.Models
{
    public class IncomeType
    {

        public int IncomeTypeId { get; set; }
        [Required, StringLength(50)]
        [Display(Name = "所得の種類")]
        public string TypeName { get; set; } = string.Empty;  //収入の種類（例：賞与、家賃収入、公的年金、銀行預金の利息、等）
        public int IncomeClassId { get; set; }                // Foreign key to IncomeClass
        //public IncomeClass? IncomeClass { get; set; } = null!; // Navigation property to IncomeClass
    }
}
