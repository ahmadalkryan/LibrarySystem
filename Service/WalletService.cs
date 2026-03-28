using Application.Dtos.Wallet;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet> _walletRepository;
        private readonly IMapper _mapper;
        public WalletService(IMapper mapper ,IRepository<Wallet> repository)
        {
            _mapper=mapper;
            _walletRepository=repository;
        }
        public async Task<WalletDto> ActivateWalletAsync(int memberId)
        {
           var wallet =(await _walletRepository.GetAllWitAllIncludeAsync(x=>x._memberId==memberId)).FirstOrDefault();

            wallet.IsActive= true;

            await _walletRepository.UpdateAsync(wallet);

            return _mapper.Map<WalletDto>(wallet);

        }
        // her or in transaction + updateeeeeeeee
        public async  Task<bool> ChargeWalletAsync(int memberId, decimal amount)
        {
            var wallet = (await _walletRepository.GetAllWitAllIncludeAsync(x => x._memberId == memberId)).FirstOrDefault();
            if(amount <=0)
            {
                return false;
            }

            if(!wallet.IsActive)
            {
                return false;
            }
            if (wallet.Balance + amount<= wallet.MaxBalance)
            {
                wallet.Balance += amount;
                wallet.TotalCharge += amount;
                wallet.LastTransactionAt = DateTime.UtcNow;

              
                await _walletRepository.UpdateAsync(wallet);
                return true;
            }

            return false;

        }

        public async Task<WalletDto> CreateWallet(CreateWalletDto createWalletDto)
        {
            var wallet = _mapper.Map<Wallet>(createWalletDto);
             
            await _walletRepository.AddAsync(wallet);
            return _mapper.Map<WalletDto>(wallet);

        }

        public async Task<WalletDto> DeactivateWalletAsync(int memberId)
        {
            var walletmember =(await _walletRepository.GetAllWitAllIncludeAsync(x=>x._memberId== memberId)).FirstOrDefault();

            walletmember.IsActive= false;
            await _walletRepository.UpdateAsync(walletmember);
            return _mapper.Map<WalletDto>(walletmember);
        }

        public async Task<bool> DeductFromWalletAsync(int memberId, decimal deductamount)
        {
            var wallet = (await _walletRepository.GetAllWitAllIncludeAsync(x => x._memberId == memberId)).FirstOrDefault();
            if(deductamount<=0 ) return false;

            if(! wallet.IsActive) return false;

            if( wallet.Balance <  deductamount ) return false;

            if (wallet.Balance - deductamount < wallet.MinBalance) return false;

            wallet.Balance -= deductamount;
            wallet.TotalSpent += deductamount;
            wallet.LastTransactionAt = DateTime.Now;
            await _walletRepository.UpdateAsync(wallet);

            return true;

        }

        public  async Task<bool> DeleteWallet(int id)
        {
           var res = await _walletRepository.DeleteAsync(id);

            return true;
        }

        public async Task<IEnumerable<WalletDto>> GetActiveWallets()
        {
            var wallets = await _walletRepository.GetAllWitAllIncludeAsync(x=>x.IsActive== true);

            return _mapper.Map<IEnumerable<WalletDto>>(wallets);
        }

        public async Task<decimal> GetBalanceAsync(int memberId)
        {
           var MemberWallet = (await _walletRepository.GetAllWitAllIncludeAsync(x => x._memberId == memberId)).FirstOrDefault();

            return MemberWallet.Balance ;
        }

        public async Task<IEnumerable<WalletDto>> GetDeavtivateWallets()
        {
            var wallets = await _walletRepository.GetAllWitAllIncludeAsync(x => x.IsActive == false);

            return _mapper.Map<IEnumerable<WalletDto>>(wallets);

        }


        public async Task<WalletDto> GetWalletById(int id)
        {
            var wallet = await _walletRepository.GetByIdAsync(id);

            return _mapper.Map<WalletDto>(wallet);
        }


        public async Task<WalletDto> GetWalletByMemberIdAsync(int memberId)
        {
            var wallet = (await _walletRepository.GetAllWitAllIncludeAsync(x => x._memberId == memberId)).FirstOrDefault();
            if (wallet == null)
            {
                wallet = new Wallet
                {
                    _memberId = memberId,
                    Balance = 0,
                    TotalCharge = 0,
                    TotalSpent = 0,
                    IsActive = true,
                    MinBalance = 0,
                    MaxBalance = 5000,
                    CreatedAt = DateTime.Now,
                    LastTransactionAt = null
                };
                await _walletRepository.AddAsync(wallet);

               }
                return _mapper.Map<WalletDto>(wallet);
        }

        public Task<WalletDto> GetWalletSummaryAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasSufficientBalanceAsync(int memberId, decimal amount)
        {
            var wallet = (await _walletRepository.GetAllWitAllIncludeAsync(x => x._memberId == memberId)).FirstOrDefault();
            return wallet.Balance >= amount;
        }

        public async Task<WalletDto> UpdateWalletLimitsAsync(int memberId, decimal minBalance, decimal maxBalance)
        {
            var wallet = (await _walletRepository.GetAllWitAllIncludeAsync(x => x._memberId == memberId)).FirstOrDefault();
          if(wallet.Balance>=maxBalance || wallet.Balance <= minBalance)
            {
                throw new InvalidOperationException("الحد الأدنى لا يمكن أن يكون أكبر من الرصيد الحالي");

            }
            wallet.MaxBalance = maxBalance;
            wallet.MinBalance = minBalance;
            await _walletRepository.UpdateAsync(wallet);

            return _mapper.Map<WalletDto>(wallet);

        }


        public async void UpddatetransactionPramater(UpdateWalletDto updateWalletDto)
        {
            var wallet = (await _walletRepository.GetAllWitAllIncludeAsync(x => x._memberId == updateWalletDto._memberId)).FirstOrDefault();
            wallet.TotalSpent= updateWalletDto.TotalSpent;
            wallet.LastTransactionAt =updateWalletDto.LastTransactionAt;
            wallet.TotalCharge=updateWalletDto.TotalCharge;

            await _walletRepository.UpdateAsync(wallet);

        }
    }
}
