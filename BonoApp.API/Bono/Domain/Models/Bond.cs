using System;

namespace BonoApp.API.Bono.Domain.Models
{
    public class Bond
    {
        public int Id { get; set; }
        public long NominalValue { get; set; }
        public long CommercialValue { get; set; }
        public int NumberAnios { get; set; }
        public string CouponFrequency { get; set; }
        public int DayByAnios { get; set; }
        public string RateType { get; set; }
        public string Capitalization { get; set; }
        public float InterestRate { get; set; }
        public float Discount { get; set; }
        public float IncomeTax { get; set; }
        public DateTime BroadcastDate { get; set; }
        public float Prima { get; set; }
        public float Structure { get; set; }
        public float Floatation { get; set; }
        public float Placement { get; set; }
        public float Cavali { get; set; }
        public int UserId { get; set; }
        
        public User.Domain.Models.User User { get; set; }
    }
}