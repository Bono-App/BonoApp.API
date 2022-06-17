using System;

namespace BonoApp.API.Bono.Resources
{
    public class BondResource
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
        public int UserId { get; set; }
    }
}