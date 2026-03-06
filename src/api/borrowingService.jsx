// src/api/BorrowingService.js
import api from '../api/apiConfig';

class BorrowingService {

    // 🧹 معالجة الأخطاء
    handleError(error, context = '') {
        if (error.response) {
            // خطأ من السيرفر
            console.error(`❌ Server Error [${context}]:`, error.response.data);
            console.error('Status:', error.response.status);

            // معالجة خاصة لبعض الحالات
            if (error.response.status === 401) {
                console.warn('🔐 Unauthorized - Redirecting to login');
                localStorage.removeItem('authToken');
                window.location.href = '/login';
            } else if (error.response.status === 403) {
                console.warn('🚫 Forbidden - Insufficient permissions');
            } else if (error.response.status === 404) {
                console.warn('🔍 Not Found - Resource missing');
            } else if (error.response.status === 400) {
                console.warn('⚠️ Bad Request - Invalid data');
            }
        } else if (error.request) {
            // مشكلة في الشبكة
            console.error(`🌐 Network Error [${context}]: No response from server`);
        } else {
            // خطأ في إعداد الطلب
            console.error(`🔧 Request Error [${context}]:`, error.message);
        }
    }

    // 📥 جلب كل سجلات الاستعارة
    async getAllBorrowingRecords() {
        try {
            const response = await api.get('/Borrowing/GetAllBorrowingRecords');
            console.log('Get all borrowing records response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب سجلات الاستعارة');
            }
        } catch (error) {
            this.handleError(error, 'getAllBorrowingRecords');
            throw error;
        }
    }

    // 🔍 جلب سجل استعارة حسب ID
    async getBorrowingRecordById(id) {
        try {
            const response = await api.get('/Borrowing/GetBorrowingRecordById', {
                params: { id }
            });
            console.log('Get borrowing record by id response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'سجل الاستعارة غير موجود');
            }
        } catch (error) {
            this.handleError(error, `getBorrowingRecordById:${id}`);
            throw error;
        }
    }

    // 🔍 جلب سجل استعارة برقم المعاملة
    async getBorrowingRecordByTransaction(transaction) {
        try {
            const response = await api.get('/Borrowing/GetBorrowingRecordsByTransactin', {
                params: { transaction }
            });
            console.log('Get borrowing record by transaction response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'سجل الاستعارة غير موجود');
            }
        } catch (error) {
            this.handleError(error, `getBorrowingRecordByTransaction:${transaction}`);
            throw error;
        }
    }

    // ➕ إنشاء سجل استعارة جديد
    async createBorrowingRecord(borrowingData) {
        try {
            const response = await api.post('/Borrowing/CreateBorrowingRecord', borrowingData);
            console.log('Create borrowing record response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل إنشاء سجل الاستعارة');
            }
        } catch (error) {
            this.handleError(error, 'createBorrowingRecord');
            throw error;
        }
    }

    // ✏️ تحديث سجل استعارة (إرجاع كتاب)
    async updateBorrowingRecord(transaction) {
        try {
            const response = await api.get('/Borrowing/UpdateBorrowingRecord', {
                params: { transaction }
            });
            console.log('Update borrowing record response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل تحديث سجل الاستعارة');
            }
        } catch (error) {
            this.handleError(error, `updateBorrowingRecord:${transaction}`);
            throw error;
        }
    }

    // 🗑️ حذف سجل استعارة
    async deleteBorrowingRecord(id) {
        try {
            const response = await api.delete('/Borrowing/DeleteBorrowingRecord', {
                params: { id }
            });
            console.log('Delete borrowing record response:', response.data);

            if (response.data?.result) {
                return response.data.data; // true/false
            } else {
                throw new Error(response.data?.message || 'فشل حذف سجل الاستعارة');
            }
        } catch (error) {
            this.handleError(error, `deleteBorrowingRecord:${id}`);
            throw error;
        }
    }

    // 👤 جلب سجلات استعارة حسب معرف المستخدم
    async getBorrowingRecordsByUserId(userId) {
        try {
            const response = await api.get('/Borrowing/GetBorrowingRecordsByUserId', {
                params: { userid: userId } // ✅ ملاحظة: اسم الباراميتر userid
            });
            console.log('Get borrowing records by user id response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب سجلات الاستعارة للمستخدم');
            }
        } catch (error) {
            this.handleError(error, `getBorrowingRecordsByUserId:${userId}`);
            throw error;
        }
    }

    // 👥 جلب سجلات استعارة حسب معرف العضو
    async getBorrowingRecordsByMemberId(memberId) {
        try {
            const response = await api.get('/Borrowing/GetBorrowingRecordsByMemberId', {
                params: { memberID: memberId } // ✅ ملاحظة: اسم الباراميتر memberID
            });
            console.log('Get borrowing records by member id response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب سجلات الاستعارة للعضو');
            }
        } catch (error) {
            this.handleError(error, `getBorrowingRecordsByMemberId:${memberId}`);
            throw error;
        }
    }

    // 📦 جلب سجلات استعارة حسب معرف نسخة الكتاب
    async getBorrowingRecordsByBookCopyId(copyId) {
        try {
            const response = await api.get('/Borrowing/GetBorrowingRecordsByBookCopyId', {
                params: { copyId }
            });
            console.log('Get borrowing records by book copy id response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب سجلات الاستعارة للنسخة');
            }
        } catch (error) {
            this.handleError(error, `getBorrowingRecordsByBookCopyId:${copyId}`);
            throw error;
        }
    }

    // 📅 جلب سجلات استعارة حسب النطاق الزمني
    async getBorrowingRecordsByDateRange(startDate, endDate) {
        try {
            const response = await api.get('/Borrowing/GetBorrowingRecordsByDateRange', {
                params: {
                    start: startDate.toISOString(),
                    end: endDate.toISOString()
                }
            });
            console.log('Get borrowing records by date range response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب سجلات الاستعارة حسب التاريخ');
            }
        } catch (error) {
            this.handleError(error, 'getBorrowingRecordsByDateRange');
            throw error;
        }
    }

    // 🟢 جلب سجلات الاستعارة النشطة
    async getActiveBorrowingRecords() {
        try {
            const response = await api.get('/Borrowing/GetActiveBorrowingRecords');
            console.log('Get active borrowing records response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الاستعارات النشطة');
            }
        } catch (error) {
            this.handleError(error, 'getActiveBorrowingRecords');
            throw error;
        }
    }

    // 🔴 جلب سجلات الاستعارة المتأخرة
    async getOverdueBorrowingRecords() {
        try {
            const response = await api.get('/Borrowing/GetOverdueBorrowingRecords');
            console.log('Get overdue borrowing records response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الاستعارات المتأخرة');
            }
        } catch (error) {
            this.handleError(error, 'getOverdueBorrowingRecords');
            throw error;
        }
    }

    // 🔴 جلب سجلات الاستعارة المتأخرة وغير المفقودة
    async getOverdueBorrowingRecordsAndNotLost() {
        try {
            const response = await api.get('/Borrowing/GetOverdueBorrowingRecordsAndNotLost');
            console.log('Get overdue borrowing records (not lost) response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الاستعارات المتأخرة');
            }
        } catch (error) {
            this.handleError(error, 'getOverdueBorrowingRecordsAndNotLost');
            throw error;
        }
    }

    // ✅ التحقق من توفر نسخة كتاب
    async isBookCopyAvailable(bookCopyId) {
        try {
            const response = await api.get('/Borrowing/ISBookBookAvailble', {
                params: { bookcopy: bookCopyId } // ✅ ملاحظة: اسم الباراميتر bookcopy
            });
            console.log('Check book copy availability response:', response.data);

            if (response.data?.result) {
                return response.data.data; // true/false
            } else {
                throw new Error(response.data?.message || 'فشل التحقق من توفر النسخة');
            }
        } catch (error) {
            this.handleError(error, `isBookCopyAvailable:${bookCopyId}`);
            throw error;
        }
    }

    // 🏷️ تعيين نسخة كمفقودة
    async setCopyAsLost(copyId) {
        try {
            const response = await api.put('/Borrowing/SetLost', null, {
                params: { copyId }
            });
            console.log('Set copy as lost response:', response.data);

            if (response.data?.result) {
                return true;
            } else {
                throw new Error(response.data?.message || 'فشل تعيين النسخة كمفقودة');
            }
        } catch (error) {
            this.handleError(error, `setCopyAsLost:${copyId}`);
            throw error;
        }
    }

    // ===== دوال مساعدة لعملية الاستعارة والإرجاع =====

    // 📤 استعارة كتاب
    async borrowBook(bookCopyId, memberId, userId, dueDate) {
        const borrowingData = {
            _bookCopyId: bookCopyId,
            _memberId: memberId,
            _userId: userId,
            dueDate: dueDate || this.calculateDefaultDueDate()
        };

        return await this.createBorrowingRecord(borrowingData);
    }

    // 📥 إرجاع كتاب
    async returnBook(transactionNumber) {
        return await this.updateBorrowingRecord(transactionNumber);
    }

    // 📊 حساب تاريخ الاستحقاق الافتراضي (14 يوم من اليوم)
    calculateDefaultDueDate() {
        const date = new Date();
        date.setDate(date.getDate() + 14);
        return date;
    }

    // 📋 جلب إحصائيات سريعة
    async getBorrowingStats() {
        try {
            const [active, overdue, all] = await Promise.all([
                this.getActiveBorrowingRecords(),
                this.getOverdueBorrowingRecords(),
                this.getAllBorrowingRecords()
            ]);

            return {
                totalBorrowings: all.length,
                activeBorrowings: active.length,
                overdueBorrowings: overdue.length,
                returnedBorrowings: all.length - active.length
            };
        } catch (error) {
            this.handleError(error, 'getBorrowingStats');
            throw error;
        }
    }

    // 👤 جلب استعارات عضو مع تفاصيلها
    async getMemberBorrowingDetails(memberId) {
        try {
            const borrowings = await this.getBorrowingRecordsByMemberId(memberId);

            return {
                memberId,
                totalBorrowings: borrowings.length,
                activeBorrowings: borrowings.filter(b => !b.returnDate).length,
                overdueBorrowings: borrowings.filter(b => {
                    return !b.returnDate && new Date(b.dueDate) < new Date();
                }).length,
                borrowings: borrowings
            };
        } catch (error) {
            this.handleError(error, `getMemberBorrowingDetails:${memberId}`);
            throw error;
        }
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new BorrowingService();