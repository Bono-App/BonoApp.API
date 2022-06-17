using System;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Domain.Repositories;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Resources;

namespace BonoApp.API.Bono.Services
{
    public class BondAmericanService : IBondAmericanService
    {
        private readonly IBondRepository _bondRepository;

        public BondAmericanService(IBondRepository bondRepository)
        {
            _bondRepository = bondRepository;
        }

        public AmericanBondResource GetResult(int bondId)
        {
            AmericanBondResource result = new AmericanBondResource();
            var bond = _bondRepository.FindByIdAsync(bondId);
            result = BondStruct(bond.Result, result);

            return result;
        }

        private int GetCouponFrequency(Bond resource)
        {
            if (resource.CouponFrequency == "Diaria")
            {
                return 1;
            }
            if (resource.CouponFrequency == "Mensual")
            {
                return 30;
            }
            if (resource.CouponFrequency == "Bimestral")
            {
                return 60;
            }
            if (resource.CouponFrequency == "Trimestral")
            {
                return 90;
            }
            if (resource.CouponFrequency == "Cuatrimestral")
            {
                return 120;
            }
            if (resource.CouponFrequency == "Semestral")
            {
                return 180;
            }
            if (resource.CouponFrequency == "Anual")
            {
                return 360;
            }

            return 0;
        }

        private int GetDayCapitalization(Bond resource)
        {
            if (resource.Capitalization == "Diaria")
            {
                return 1;
            }
            if (resource.Capitalization == "Quincenal")
            {
                return 15;
            }
            if (resource.Capitalization == "Mensual")
            {
                return 30;
            }
            if (resource.Capitalization == "Bimestral")
            {
                return 60;
            }
            if (resource.Capitalization == "Trimestral")
            {
                return 90;
            }
            if (resource.Capitalization == "Cuatrimestral")
            {
                return 120;
            }
            if (resource.Capitalization == "Semestral")
            {
                return 180;
            }
            if (resource.Capitalization == "Anual")
            {
                return 360;
            }
            return 0;
        }

        private float GetTEA(Bond resource, AmericanBondResource result)
        {
            if (resource.RateType == "Efectiva")
                return resource.InterestRate;
            else
            {
                float ir = resource.InterestRate / 100;
                float n = resource.DayByAnios / result.DayCapitalization;
                float firststep = 1 + ir / n;
                double r = (Math.Pow(firststep, n) - 1) * 100;
                return (float) r;
            }
        }

        private AmericanBondResource BondStruct(Bond resource, AmericanBondResource result)
        {
            result.CouponFrequency = GetCouponFrequency(resource);
            result.DayCapitalization = GetDayCapitalization(resource);
            result.PeriodsPerYear = resource.DayByAnios / result.CouponFrequency;
            result.TotalPeriods = result.PeriodsPerYear * resource.NumberAnios;
            result.TEA = GetTEA(resource, result);
            return result;
        }
    }
}