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
        public double InterestRate { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double IncomeTax { get; set; }
        [Required]
        public DateTime BroadcastDate { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}