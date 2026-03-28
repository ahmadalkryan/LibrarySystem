using Application.Dtos.Book;
using Application.Dtos.BookCopy;
using Application.Dtos.BorrowingRecord;
using Application.Dtos.Statistics;
using Application.Dtos.WalletTransaction;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities;
using Infrastructure.CustomException;
using Infrastructure.CustomExecption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    // TO DO 
    // Lost Fine And Renew Fine ****
    public class BorrowingRecordService : IBorrowingRecordService
    {

        private readonly IRepository<BorrowingRecord> _repo;
        private readonly IRepository<BookCopy> _bookCopyRepository;
        private readonly IMapper _mapper;
        private readonly IBookCopyService _bookCopyService;
        private readonly IMemberService _memberService;
        private readonly IWalletService _wallet;
        private readonly IWalletTransactionService _walletTransactionService;

        public BorrowingRecordService( IWalletService walletService,
            IMapper mapper,IMemberService memberService,IWalletTransactionService walletTransactionService, IRepository<BookCopy> repository1 ,IRepository<BorrowingRecord> repository ,IBookCopyService bookCopyService)
        {
            _wallet = walletService;
            _mapper=mapper;
            _repo=repository;
            _walletTransactionService=walletTransactionService;
            _bookCopyService=bookCopyService;
                _memberService=memberService;
                _bookCopyRepository=repository1;
        }

        public async Task<BorrowingRecordDto> CreateBorrowing(CreateBorrowingRecordDto createBorrowingRecordDto)
        {

            if (createBorrowingRecordDto == null)
                throw new ArgumentNullException(nameof(createBorrowingRecordDto));

            if (!createBorrowingRecordDto.IsValidDueDate())
                throw new BusinessException("Due date must be in the future");


           

            var copy = await _bookCopyRepository.GetByIdAsync(createBorrowingRecordDto._bookCopyId);
            if (copy == null)
                throw new NotFoundException($"Book copy with ID {createBorrowingRecordDto._bookCopyId} not found");


            var member = await _memberService.GetMemberById(createBorrowingRecordDto._memberId);

            if (member == null)
                throw new NotFoundException($"Member with ID {createBorrowingRecordDto._memberId} not found");

            if (copy.Status != "Available")
            {
                throw new BusinessException($"Cannot borrow book. Current status: {copy.Status}");

            }

            var borrow = _mapper.Map<BorrowingRecord>(createBorrowingRecordDto);
            
            //borrow.ReturnDate = null;

            await _repo.AddAsync(borrow);
            
            copy.Status = "Borrowed";
            
            //var days = (createBorrowingRecordDto.DueDate -DateTime.Now).Days;

            await _bookCopyRepository.UpdateAsync(copy);



            await BorrowPayment(borrow);
            
            //if(!res)
            //{
            //    throw new BusinessException("$Cannot Borrow Brcause payment Failes");
            //}
            //var CreateTrans = new CreateWalletTransactionDto
            //{
            //    TransactionType ="Brorow",
            //    Amount=createBorrowingRecordDto.BorrowingCost*days,



            //}
            //;
          //  await _walletTransactionService.CreationTransaction()

            return _mapper.Map<BorrowingRecordDto>(borrow);
            
            
        }

        public async Task<bool> BorrowPayment(BorrowingRecord borrow)
        {
            var memberwallet = await _wallet.GetWalletByMemberIdAsync(borrow._memberId);

            //  var Days = (createBorrowingRecordDto.DueDate -DateTime.Now).Days;

            var Days = (int)Math.Ceiling((borrow.DueDate - DateTime.Now).TotalDays);

            var transaction = new CreateWalletTransactionDto
            {
                Amount = Days * borrow.BorrowingCost,
                TransactionType = "Borrow",
                Notes = $"{borrow.BorrowingCost}استعارة كتاب - خصم" ,
                Description = $"{borrow.BorrowingCost}استعارة كتاب - خصم",
                _walletId = memberwallet.Id,
                CreatedByUserId = borrow._userId,

                _borrowingRecordId = borrow.Id,

                _memberId = borrow._memberId

            };

            await _walletTransactionService.CreationTransaction(transaction);

            //if (res != null) return true;
            //return false;
            return true;    

        }


        public async Task<bool> DeleteBorrowingRecord(int id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetActiveBorrowingRecords()
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x.ReturnDate==null));
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetAllBorrowingRecords()
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>((await _repo.GetAllAsync()).OrderByDescending(x=>x.BorrowDate).Take(100)

                );
        }

        public async Task<BorrowingRecordDto> GetBorrowingRecordById(int id)
        {
           return  _mapper.Map<BorrowingRecordDto>(await _repo.GetByIdAsync(id));
        }

        public async Task<BorrowingRecordDto> GetBorrowingRecordByTransaction(string transaction)
        {
            return  _mapper.Map<BorrowingRecordDto>((await _repo.GetAllWitAllIncludeAsync(x => x.TransactionNumber == transaction)).FirstOrDefault());
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByBookCopyId(int bookCopyId)
        {
            var borrow = await _repo.GetAllWitAllIncludeAsync(x=>x._bookCopyId == bookCopyId);

            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(borrow);
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>((await _repo.GetAllWitAllIncludeAsync(x => x.BorrowDate >= startDate && x.BorrowDate <= endDate)).OrderByDescending(x => x.BorrowDate)


                );
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByMemberId(int memberId)
        {
           return _mapper.Map<IEnumerable<BorrowingRecordDto>>( await _repo.GetAllWitAllIncludeAsync(x=>x._memberId == memberId));
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetBorrowingRecordsByUserId(int userId)
        {
           var borrow =  _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x._userId == userId));
            return borrow;
        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecords()
        {
            //return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x.ReturnDate >= x.DueDate ));

            // ✅ الكتب المتأخرة: التي لم تُرجع وتاريخ استحقاقها مضى
            var overdueRecords = await _repo.GetAllWitAllIncludeAsync(x =>
                x.ReturnDate==null &&  // لم تُرجع بعد
                x.DueDate < DateTime.Now    // تاريخ الاستحقاق مضى
            );

            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(overdueRecords);

        }

        public async Task<IEnumerable<BorrowingRecordDto>> GetOverdueBorrowingRecordsAndNotLost()
        {
            //   return _mapper.Map<IEnumerable<BorrowingRecordDto>>(await _repo.GetAllWitAllIncludeAsync(x => x.ReturnDate >= x.DueDate && x._bookCopy.Status != "Lost"));

            var overdueRecords = await _repo.GetAllWitAllIncludeAsync(x =>
                              !x.ReturnDate.HasValue &&           // لم تُرجع بعد
                            x.DueDate < DateTime.Now &&         // تاريخ الاستحقاق مضى
                              x._bookCopy.Status =="Borrowed"     // ليست مفقودة
                                    // وليست تالفة (اختياري)
                                                      ); 
            return _mapper.Map<IEnumerable<BorrowingRecordDto>>(overdueRecords);
        }

        public async  Task<bool> IsBookCopyAvailableAsync(int bookCopyId)
        {
           var copy =  await _bookCopyService.GetBookCopyByIdAsync(bookCopyId);
            if(copy.Status == "Available")
            {
                return true;
            }
            return false;
        }



       
         public async Task<bool> MarkCopyAsLostAsync(int BorrowingId)
        {


            var borrow =await _repo.GetByIdAsync(BorrowingId);

            var copy = await _bookCopyService.GetBookCopyByIdAsync(borrow._bookCopyId);

            var success = await _bookCopyService.SetLost(copy.Id);


            if (success)
            {
                borrow.ReturnDate= DateTime.Now;

                await _repo.UpdateAsync(borrow);


                return true;




            }

            return false;

            //var success = await _bookCopyService.SetLost(copyId);

            //var borrow = (await _repo.GetAllWitAllIncludeAsync(x => x._bookCopyId == copyId && x.ReturnDate == null)).FirstOrDefault();
            //if (borrow != null)
            //{
            //    borrow.ReturnDate = DateTime.Now;
            //    await _repo.UpdateAsync(borrow);
            //}

            //return success;

        }


        //get by id 
         public async  Task<BorrowingRecordDto> RenewBorrowingAsync(int BorrowingId)
        {    
            var borrow = await _repo.GetByIdAsync(BorrowingId);

           if(borrow == null)
                throw new NotFoundException($"Borrowing record with transaction {BorrowingId} not found");

            await _bookCopyService.SetInRepair(borrow._bookCopyId);
            borrow.DueDate = borrow.DueDate.AddDays(14);            // تمديد لمدة أسبوعين

                await _repo.UpdateAsync(borrow);
            return _mapper.Map<BorrowingRecordDto>(borrow);



        }

        public async Task<BorrowingRecordDto> ReturnBookAsync(int BorrowingId)
        {
            var borrow = await _repo.GetByIdAsync(BorrowingId);
              


            if (borrow == null)
                throw new NotFoundException($"Borrowing record with transaction {BorrowingId} not found");
            
            borrow.ReturnDate = DateTime.Now;



            var borrr =   await _repo.UpdateAsync(borrow);


            await _bookCopyService.SetAvailable(borrow._bookCopyId);




            // with fine 

            if (borrr != null)
            {
                if (borrr.ReturnDate > borrr.DueDate)
                {


                    var memberid = borrr._memberId ;
                   
                     var mwmberwallet = await _wallet.GetWalletByMemberIdAsync(memberid);


                    var retdate = borrr.ReturnDate.GetValueOrDefault();
                    var dudate = borrr.DueDate;

                    var daysLate = 0;

                    daysLate = (int)Math.Ceiling((retdate - dudate).TotalDays);


                   
                   


                    var trans = new CreateWalletTransactionDto
                    {
                        Amount = (decimal)(daysLate * borrr.FinePaid),
                        TransactionType = "Fine",
                        Notes = $"  {memberid} دفع الغرامة للعضو  ",
                        Description = $"  غرامة تأخير  {memberid} دفع الغرامة للعضو  ",
                        _walletId = mwmberwallet.Id,
                        CreatedByUserId = borrr._userId,
                        _memberId = memberid,

                    };
                await _walletTransactionService.CreationTransaction(trans);
                    


                }
            }


            return _mapper.Map<BorrowingRecordDto>(borrow);

        }












        Task<decimal> IBorrowingRecordService.CalculateFineAsync(int borrowingId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBorrowingRecordService.CanMemberBorrowAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        public async  Task<int> GetActiveBorrowingsCountAsync(int memberId)
        {
            var borrowings = await _repo.GetAllWitAllIncludeAsync(x => x._memberId == memberId && x.ReturnDate == null);
            return borrowings.Count();
        }



        public async Task<IEnumerable<MostBorrowedBookDto>> GetMostBorrowedBooksAsyncLastMounth(int topN , int numdays)
        {
            var fromDate = DateTime.Now.AddDays(-numdays);

            // var borrowings = await _repo.GetAllWitAllIncludeAsync(x => x.BorrowDate >= fromDate);

            var borrowings = await _repo.GetAllWithThenIncludeAsync(
                x => x.BorrowDate >= fromDate,
                n => n.Include(b => b._bookCopy).ThenInclude(c => c._book).ThenInclude(a => a._author),
                n=> n.Include(b => b._bookCopy).ThenInclude(c => c._book).ThenInclude(p => p._publisher)
                ,
                n => n.Include(b => b._bookCopy).ThenInclude(c => c._book).ThenInclude(cat => cat._category),
                n=>n.Include(b => b._bookCopy).ThenInclude(c => c._book).ThenInclude(u => u._user),

                n=>n.Include(b => b._member)


                );
            if(borrowings == null || !borrowings.Any())
            {
                return new List<MostBorrowedBookDto>();
            }


            var Mostborrwing = borrowings.Where(x=>x._bookCopy != null && x._bookCopy._book != null)

                .GroupBy(b => b._bookCopy._bookId)

                .Select(g => new
                {
                    BookId = g.Key,
                    BorrowCount = g.Count(),
                    // أخذ أول نسخة للحصول على تفاصيل الكتاب
                    Book = g.First()._bookCopy._book
                })
               .OrderByDescending(x => x.BorrowCount)  // الترتيب أولاً
              .Take(topN)  // ثم أخذ العدد المطلوب
                .ToList();

            var result = Mostborrwing.Select(x => new MostBorrowedBookDto
            {
              BookDto = new BookDto
              {
                  Id = x.Book.Id,
                  Title = x.Book.Title ?? "Unknown",
                  ISBN = x.Book.ISBN ?? "Unknown",
                  Description = x.Book.Description ?? "",
                  PageCount = x.Book.PageCount,
                  Language = x.Book.Language ?? "Unknown",

                  // ✅ التحقق من null لجميع العلاقات
                  AuthorName = x.Book._author?.Name ?? "Unknown",
                  PublisherName = x.Book._publisher?.Name ?? "Unknown",
                  CategoryName = x.Book._category?.Name ?? "Unknown",
                  UserName = x.Book._user?.Username ?? "Unknown",
                  _userId = x.Book._userId

              },
                // يمكن إضافة BorrowCount إذا أضفته إلى BookDto
                BorrowCount = x.BorrowCount 
            }).ToList();

            return result;

        }

      

        

       public async Task<bool> FinePayment(CreateBorrowingRecordDto createBorrowingRecordDto)
        {
            var memberwallet = await _wallet.GetWalletByMemberIdAsync(createBorrowingRecordDto._memberId);
            var Days = (DateTime.Now - createBorrowingRecordDto.DueDate).Days;
            var trans = new CreateWalletTransactionDto
            {
                Amount = Days * createBorrowingRecordDto.BorrowingCost,
                TransactionType = "Fine",
                Notes = null,
                Description = null,
                _walletId = memberwallet.Id,
                CreatedByUserId = createBorrowingRecordDto._userId,
                _memberId = createBorrowingRecordDto._memberId

            };
            var res = _walletTransactionService.CreationTransaction(trans);
            if (res != null) return true;
            return false;

        }
    }
}






// optimization query by take Top 100 
// optimization query and Take last 30 day






// update All Dates in Borrwings 
// Update Status for copy when return