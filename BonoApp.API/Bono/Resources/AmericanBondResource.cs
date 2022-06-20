﻿namespace BonoApp.API.Bono.Resources
{
    public class AmericanBondResource
    {
        //public double Van { get; set; }
        public int CouponFrequency { get; set; }
        public int DayCapitalization { get; set; }
        public int PeriodsPerYear { get; set; }
        public int TotalPeriods { get; set; }
        public float TEA { get; set; }
        public float TEP { get; set; }
        public float COK { get; set; }
        public float CostTransmisor { get; set; }
        public float CostBondHolder { get; set; }
        public float VAN { get; set; }
        public float UtilityOrLose { get; set; }
        public float Duration { get; set; }
        public float Convexity { get; set; }
        public float Total { get; set; }
        public float ModifiedDuration { get; set; }
    }
}