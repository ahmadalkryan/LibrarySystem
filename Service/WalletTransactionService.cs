using Application.Dtos.Wallet;
using Application.Dtos.WalletTransaction;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class WalletTransactionService : IWalletTransactionService
    {
        private readonly IRepository<WalletTransaction> _repo;
        private readonly IMapper _mapper;
        private readonly IWalletService _walletService;


        public WalletTransactionService( IRepository<WalletTransaction> repository ,IMapper mapper ,IWalletService walletService
            )
        {
            _repo = repository;
            _mapper = mapper;
            _walletService = walletService;
            
        } 
        public async Task<WalletTransactionDto> CreationTransaction(CreateWalletTransactionDto createWalletTransactionDto)
        {
          // var transaction =  _mapper.Map<WalletTransaction>( createWalletTransactionDto );

            

           // var wallet = await _walletService.GetWalletById(createWalletTransactionDto._walletId);


           var _BalanceBefore =   await _walletService.GetBalanceAsync(createWalletTransactionDto._memberId);
            decimal _BalanceAfter = 0;
            bool operationSuccess = false;

            switch (createWalletTransactionDto.TransactionType)
            {
                case "Charge":
                    operationSuccess = await _walletService.ChargeWalletAsync(createWalletTransactionDto._memberId, createWalletTransactionDto.Amount);
                    if (operationSuccess)
                    {
                        createWalletTransactionDto.Description = string.IsNullOrEmpty(createWalletTransactionDto.Description)
                            ? $"شحن المحفظة بمبلغ {createWalletTransactionDto.Amount} ريال"
                            : createWalletTransactionDto.Description;
                        createWalletTransactionDto.Notes = string.IsNullOrEmpty(createWalletTransactionDto.Notes)
                           ? $"شحن المحفظة بمبلغ {createWalletTransactionDto.Amount} - ل  {createWalletTransactionDto._memberId} ريال"
                           : createWalletTransactionDto.Notes;
                        _BalanceAfter = _BalanceBefore + createWalletTransactionDto.Amount;
                    }
                    break;

                case "Borrow":
                    operationSuccess = await _walletService.DeductFromWalletAsync(createWalletTransactionDto._memberId, createWalletTransactionDto.Amount);
                    if (operationSuccess)
                    {
                        createWalletTransactionDto.Description = string.IsNullOrEmpty(createWalletTransactionDto.Description)
                            ? $"استعارة كتاب - خصم {createWalletTransactionDto.Amount} ريال"
                            : createWalletTransactionDto.Description;

                        createWalletTransactionDto.Notes = string.IsNullOrEmpty(createWalletTransactionDto.Notes)
                            ? $"استعارة كتاب - خصم {createWalletTransactionDto.Amount} ريال"
                            : createWalletTransactionDto.Notes;
                        _BalanceAfter = _BalanceBefore - createWalletTransactionDto.Amount;

                    }
                    break;

                case "Fine":
                    operationSuccess = await _walletService.DeductFromWalletAsync(createWalletTransactionDto._memberId, createWalletTransactionDto.Amount);
                    if (operationSuccess)
                    {
                        createWalletTransactionDto.Description = string.IsNullOrEmpty(createWalletTransactionDto.Description)
                            ? $"غرامة تأخير - خصم {createWalletTransactionDto.Amount} ل {createWalletTransactionDto._memberId} ريال"
                            : createWalletTransactionDto.Description;
                        createWalletTransactionDto.Notes = string.IsNullOrEmpty(createWalletTransactionDto.Notes)
                           ? $"غرامة تأخير - خصم {createWalletTransactionDto.Amount} ل {createWalletTransactionDto._memberId} ريال"
                           : createWalletTransactionDto.Notes;
                        _BalanceAfter =_BalanceBefore - createWalletTransactionDto.Amount;

                    }
                    break;

                default:
                    throw new ArgumentException($"نوع المعاملة غير معروف: {createWalletTransactionDto.TransactionType}");
            }


            if (!operationSuccess)
            {
                throw new InvalidOperationException("فشلت عملية تحديث المحفظة");
            }

            var Trans = new WalletTransaction
            {
                TransactionNumber= $"TRANS-{DateTime.Now:yyyyMMdd-HHmmss}--{Guid.NewGuid()}-{createWalletTransactionDto._memberId}-",
                Amount =createWalletTransactionDto.Amount,
                CreatedAt=DateTime.Now,
                BalanceBefore =_BalanceBefore,
                BalanceAfter=  _BalanceAfter,
                Notes =createWalletTransactionDto.Notes
                ,
                Description =createWalletTransactionDto.Description,
                _walletId = createWalletTransactionDto._walletId,
                _borrowingRecordId =createWalletTransactionDto._borrowingRecordId,
                _memberId=createWalletTransactionDto._memberId,
                CreatedByUserId =createWalletTransactionDto.CreatedByUserId
                ,
                TransactionType =createWalletTransactionDto.TransactionType,
                Status ="Completed",
                
            };
          

            
            await _repo.AddAsync(Trans);

            return _mapper.Map<WalletTransactionDto>(Trans);









            //if (transaction.TransactionType=="Charge")
            //{
            //    var result = await _walletService.ChargeWalletAsync(transaction._memberId , transaction.Amount );
            //    transaction._borrowingRecordId = null;

            //    var updateWallet = new UpdateWalletDto
            //    {
            //        LastTransactionAt = DateTime.Now,
            //        TotalCharge =transaction.Amount+wallet.TotalCharge,
            //        TotalSpent=wallet.TotalSpent,

            //};
            //     _walletService.UpddatetransactionPramater(updateWallet);


            //}
            //else if (transaction.TransactionType == "Borrow")
            //{
            //    var result = await _walletService.DeductFromWalletAsync(transaction._memberId, transaction.Amount);
            //    var updateWallet = new UpdateWalletDto
            //    {
            //        LastTransactionAt = DateTime.Now,
            //        TotalCharge =  wallet.TotalCharge,
            //        TotalSpent = wallet.TotalSpent+transaction.Amount,

            //    };
            //    _walletService.UpddatetransactionPramater(updateWallet);
            //}
            //else if (transaction.TransactionType == "Fine")
            //{
            //    var result = await _walletService.DeductFromWalletAsync(transaction._memberId, transaction.Amount);
            //    var updateWallet = new UpdateWalletDto
            //    {
            //        LastTransactionAt = DateTime.Now,
            //        TotalCharge = wallet.TotalCharge,
            //        TotalSpent = wallet.TotalSpent+transaction.Amount,

            //    };
            //    _walletService.UpddatetransactionPramater(updateWallet);
            //}
           
            //transaction.BalanceAfter = await _walletService.GetBalanceAsync (transaction._memberId );
           


            //transaction.Status = "Completed";

            //await _repo.AddAsync (transaction);

            //return _mapper.Map<WalletTransactionDto>(transaction);

        }

        public async Task<bool> DeletionTransaction(int id)
        {
            var result = await _repo.DeleteAsync (id);

            return true;
        }
                // Get Fine Or charge Transaction 
        public async Task<IEnumerable<WalletTransactionDto>> GetAllBorrowTransaction()
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>(await _repo.GetAllWitAllIncludeAsync(x=>x.TransactionType=="Borrow"));
        }



        public async Task<IEnumerable<WalletTransactionDto>> GetAllTransaction()
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>(await _repo.GetAllWitAllIncludeAsync());
        }

        public async Task<Dictionary<DateTime, decimal>> GetDailyTransactionTotalsAsync(DateTime startDate, DateTime endDate)
        {
           var transaction =  await _repo.GetAllWitAllIncludeAsync(
               x=>x.CreatedAt >= startDate && x.CreatedAt <= endDate);

            var dailyTotals = transaction.GroupBy(x => x.CreatedAt.Date)
                .ToDictionary(
                g => g.Key
                ,
                g => g.Sum(x => x.Amount));
            var result = new Dictionary<DateTime, decimal>();
            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                result[date] = dailyTotals.ContainsKey(date) ? dailyTotals[date] : 0;
            }
            return result;
        }

        public  async Task<IEnumerable<WalletTransactionDto>> GetTopNTransactions(int topN = 10)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync()).OrderByDescending(x => x.CreatedAt).Take(topN));
        }

        public async Task<WalletTransactionDto> GetTransactionByIdAsync(int id)
        {
            return _mapper.Map<WalletTransactionDto>(await _repo.GetByIdAsync(id));
        }

        public async  Task<WalletTransactionDto> GetTransactionByNumberAsync(string transactionNumber)
        {
            return _mapper.Map<WalletTransactionDto >((await _repo.GetAllWitAllIncludeAsync(x => x.TransactionNumber == transactionNumber)).FirstOrDefault());
        }

        public async Task<IEnumerable<WalletTransactionDto>> GetTRansactionGreaterThan(decimal amount)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync(x => x.Amount >= amount)));
        }

        public async Task<IEnumerable<WalletTransactionDto>> GetTransactionsByBorrowingIdAsync(int borrowingRecordId)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync(x => x._borrowingRecordId == borrowingRecordId)));
        }

        public async Task<IEnumerable<WalletTransactionDto>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>(await _repo.GetAllWitAllIncludeAsync(x=>x.CreatedAt>=startDate && x.CreatedAt<= endDate));
        }

        public async Task<IEnumerable<WalletTransactionDto>> GetTransactionsByMemberIdAsync(int memberId)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync(x=>x._memberId==memberId)));
        }




        public async Task<IEnumerable<WalletTransactionDto>> GetTransactionsByStatusAsync(string status)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync(x=>x.Status==status)));
        }




        public async Task<IEnumerable<WalletTransactionDto>> GetTransactionsByTypeAsync(string transactionType)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync(x => x.TransactionType==transactionType)));
        }






        public async Task<IEnumerable<WalletTransactionDto>> GetTransactionsByUserIdAsync(int userId)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync(x => x.CreatedByUserId==userId)));
        }

        public async Task<IEnumerable<WalletTransactionDto>> GetTransactionsByWalletIdAsync(int walletId)
        {
            return _mapper.Map<IEnumerable<WalletTransactionDto>>((await _repo.GetAllWitAllIncludeAsync(x => x._walletId== walletId)));
        }

        public Task<Dictionary<string, decimal>> GetTransactionTotalsByTypeAsync(DateTime? from = null, DateTime? to = null)
        {
            throw new NotImplementedException();
        }





        public async Task<bool> UpdateStatusForTransaction(int id, string newStatus)
        {
           var trans =await _repo.GetByIdAsync(id);
            trans.Status = newStatus;

            await _repo.UpdateAsync(trans);
            return true;

        }



        // charge walllet !!!!!
        public async Task<bool> ChargeWallet(ChargeDto chargeDto)
        {
            var trans = new CreateWalletTransactionDto
            {
                Amount = chargeDto.Amount,
                TransactionType="Charge",
                CreatedByUserId=chargeDto.CreatedByUserId
                ,
                _memberId=chargeDto._memberId,
                Description=chargeDto.Description,

                Notes=chargeDto.Notes,
                _walletId=chargeDto._walletId,
                _borrowingRecordId=null

            };

            await CreationTransaction(trans);
            return true;



        }
    }
}
