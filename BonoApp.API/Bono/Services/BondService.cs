using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Domain.Repositories;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Domain.Services.Communication;
using BonoApp.API.Shared.Domain.Repositories;
using BonoApp.API.User.Domain.Repositories;

namespace BonoApp.API.Bono.Services
{
    public class BondService : IBondService
    {
        private readonly IBondRepository _bondRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BondService(IBondRepository bondRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _bondRepository = bondRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Bond>> ListAsync()
        {
            return await _bondRepository.ListAsync();
        }

        public async Task<IEnumerable<Bond>> ListByUserIdAsync(int userId)
        {
            return await _bondRepository.FindByUserId(userId);
        }

        public async Task<BondResponse> SaveAsync(Bond bond)
        {
            var existingUser = _userRepository.FindByIdAsync(bond.UserId);

            if (existingUser.Result == null)
                return new BondResponse("Invalid User");

            try
            {
                await _bondRepository.AddAsync(bond);
                await _unitOfWork.CompleteAsync();

                return new BondResponse(bond);
            }
            catch (Exception e)
            {
                return new BondResponse($"An error occurred while saving the item: {e.Message}");
            }
        }

        public async Task<BondResponse> UpdateAsync(int id, Bond bond)
        {
            var existingBond = await _bondRepository.FindByIdAsync(id);
            
            if (existingBond == null)
                return new BondResponse("Bond not found");
            
            var existingUser = _userRepository.FindByIdAsync(bond.UserId);

            if (existingUser.Result == null)
                return new BondResponse("Invalid User");

            existingBond.NominalValue = bond.NominalValue;
            existingBond.CommercialValue = bond.CommercialValue;
            existingBond.NumberAnios = bond.NumberAnios;
            existingBond.CouponFrequency = bond.CouponFrequency;
            existingBond.DayByAnios = bond.DayByAnios;
            existingBond.RateType = bond.RateType;
            existingBond.Capitalization = bond.Capitalization;
            existingBond.InterestRate = bond.InterestRate;
            existingBond.Discount = bond.Discount;
            existingBond.IncomeTax = bond.IncomeTax;
            existingBond.BroadcastDate = bond.BroadcastDate;
            existingBond.UserId = bond.UserId;

            try
            {
                _bondRepository.Update(existingBond);
                await _unitOfWork.CompleteAsync();

                return new BondResponse(existingBond);
            }
            catch (Exception e)
            {
                return new BondResponse($"An error occurred while saving the bond: {e.Message}");
            }
        }

        public async Task<BondResponse> DeleteAsync(int id)
        {
            var existingBond = await _bondRepository.FindByIdAsync(id);
            
            if (existingBond == null)
                return new BondResponse("Bond not found");
            
            try
            {
                _bondRepository.Remove(existingBond);
                await _unitOfWork.CompleteAsync();

                return new BondResponse(existingBond);
            }
            catch (Exception e)
            {
                return new BondResponse($"An error occurred while saving the bond: {e.Message}");
            }
        }
    }
}