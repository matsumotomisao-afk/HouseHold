using System.ComponentModel.DataAnnotations;

namespace HouseHold.Models
{
    public class Income
    {
        public int IncomeId { get; set; }
        [Required]
        [Display(Name = "収入予定日")]
        public DateTime Posted { get; set; }
        [Required]
        [Display(Name = "所得区分")]
        public int IncomeClassId { get; set; }
        public IncomeClass? IncomeClassNavigation { get; set; }  //1対多のリレーションシップを構築するためのNavigationプロパティ

        [Required, StringLength(50)]
        [Display(Name = "所得の種類")]
        public string? TypeName { get; set; }

        [Required]
        [Display(Name = "金額")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]  //金額を通貨形式で表示するための属性
        public int Amount { get; set; } = 0; // デフォルト値を0に設定
    }
}
