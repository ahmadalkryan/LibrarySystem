// src/api/publisherService.js


import api from '../api/apiConfig';

class PublisherService {

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

    // 📥 جلب كل الناشرين
    async getAllPublishers() {
        try {
            // ✅ ملاحظة: في الـ Controller اسم الدالة GetAllPublshers (خطأ إملائي)
            // تأكد من المسار الصحيح في الـ API
            const response = await api.get('/Publisher/GetAllPublshers');
            console.log('Get all publishers response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الناشرين');
            }
        } catch (error) {
            this.handleError(error, 'getAllPublishers');
            throw error;
        }
    }

    // 🔍 جلب ناشر حسب ID (إذا كان موجود)
    async getPublisherById(id) {
        try {
            // ✅ هذه الدالة غير موجودة في الـ Controller حالياً
            //但如果 موجودة، استخدم هذا المسار
            const response = await api.get('/Publisher/GetPublisherById', {
                params: { id }
            });
            console.log('Get publisher by id response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'الناشر غير موجود');
            }
        } catch (error) {
            this.handleError(error, `getPublisherById:${id}`);
            throw error;
        }
    }

    // ➕ إنشاء ناشر جديد
    async createPublisher(publisherData) {
        try {
            const response = await api.post('/Publisher/CreatePublisher', publisherData);
            console.log('Create publisher response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل إنشاء الناشر');
            }
        } catch (error) {
            this.handleError(error, 'createPublisher');
            throw error;
        }
    }

    // ✏️ تحديث ناشر
    async updatePublisher(publisherData) {
        try {
            const response = await api.put('/Publisher/UpdatePublisher', publisherData);
            console.log('Update publisher response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل تحديث الناشر');
            }
        } catch (error) {
            this.handleError(error, 'updatePublisher');
            throw error;
        }
    }

    // 🗑️ حذف ناشر
    async deletePublisher(id) {
        try {
            const response = await api.delete('/Publisher/DeletePublisher', {
                params: { id }
            });
            console.log('Delete publisher response:', response.data);

            if (response.data?.result) {
                return response.data.data; // true/false
            } else {
                throw new Error(response.data?.message || 'فشل حذف الناشر');
            }
        } catch (error) {
            this.handleError(error, `deletePublisher:${id}`);
            throw error;
        }
    }

    // 🔍 البحث عن ناشر بالاسم (اختياري - إذا كان موجود)
    async searchPublishers(searchTerm) {
        try {
            // ✅ هذه الدالة غير موجودة في الـ Controller حالياً
            // لكن يمكن إضافتها لاحقاً
            const response = await api.get('/Publisher/SearchPublishers', {
                params: { searchTerm }
            });
            console.log('Search publishers response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل البحث');
            }
        } catch (error) {
            this.handleError(error, `searchPublishers:${searchTerm}`);
            throw error;
        }
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new PublisherService();


