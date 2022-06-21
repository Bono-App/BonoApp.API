using System;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Domain.Repositories;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Resources;
using Humanizer;

namespace BonoApp.API.Bono.Services
{
    public class BondResultService : IBondResultService
    {
        private readonly IBondRepository _bondRepository;

        public BondResultService(IBondRepository bondRepository)
        {
            _bondRepository = bondRepository;
        }

        public BondResultResource GetResultAmerican(int bondId)
        {
            BondResultResource result = new BondResultResource();
            var bond = _bondRepository.FindByIdAsync(bondId);
            result = BondStructAmerican(bond.Result, result);

            return result;
        }

        public BondResultResource GetResultFrances(int bondId)
        {
            BondResultResource result = new BondResultResource();
            var bond = _bondRepository.FindByIdAsync(bondId);
            result = BondStructFrances(bond.Result, result);

            return result;
        }

        public BondResultResource GetResultGermany(int bondId)
        {
            BondResultResource result = new BondResultResource();
            var bond = _bondRepository.FindByIdAsync(bondId);
            result = BondStructGermany(bond.Result, result);

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

        private float GetTEA(Bond resource, BondResultResource result)
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

        private float GetTEP(Bond resource, BondResultResource result)
        {
            float toPercentage = result.TEA / 100;
            float firstStep = (1 + toPercentage);
            float n = (float)result.CouponFrequency / resource.DayByAnios;
            double secondStep = (Math.Pow(firstStep, n) - 1)*100;
            return (float)secondStep;
        }

        private float GetCOK(Bond resource, BondResultResource result)
        {
            float toPercentage = resource.Discount / 100;
            float firstStep = (1 + toPercentage);
            float n = (float)result.CouponFrequency/ resource.DayByAnios;
            double secondStep = (Math.Pow(firstStep, n) - 1)*100;
            return (float)secondStep;
        }

        private float GetCostTransmisor(Bond resource)
        {
            float toPercentage = (resource.Structure + resource.Placement + resource.Floatation + resource.Cavali)/100;
            return toPercentage * resource.CommercialValue;
        }

        private float GetCostBondHolder(Bond resource)
        {
            float toPercentage = (resource.Floatation + resource.Cavali)/100;
            return toPercentage * resource.CommercialValue;
        }

        private void GetDataAmerican(Bond resource, BondResultResource result)
        {
            float interest = (result.TEP / 100);
            float bond=resource.NominalValue;
            float coupon=-bond*interest;
            
            float amort=0; //Diff
            double cuotes = coupon+amort; //Diff
            float prima = 0; //Diff
            
            float flujoEmisor = (float)cuotes + prima;
            float flujoBonista = -flujoEmisor;
            float COK=1+(result.COK/100);
            float VAN = flujoBonista/COK;

            double flujoAct = flujoBonista / Math.Pow(1+(result.COK/100), 1);
            float FA = (float)flujoAct * 1 * ((float)result.CouponFrequency / resource.DayByAnios);
            float convexity = (float)flujoAct * 1 * (1 + 1);
            
            double valueI = 0;
            float allFlujoAct = (float)flujoAct;
            float allFa = FA;
            float allConvexity = convexity;
            float costs = (-resource.CommercialValue) - result.CostBondHolder;
            
            for (int i = 2; i <= result.TotalPeriods; i++)
            {
                bond = bond + amort;
                coupon = -bond*interest;
                if (i == result.TotalPeriods)
                {
                    prima=-((resource.Prima / 100) * resource.NominalValue);
                    amort=-bond;
                }
                cuotes = coupon+amort;
                
                flujoEmisor = (float)cuotes + prima;
                flujoBonista = -flujoEmisor;
                valueI = Math.Pow(COK, i);
                VAN = VAN+(flujoBonista /(float)valueI);
                
                flujoAct = flujoBonista / Math.Pow(1+(result.COK/100), i);
                FA= (float)flujoAct * i * ((float)result.CouponFrequency / resource.DayByAnios);
                convexity = (float)flujoAct * i * (1 + i);
                
                allFlujoAct = allFlujoAct + (float)flujoAct;
                allFa = allFa + FA;
                allConvexity = allConvexity + convexity;
            }
            
            double aux=Math.Pow(1+(result.COK/100), 2)*allFlujoAct*Math.Pow((float)resource.DayByAnios/result.CouponFrequency, 2);
            
            result.VAN = VAN;
            result.UtilityOrLose = costs+VAN;
            result.Duration = allFa / allFlujoAct;
            result.Convexity = allConvexity / (float)aux;
            result.Total = result.Duration + result.Convexity;
            result.ModifiedDuration = result.Duration / (1 + (result.COK / 100));
        }

        private void GetDataFrances(Bond resource, BondResultResource result)
        {
            float interest = (result.TEP / 100);
            float bond = resource.NominalValue;
            float coupon = -bond * interest;

            double cuotes = coupon / (1 - (Math.Pow(1 + interest, -(result.TotalPeriods - 1 + 1))));
            float amort = (float)cuotes - coupon;
            float prima = 0; //Diff

            float flujoEmisor = (float)cuotes + prima;
            float flujoBonista = -flujoEmisor;
            float COK = 1 + (result.COK / 100);
            float VAN = flujoBonista / COK;

            double flujoAct = flujoBonista / Math.Pow(1 + (result.COK / 100), 1);
            float FA = (float)flujoAct * 1 * ((float)result.CouponFrequency / resource.DayByAnios);
            float convexity = (float)flujoAct * 1 * (1 + 1);

            double valueI = 0;
            float allFlujoAct = (float)flujoAct;
            float allFa = FA;
            float allConvexity = convexity;
            float costs = (-resource.CommercialValue) - result.CostBondHolder;

            for (int i = 2; i <= result.TotalPeriods; i++)
            {
                bond = bond + amort;
                coupon = -bond * interest;
                cuotes = coupon / (1 - (Math.Pow(1 + interest, -(result.TotalPeriods - i + 1))));
                amort = (float)cuotes - coupon;
                if (i == result.TotalPeriods)
                {
                    prima = -((resource.Prima / 100) * resource.NominalValue);
                }

                flujoEmisor = (float)cuotes + prima;
                flujoBonista = -flujoEmisor;
                valueI = Math.Pow(COK, i);
                VAN = VAN + (flujoBonista / (float)valueI);

                flujoAct = flujoBonista / Math.Pow(1 + (result.COK / 100), i);
                FA = (float)flujoAct * i * ((float)result.CouponFrequency / resource.DayByAnios);
                convexity = (float)flujoAct * i * (1 + i);

                allFlujoAct = allFlujoAct + (float)flujoAct;
                allFa = allFa + FA;
                allConvexity = allConvexity + convexity;
            }
            double aux=Math.Pow(1+(result.COK/100), 2)*allFlujoAct*Math.Pow((float)resource.DayByAnios/result.CouponFrequency, 2);
            
            result.VAN = VAN;
            result.UtilityOrLose = costs+VAN;
            result.Duration = allFa / allFlujoAct;
            result.Convexity = allConvexity / (float)aux;
            result.Total = result.Duration + result.Convexity;
            result.ModifiedDuration = result.Duration / (1 + (result.COK / 100));
        }
        
        private void GetDataGermany(Bond resource, BondResultResource result)
        {
            float interest = (result.TEP / 100);
            float bond=resource.NominalValue;
            float coupon=-bond*interest;
            
            float amort=-bond/(result.TotalPeriods-1+1); //Diff
            double cuotes = coupon+amort; //Diff
            float prima = 0; //Diff
            
            float flujoEmisor = (float)cuotes + prima;
            float flujoBonista = -flujoEmisor;
            float COK=1+(result.COK/100);
            float VAN = flujoBonista/COK;

            double flujoAct = flujoBonista / Math.Pow(1+(result.COK/100), 1);
            float FA = (float)flujoAct * 1 * ((float)result.CouponFrequency / resource.DayByAnios);
            float convexity = (float)flujoAct * 1 * (1 + 1);
            
            double valueI = 0;
            float allFlujoAct = (float)flujoAct;
            float allFa = FA;
            float allConvexity = convexity;
            float costs = (-resource.CommercialValue) - result.CostBondHolder;
            
            for (int i = 2; i <= result.TotalPeriods; i++)
            {
                bond = bond + amort;
                coupon = -bond*interest;
                amort=-bond/(result.TotalPeriods-i+1); 
                if (i == result.TotalPeriods)
                {
                    prima=-((resource.Prima / 100) * resource.NominalValue);
                }
                cuotes = coupon+amort;
                
                flujoEmisor = (float)cuotes + prima;
                flujoBonista = -flujoEmisor;
                valueI = Math.Pow(COK, i);
                VAN = VAN+(flujoBonista /(float)valueI);
                
                flujoAct = flujoBonista / Math.Pow(1+(result.COK/100), i);
                FA= (float)flujoAct * i * ((float)result.CouponFrequency / resource.DayByAnios);
                convexity = (float)flujoAct * i * (1 + i);
                
                allFlujoAct = allFlujoAct + (float)flujoAct;
                allFa = allFa + FA;
                allConvexity = allConvexity + convexity;
            }
            
            double aux=Math.Pow(1+(result.COK/100), 2)*allFlujoAct*Math.Pow((float)resource.DayByAnios/result.CouponFrequency, 2);
            
            result.VAN = VAN;
            result.UtilityOrLose = costs+VAN;
            result.Duration = allFa / allFlujoAct;
            result.Convexity = allConvexity / (float)aux;
            result.Total = result.Duration + result.Convexity;
            result.ModifiedDuration = result.Duration / (1 + (result.COK / 100));
        }

        private BondResultResource BondStructAmerican(Bond resource, BondResultResource result)
        {
            result.CouponFrequency = GetCouponFrequency(resource);
            result.DayCapitalization = GetDayCapitalization(resource);
            result.PeriodsPerYear = resource.DayByAnios / result.CouponFrequency;
            result.TotalPeriods = result.PeriodsPerYear * resource.NumberAnios;
            result.TEA = GetTEA(resource, result);
            result.TEP = GetTEP(resource, result);
            result.COK = GetCOK(resource,result);
            result.CostTransmisor = GetCostTransmisor(resource);
            result.CostBondHolder = GetCostBondHolder(resource);
            GetDataAmerican(resource,result);
            return result;
        }
        private BondResultResource BondStructFrances(Bond resource, BondResultResource result)
        {
            result.CouponFrequency = GetCouponFrequency(resource);
            result.DayCapitalization = GetDayCapitalization(resource);
            result.PeriodsPerYear = resource.DayByAnios / result.CouponFrequency;
            result.TotalPeriods = result.PeriodsPerYear * resource.NumberAnios;
            result.TEA = GetTEA(resource, result);
            result.TEP = GetTEP(resource, result);
            result.COK = GetCOK(resource,result);
            result.CostTransmisor = GetCostTransmisor(resource);
            result.CostBondHolder = GetCostBondHolder(resource);
            GetDataFrances(resource,result);
            return result;
        }
        
        private BondResultResource BondStructGermany(Bond resource, BondResultResource result)
        {
            result.CouponFrequency = GetCouponFrequency(resource);
            result.DayCapitalization = GetDayCapitalization(resource);
            result.PeriodsPerYear = resource.DayByAnios / result.CouponFrequency;
            result.TotalPeriods = result.PeriodsPerYear * resource.NumberAnios;
            result.TEA = GetTEA(resource, result);
            result.TEP = GetTEP(resource, result);
            result.COK = GetCOK(resource,result);
            result.CostTransmisor = GetCostTransmisor(resource);
            result.CostBondHolder = GetCostBondHolder(resource);
            GetDataGermany(resource,result);
            return result;
        }
    }
}