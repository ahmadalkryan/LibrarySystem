// src/api/authorService.js
// eslint-disable-next-line no-unused-vars
// src/api/authorService.js
import api from './apiConfig';

class AuthorService {

    // 🧹 معالجة الأخطاء
    handleError(error, context = '') {
        if (error.response) {
            console.error(`❌ Server Error [${context}]:`, error.response.data);
            console.error('Status:', error.response.status);

            if (error.response.status === 401) {
                // تمت المعالجة في interceptor
                console.warn('🔐 Unauthorized access');
            } else if (error.response.status === 403) {
                console.warn('🚫 Forbidden - Insufficient permissions');
            } else if (error.response.status === 404) {
                console.warn('🔍 Not Found - Author not found');
            }
        } else if (error.request) {
            console.error(`🌐 Network Error [${context}]: No response from server`);
        } else {
            console.error(`🔧 Request Error [${context}]:`, error.message);
        }
    }

    // 📥 جلب كل المؤلفين
    async getAllAuthors() {
        try {
            const response = await api.get('/Author/GetAllAuthor');
            console.log('Get all authors response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب المؤلفين');
            }
        } catch (error) {
            this.handleError(error, 'getAllAuthors');
            throw error;
        }
    }

    // 🔍 جلب مؤلف حسب ID
    async getAuthorById(id) {
        try {
            const response = await api.get('/Author/GetAuthorById', {
                params: { id }
            });
            console.log('Get author by id response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'المؤلف غير موجود');
            }
        } catch (error) {
            this.handleError(error, `getAuthorById:${id}`);
            throw error;
        }
    }

    // ➕ إنشاء مؤلف جديد
    async createAuthor(authorData) {
        try {
            // التأكد من أن التاريخ بصيغة صحيحة
            const formattedData = {
                name: authorData.name,
                biography: authorData.biography,
                nationality: authorData.nationality,
                birthDate: authorData.birthDate ? new Date(authorData.birthDate).toISOString() : null,
                deathDate: authorData.deathDate ? new Date(authorData.deathDate).toISOString() : null
            };

            const response = await api.post('/Author/CreateAuthor', formattedData);
            console.log('Create author response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل إنشاء المؤلف');
            }
        } catch (error) {
            this.handleError(error, 'createAuthor');
            throw error;
        }
    }

    // ✏️ تحديث مؤلف
    async updateAuthor(authorData) {
        try {
            // التأكد من أن التاريخ بصيغة صحيحة
            const formattedData = {
                id: authorData.id,
                name: authorData.name,
                biography: authorData.biography,
                nationality: authorData.nationality,
                birthDate: authorData.birthDate ? new Date(authorData.birthDate).toISOString() : null,
                deathDate: authorData.deathDate ? new Date(authorData.deathDate).toISOString() : null
            };

            const response = await api.put('/Author/UpdateAuthor', formattedData);
            console.log('Update author response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل تحديث المؤلف');
            }
        } catch (error) {
            this.handleError(error, 'updateAuthor');
            throw error;
        }
    }

    // 🗑️ حذف مؤلف
    async deleteAuthor(id) {
        try {
            const response = await api.delete('/Author/DeleteAuthor', {
                params: { id }
            });
            console.log('Delete author response:', response.data);

            if (response.data?.result) {
                return response.data.data; // true/false
            } else {
                throw new Error(response.data?.message || 'فشل حذف المؤلف');
            }
        } catch (error) {
            this.handleError(error, `deleteAuthor:${id}`);
            throw error;
        }
    }

    // 🔎 البحث عن مؤلفين (وظيفة إضافية مفيدة)
    async searchAuthors(searchTerm) {
        try {
            // إذا كانت هذه الوظيفة غير موجودة في الـ API، نستخدم الفلترة المحلية
            const authors = await this.getAllAuthors();
            return authors.filter(author =>
                author.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                author.nationality?.toLowerCase().includes(searchTerm.toLowerCase())
            );
        } catch (error) {
            this.handleError(error, 'searchAuthors');
            throw error;
        }
    }

    // 📊 الحصول على إحصائيات المؤلفين
    async getAuthorsStats() {
        try {
            const authors = await this.getAllAuthors();

            // تجميع حسب الجنسية
            const byNationality = authors.reduce((acc, author) => {
                const nationality = author.nationality || 'غير معروف';
                acc[nationality] = (acc[nationality] || 0) + 1;
                return acc;
            }, {});

            return {
                totalAuthors: authors.length,
                byNationality,
                livingAuthors: authors.filter(a => !a.deathDate).length,
                deceasedAuthors: authors.filter(a => a.deathDate).length
            };
        } catch (error) {
            this.handleError(error, 'getAuthorsStats');
            throw error;
        }
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new AuthorService();