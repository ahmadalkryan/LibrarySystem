// src/api/ReportsService.js
import api from '../api/apiConfig';

class ReportsService {

    // 🧹 معالجة الأخطاء
    handleError(error, context = '') {
        if (error.response) {
            // خطأ من السيرفر
            console.error(`❌ Server Error [${context}]:`, error.response.data);
            console.error('Status:', error.response.status);

            if (error.response.status === 401) {
                console.warn('🔐 Unauthorized - Redirecting to login');
                localStorage.removeItem('authToken');
                window.location.href = '/login';
            }
        } else if (error.request) {
            console.error(`🌐 Network Error [${context}]: No response from server`);
        } else {
            console.error(`🔧 Request Error [${context}]:`, error.message);
        }
    }

    // ============ إحصائيات نسخ الكتب ============

    // 📊 عدد نسخ الكتب حسب الحالة
    async getBookCopiesCountByStatus(status) {
        try {
            const response = await api.get('/Reports/GetBookCopiesCountForStutus', {
                params: { stutus: status } // ✅ ملاحظة: stutus (خطأ إملائي في الـ API)
            });
            console.log('Get book copies count by status response:', response.data);

            if (response.data?.result) {
                return response.data.data; // العدد
            } else {
                throw new Error(response.data?.message || 'فشل جلب عدد نسخ الكتب حسب الحالة');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesCountByStatus:${status}`);
            throw error;
        }
    }

    // 📊 عدد نسخ الكتب لكتاب معين
    async getBookCopiesCountByBookId(bookId) {
        try {
            const response = await api.get('/Reports/GetBookCopiesCountForBook', {
                params: { id: bookId }
            });
            console.log('Get book copies count by book id response:', response.data);

            if (response.data?.result) {
                return response.data.data; // العدد
            } else {
                throw new Error(response.data?.message || 'فشل جلب عدد نسخ الكتاب');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesCountByBookId:${bookId}`);
            throw error;
        }
    }

    // 📊 إجمالي عدد نسخ الكتب
    async getTotalBookCopiesCount() {
        try {
            const response = await api.get('/Reports/GetTotalBookCopiesCount');
            console.log('Get total book copies count response:', response.data);

            if (response.data?.result) {
                return response.data.data; // العدد
            } else {
                throw new Error(response.data?.message || 'فشل جلب إجمالي عدد نسخ الكتب');
            }
        } catch (error) {
            this.handleError(error, 'getTotalBookCopiesCount');
            throw error;
        }
    }

