// src/api/BookCopyService.js
import api from '../api/apiConfig';

class BookCopyService {

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
            }
        } else if (error.request) {
            // مشكلة في الشبكة
            console.error(`🌐 Network Error [${context}]: No response from server`);
        } else {
            // خطأ في إعداد الطلب
            console.error(`🔧 Request Error [${context}]:`, error.message);
        }
    }

    // 📥 جلب كل نسخ الكتب
    async getAllBookCopies() {
        try {
            const response = await api.get('/BookCopy/GetAllBookCopy');
            console.log('Get all book copies response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب نسخ الكتب');
            }
        } catch (error) {
            this.handleError(error, 'getAllBookCopies');
            throw error;
        }
    }

    // 🔍 جلب نسخة كتاب حسب ID
    async getBookCopyById(id) {
        try {
            const response = await api.get('/BookCopy/GetBookCopyById', {
                params: { id }
            });
            console.log('Get book copy by id response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'نسخة الكتاب غير موجودة');
            }
        } catch (error) {
            this.handleError(error, `getBookCopyById:${id}`);
            throw error;
        }
    }

    // ➕ إنشاء نسخة كتاب جديدة
    async createBookCopy(bookCopyData) {
        try {
            const response = await api.post('/BookCopy/CreateBookCopy', bookCopyData);
            console.log('Create book copy response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل إنشاء نسخة الكتاب');
            }
        } catch (error) {
            this.handleError(error, 'createBookCopy');
            throw error;
        }
    }

    // ✏️ تحديث نسخة كتاب
    async updateBookCopy(bookCopyData) {
        try {
            const response = await api.put('/BookCopy/UpdateBookCopy', bookCopyData);
            console.log('Update book copy response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل تحديث نسخة الكتاب');
            }
        } catch (error) {
            this.handleError(error, 'updateBookCopy');
            throw error;
        }
    }

    // 🗑️ حذف نسخة كتاب
    async deleteBookCopy(id) {
        try {
            const response = await api.delete('/BookCopy/DeleteBookCopy', {
                params: { id }
            });
            console.log('Delete book copy response:', response.data);

            if (response.data?.result) {
                return response.data.data; // true/false
            } else {
                throw new Error(response.data?.message || 'فشل حذف نسخة الكتاب');
            }
        } catch (error) {
            this.handleError(error, `deleteBookCopy:${id}`);
            throw error;
        }
    }

    // 📚 جلب نسخ الكتب حسب معرف الكتاب
    async getBookCopiesByBookId(bookId) {
        try {
            const response = await api.get('/BookCopy/GetBookCopyByBookId', {
                params: { bookId }
            });
            console.log('Get book copies by book id response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب نسخ الكتاب');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesByBookId:${bookId}`);
            throw error;
        }
    }

    // 📊 جلب نسخ الكتب حسب الحالة
    async getBookCopiesByStatus(status) {
        try {
            const response = await api.get('/BookCopy/GetBookCopyByStatus', {
                params: { status }
            });
            console.log('Get book copies by status response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب نسخ الكتب حسب الحالة');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesByStatus:${status}`);
            throw error;
        }
    }

    // 👤 جلب نسخ الكتب حسب المستخدم
    async getBookCopiesByUserId(userId) {
        try {
            const response = await api.get('/BookCopy/GetBookCopyByUserID', {
                params: { userId }
            });
            console.log('Get book copies by user id response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب نسخ الكتب حسب المستخدم');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesByUserId:${userId}`);
            throw error;
        }
    }

    // 📖 جلب نسخ الكتب حسب عنوان الكتاب
    async getBookCopiesByTitle(title) {
        try {
            const response = await api.get('/BookCopy/GetBookCopyByTiltle', {
                params: { tiltle: title } // ✅ ملاحظة: خطأ إملائي في الـ API (tiltle)
            });
            console.log('Get book copies by title response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب نسخ الكتب حسب العنوان');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesByTitle:${title}`);
            throw error;
        }
    }

    // 🏷️ جلب نسخ الكتب حسب اسم التصنيف
    async getBookCopiesByCategoryName(categoryName) {
        try {
            const response = await api.get('/BookCopy/GetBookCopyByCategoryName', {
                params: { categoryname: categoryName }
            });
            console.log('Get book copies by category response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب نسخ الكتب حسب التصنيف');
            }
        } catch (error) {
            this.handleError(error, `getBookCopiesByCategoryName:${categoryName}`);
            throw error;
        }
    }

    // 🔢 عدد نسخ الكتب حسب الحالة
    async getBookCopiesCountByStatus(status) {
        try {
            const response = await api.get('/BookCopy/GetBookCopiesCountForStutus', {
                params: { stutus: status } // ✅ ملاحظة: خطأ إملائي (stutus)
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

    // 🔢 عدد نسخ الكتب حسب معرف الكتاب
    async getBookCopiesCountByBookId(bookId) {
        try {
            const response = await api.get('/BookCopy/GetBookCopiesCountForBook', {
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

    // 🔢 إجمالي عدد نسخ الكتب
    async getTotalBookCopiesCount() {
        try {
            const response = await api.get('/BookCopy/GetTotalBookCopiesCount');
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

    // 📅 جلب نسخ الكتب المضافة بعد تاريخ معين
    async getBookCopiesAddedAfterDate(date) {
        try {
            const response = await api.get('/BookCopy/GetBookCopiesThatAddedAfterDate', {
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

    // 🟢 تحديث حالة نسخة كتاب (دالة مساعدة)
    async updateBookCopyStatus(id, status, userId) {
        try {
            // أولاً نجيب بيانات النسخة الحالية
            const copy = await this.getBookCopyById(id);

            // نحدث الحالة فقط
            const updatedData = {
                id: copy.id,
                barcode: copy.barcode,
                status: status,
                location: copy.location,
                notes: copy.notes,
                _userId: userId
            };

            return await this.updateBookCopy(updatedData);
        } catch (error) {
            this.handleError(error, `updateBookCopyStatus:${id}`);
            throw error;
        }
    }

    // ✅ جلب النسخ المتاحة
    async getAvailableCopies() {
        return await this.getBookCopiesByStatus('Available');
    }

    // 📤 جلب النسخ المستعارة
    async getBorrowedCopies() {
        return await this.getBookCopiesByStatus('Borrowed');
    }

    // 🔴 جلب النسخ المفقودة
    async getLostCopies() {
        return await this.getBookCopiesByStatus('Lost');
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new BookCopyService();