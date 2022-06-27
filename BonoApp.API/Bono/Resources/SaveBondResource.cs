using System;
using System.ComponentModel.DataAnnotations;

namespace BonoApp.API.Bono.Resources
{
    public class SaveBondResource
    {
        [Required]
        public long NominalValue { get; set; }
        [Required]
        public long CommercialValue { get; set; }
        [Required]
        public int NumberAnios { get; set; }
        [Required]
        [MaxLength(60)]
        public string CouponFrequency { get; set; }
        [Required]
        public int DayByAnios { get; set; }
        [Required]
        [MaxLength(60)]
        public string RateType { get; set; }
        [Required]
        [MaxLength(60)]
        public string Capitalization { get; set; }
        [Required]
        public float InterestRate { get; set; }
        [Required]
        public float Discount { get; set; }
        [Required]
        public float IncomeTax { get; set; }
        [Required]
        public DateTime BroadcastDate { get; set; }
        [Required]
        public float Prima { get; set; }
        [Required]
        public float Structure { get; set; }
        [Required]
        public float Placement { get; set; }
        [Required]
        public float Floatation { get; set; }
        [Required]
        public float Cavali { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}