    // 📅 نسخ الكتب المضافة بعد تاريخ معين
    async getBookCopiesAddedAfterDate(date) {
        try {
            const response = await api.get('/Reports/GetBookCopiesThatAddedAfterDate', {
                params: { date: date.toISOString() }
            });
            console.log('Get book copies added after date response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب نسخ الكتب المضافة');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesAddedAfterDate:${date}`);
            throw error;
        }
    }

    // ============ إحصائيات الاستعارات ============

    // 👤 سجلات الاستعارة حسب المستخدم
    async getBorrowingRecordsByUserId(userId) {
        try {
            const response = await api.get('/Reports/GetBorrowingRecordsByUserId', {
                params: { userid: userId } // ✅ ملاحظة: userid بحروف صغيرة
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

    // 📅 سجلات الاستعارة حسب النطاق الزمني
    async getBorrowingRecordsByDateRange(startDate, endDate) {
        try {
            const response = await api.get('/Reports/GetBorrowingRecordsByDateRange', {
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

    // ============ إحصائيات الكتب ============

    // 📚 كتب حسب اسم التصنيف
    async getBooksByCategoryName(categoryName) {
        try {
            const response = await api.get('/Reports/GetBooksByCategoryNameAsync', {
                params: { categoryname: categoryName }
            });
            console.log('Get books by category response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب كتب التصنيف');
            }
        } catch (error) {
            this.handleError(error, `getBooksByCategoryName:${categoryName}`);
            throw error;
        }
    }

    // 📊 عدد الكتب حسب اسم التصنيف
    async getBooksCountByCategoryName(categoryName) {
        try {
            const response = await api.get('/Reports/GetBooksCountsByCategoryNameAsync', {
                params: { categoryname: categoryName }
            });
            console.log('Get books count by category response:', response.data);

            if (response.data?.result) {
                return response.data.data; // العدد
            } else {
                throw new Error(response.data?.message || 'فشل جلب عدد الكتب حسب التصنيف');
            }
        } catch (error) {
            this.handleError(error, `getBooksCountByCategoryName:${categoryName}`);
            throw error;
        }
    }

    // ============ دوال مساعدة متقدمة ============

    // 📊 لوحة التحكم الرئيسية (Dashboard)
    async getDashboardStats() {
        try {
            // تشغيل كل الطلبات بالتوازي
            const [
                totalCopies,
                availableCopies,
                borrowedCopies,
                lostCopies,
                activeBorrowings,
                overdueBorrowings,
                totalBooks
            ] = await Promise.all([
                this.getTotalBookCopiesCount(),
                this.getBookCopiesCountByStatus('Available'),
                this.getBookCopiesCountByStatus('Borrowed'),
                this.getBookCopiesCountByStatus('Lost'),
                this.getBorrowingRecordsByDateRange(
                    new Date(new Date().setMonth(new Date().getMonth() - 1)),
                    new Date()
                ),
                this.getBorrowingRecordsByDateRange(
                    new Date(new Date().setDate(new Date().getDate() - 30)),
                    new Date()
                ),
                this.getBooksCountByCategoryName('all') // إذا كانت موجودة
            ]);

            return {
                copies: {
                    total: totalCopies || 0,
                    available: availableCopies || 0,
                    borrowed: borrowedCopies || 0,
                    lost: lostCopies || 0,
                    availablePercentage: totalCopies ? Math.round((availableCopies / totalCopies) * 100) : 0
                },
                borrowings: {
                    active: activeBorrowings?.length || 0,
                    overdue: overdueBorrowings?.filter(b =>
                        !b.returnDate && new Date(b.dueDate) < new Date()
                    ).length || 0,
                    recent: activeBorrowings?.slice(0, 5) || []
                },
                totalBooks: totalBooks || 0
            };
        } catch (error) {
            this.handleError(error, 'getDashboardStats');
            throw error;
        }
    }

    // 📊 تقرير شامل حسب الفئة
    async getCategoryReport() {
        try {
            const categories = await this.getBooksByCategoryName(''); // جلب كل التصنيفات

            const categoryStats = await Promise.all(
                categories.map(async (category) => {
                    const count = await this.getBooksCountByCategoryName(category.name);
                    return {
                        categoryName: category.name,
                        bookCount: count,
                        percentage: '...' // يمكن حسابه لاحقاً
                    };
                })
            );

            return categoryStats;
        } catch (error) {
            this.handleError(error, 'getCategoryReport');
            throw error;
        }
    }

    // 📊 تقرير الاستعارات الشهري
    async getMonthlyBorrowingReport(year, month) {
        try {
            const startDate = new Date(year, month - 1, 1);
            const endDate = new Date(year, month, 0);

            const borrowings = await this.getBorrowingRecordsByDateRange(startDate, endDate);

            // تجميع حسب اليوم
            const dailyStats = {};
            borrowings.forEach(borrowing => {
                const day = new Date(borrowing.borrowDate).getDate();
                dailyStats[day] = (dailyStats[day] || 0) + 1;
            });

            return {
                month,
                year,
                totalBorrowings: borrowings.length,
                dailyBreakdown: dailyStats,
                averagePerDay: Math.round(borrowings.length / new Date(year, month, 0).getDate())
            };
        } catch (error) {
            this.handleError(error, 'getMonthlyBorrowingReport');
            throw error;
        }
    }

    // 📊 تقرير أداء المستخدمين (الموظفين)
    async getUserPerformanceReport(userId = null) {
        try {
            if (userId) {
                // تقرير لمستخدم محدد
                const borrowings = await this.getBorrowingRecordsByUserId(userId);
                return {
                    userId,
                    totalTransactions: borrowings.length,
                    lastTransaction: borrowings[0] || null
                };
            } else {
                // يمكن إضافة تقرير لكل المستخدمين إذا كانت الـ API تدعم
                return { message: 'تقرير كل المستخدمين غير مدعوم' };
            }
        } catch (error) {
            this.handleError(error, 'getUserPerformanceReport');
            throw error;
        }
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new ReportsService();