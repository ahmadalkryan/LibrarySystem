// src/api/BookService.js
import api from '../api/apiConfig';

class BookService {

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

    // 📥 جلب كل الكتب
    async getAllBooks() {
        try {
            const response = await api.get('/Book/GetAllBooks');
            console.log('Get all books response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الكتب');
            }
        } catch (error) {
            this.handleError(error, 'getAllBooks');
            throw error;
        }
    }

    // 🔍 جلب كتاب حسب ID
    async getBookById(id) {
        try {
            const response = await api.get('/Book/GetBookById', {
                params: { id }
            });
            console.log('Get book by id response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'الكتاب غير موجود');
            }
        } catch (error) {
            this.handleError(error, `getBookById:${id}`);
            throw error;
        }
    }

    // 🔍 جلب كتاب حسب ISBN
    async getBookByIsbn(isbn) {
        try {
            const response = await api.get('/Book/GetBooksByIsbn', {
                params: { isbn }
            });
            console.log('Get book by ISBN response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'الكتاب غير موجود بهذا الرقم');
            }
        } catch (error) {
            this.handleError(error, `getBookByIsbn:${isbn}`);
            throw error;
        }
    }

    // ➕ إنشاء كتاب جديد
    async createBook(bookData) {
        try {
            const response = await api.post('/Book/CreateBook', bookData);
            console.log('Create book response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل إنشاء الكتاب');
            }
        } catch (error) {
            this.handleError(error, 'createBook');
            throw error;
        }
    }

    // ✏️ تحديث كتاب
    async updateBook(bookData) {
        try {
            const response = await api.put('/Book/UpdateBook', bookData);
            console.log('Update book response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل تحديث الكتاب');
            }
        } catch (error) {
            this.handleError(error, 'updateBook');
            throw error;
        }
    }

    // 🗑️ حذف كتاب
    async deleteBook(id) {
        try {
            const response = await api.delete('/Book/DeleteBook', {
                params: { id }
            });
            console.log('Delete book response:', response.data);

            if (response.data?.result) {
                return response.data.data; // true/false
            } else {
                throw new Error(response.data?.message || 'فشل حذف الكتاب');
            }
        } catch (error) {
            this.handleError(error, `deleteBook:${id}`);
            throw error;
        }
    }

    // 📚 جلب كتب حسب المؤلف
    async getBooksByAuthor(authorId) {
        try {
            const response = await api.get('/Book/GetBooksByAuthor', {
                params: { authorId }
            });
            console.log('Get books by author response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب كتب المؤلف');
            }
        } catch (error) {
            this.handleError(error, `getBooksByAuthor:${authorId}`);
            throw error;
        }
    }

    // 📚 جلب كتب حسب التصنيف (باسم التصنيف)
    async getBooksByCategoryName(categoryName) {
        try {
            const response = await api.get('/Book/GetBooksByCategoryNameAsync', {
                params: { categoryname: categoryName }  // ✅ ملاحظة: اسم الباراميتر categoryname (كلها صغيرة)
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

    // 📊 عدد الكتب في تصنيف معين
    async getBooksCountByCategoryName(categoryName) {
        try {
            const response = await api.get('/Book/GetBooksCountsByCategoryNameAsync', {
                params: { categoryname: categoryName }
            });
            console.log('Get books count by category response:', response.data);

            if (response.data?.result) {
                return response.data.data; // العدد
            } else {
                throw new Error(response.data?.message || 'فشل جلب عدد الكتب');
            }
        } catch (error) {
            this.handleError(error, `getBooksCountByCategoryName:${categoryName}`);
            throw error;
        }
    }

    // 📚 جلب كتب حسب المستخدم
    async getBooksByUserId(userId) {
        try {
            const response = await api.get('/Book/GetBooksByUserID', {
                params: { userId }
            });
            console.log('Get books by user response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب كتب المستخدم');
            }
        } catch (error) {
            this.handleError(error, `getBooksByUserId:${userId}`);
            throw error;
        }
    }

    // 📚 جلب كتب حسب اللغة
    async getBooksByLanguage(language) {
        try {
            const response = await api.get('/Book/GetBooksByLanguage', {
                params: { language }
            });
            console.log('Get books by language response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الكتب حسب اللغة');
            }
        } catch (error) {
            this.handleError(error, `getBooksByLanguage:${language}`);
            throw error;
        }
    }

    // 📚 جلب كتب حسب الناشر
    async getBooksByPublisherId(publisherId) {
        try {
            const response = await api.get('/Book/GetBooksByPublisherId', {
                params: { id: publisherId }
            });
            console.log('Get books by publisher response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب كتب الناشر');
            }
        } catch (error) {
            this.handleError(error, `getBooksByPublisherId:${publisherId}`);
            throw error;
        }
    }

    // 🔎 بحث متقدم في الكتب
    async searchBooks(searchParams) {
        try {
            // بناء query string من معاملات البحث
            const params = {};
            if (searchParams.title) params.title = searchParams.title;
            if (searchParams.author) params.author = searchParams.author;
            if (searchParams.category) params.category = searchParams.category;
            if (searchParams.language) params.language = searchParams.language;

            const response = await api.get('/Book/SearchBooks', { params });
            console.log('Search books response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل البحث');
            }
        } catch (error) {
            this.handleError(error, 'searchBooks');
            throw error;
        }
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new BookService();