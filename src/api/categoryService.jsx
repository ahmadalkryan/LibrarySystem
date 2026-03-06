////// src/services/CategoryService.js
////import api from '../api/apiConfig';

////class CategoryService {

////    // 🧹 معالجة الأخطاء
////    handleError(error, context = '') {
////        if (error.response) {
////            // خطأ من السيرفر
////            console.error(`❌ Server Error [${context}]:`, error.response.data);
////            console.error('Status:', error.response.status);

////            // معالجة خاصة لبعض الحالات
////            if (error.response.status === 401) {
////                console.warn('🔐 Unauthorized - Redirecting to login');
////                localStorage.removeItem('authToken');
////                window.location.href = '/login';
////            } else if (error.response.status === 403) {
////                console.warn('🚫 Forbidden - Insufficient permissions');
////            } else if (error.response.status === 404) {
////                console.warn('🔍 Not Found - Resource missing');
////            }
////        } else if (error.request) {
////            // مشكلة في الشبكة
////            console.error(`🌐 Network Error [${context}]: No response from server`);
////        } else {
////            // خطأ في إعداد الطلب
////            console.error(`🔧 Request Error [${context}]:`, error.message);
////        }
////    }

////    // 📥 جلب كل التصنيفات
////    async getAllCategories() {
////        try {
////            const response = await api.get('/Category/GetAllCategories');
////            console.log('Get all categories response:', response.data);

////            // التعامل مع ApiResponse من الـ Backend
////            // الـ Backend يرجع: { result, message, code, data }
////            if (response.data?.result) {
////                return response.data.data || [];
////            } else {
////                throw new Error(response.data?.message || 'فشل جلب التصنيفات');
////            }
////        } catch (error) {
////            this.handleError(error, 'getAllCategories');
////            throw error;
////        }
////    }

////    // 🔍 جلب تصنيف حسب ID
////    async getCategoryById(id) {
////        try {
////            const response = await api.get('/Category/GetCategoryByID', {
////                params: { id }
////            });
////            console.log('Get category by id response:', response.data);

////            if (response.data?.result) {
////                return response.data.data;
////            } else {
////                throw new Error(response.data?.message || 'التصنيف غير موجود');
////            }
////        } catch (error) {
////            this.handleError(error, `getCategoryById:${id}`);
////            throw error;
////        }
////    }

////    // ➕ إنشاء تصنيف جديد
////    async createCategory(categoryData) {
////        try {
////            const response = await api.post('/Category/CreateCategory', categoryData);
////            console.log('Create category response:', response.data);

////            if (response.data?.result) {
////                return response.data.data;
////            } else {
////                throw new Error(response.data?.message || 'فشل إنشاء التصنيف');
////            }
////        } catch (error) {
////            this.handleError(error, 'createCategory');
////            throw error;
////        }
////    }

////    // ✏️ تحديث تصنيف
////    async updateCategory(categoryData) {
////        try {
////            const response = await api.put('/Category/UpdateCategory', categoryData);
////            console.log('Update category response:', response.data);

////            if (response.data?.result) {
////                return response.data.data;
////            } else {
////                throw new Error(response.data?.message || 'فشل تحديث التصنيف');
////            }
////        } catch (error) {
////            this.handleError(error, 'updateCategory');
////            throw error;
////        }
////    }

////    // 🗑️ حذف تصنيف
////    async deleteCategory(id) {
////        try {
////            const response = await api.delete('/Category/DeleteCategory', {
////                params: { id }
////            });
////            console.log('Delete category response:', response.data);

////            if (response.data?.result) {
////                return response.data.data;  // ممكن يرجع true/false
////            } else {
////                throw new Error(response.data?.message || 'فشل حذف التصنيف');
////            }
////        } catch (error) {
////            this.handleError(error, `deleteCategory:${id}`);
////            throw error;
////        }
////    }
////}

////// ✅ تصدير كائن واحد (Singleton)
////export default new CategoryService();



//// src/services/CategoryService.js
//import api from '../api/apiConfig';

//class CategoryService {

//    // 🧹 معالجة الأخطاء
//    handleError(error, context = '') {
//        if (error.response) {
//            console.error(`❌ Server Error [${context}]:`, error.response.data);
//            console.error('Status:', error.response.status);

//            if (error.response.status === 401) {
//                console.warn('🔐 Unauthorized - Redirecting to login');
//                localStorage.removeItem('authToken');
//                window.location.href = '/login';
//            }
//        } else if (error.request) {
//            console.error(`🌐 Network Error [${context}]: No response from server`);
//        } else {
//            console.error(`🔧 Request Error [${context}]:`, error.message);
//        }
//    }

//    // 📥 جلب كل التصنيفات - ✅ النسخة المصححة
//    async getAllCategories() {
//        try {
//            const response = await api.get('/Category/GetAllCategories');
//            console.log('Get all categories response:', response.data);

//            // ✅ التحقق من Result (بحرف R كبير) كما في الـ API
//            if (response.data?.Result === true) {
//                return response.data.Data || [];
//            }
//            // ✅ إذا كان Result غير موجود، نتحقق من وجود Data مباشرة
//            else if (response.data?.Data && Array.isArray(response.data.Data)) {
//                return response.data.Data;
//            }
//            // ✅ إذا كانت البيانات مباشرة في response.data
//            else if (Array.isArray(response.data)) {
//                return response.data;
//            }
//            // ✅ إذا كان كل شيء فشل، نرجع مصفوفة فاضية
//            else {
//                console.warn('⚠️ Unexpected API response format:', response.data);
//                return [];
//            }
//        } catch (error) {
//            this.handleError(error, 'getAllCategories');
//            return []; // ✅ نرجع مصفوفة فاضية عشان الموقع ما يعلق
//        }
//    }

//    // 🔍 جلب تصنيف حسب ID
//    async getCategoryById(id) {
//        try {
//            const response = await api.get('/Category/GetCategoryByID', {
//                params: { id }
//            });
//            console.log('Get category by id response:', response.data);

//            if (response.data?.Result === true) {
//                return response.data.Data;
//            } else {
//                return null;
//            }
//        } catch (error) {
//            this.handleError(error, `getCategoryById:${id}`);
//            return null;
//        }
//    }

//    // ➕ إنشاء تصنيف جديد
//    async createCategory(categoryData) {
//        try {
//            const response = await api.post('/Category/CreateCategory', categoryData);
//            console.log('Create category response:', response.data);

//            if (response.data?.Result === true) {
//                return response.data.Data;
//            } else {
//                throw new Error(response.data?.Message || 'فشل إنشاء التصنيف');
//            }
//        } catch (error) {
//            this.handleError(error, 'createCategory');
//            throw error;
//        }
//    }

//    // ✏️ تحديث تصنيف
//    async updateCategory(categoryData) {
//        try {
//            const response = await api.put('/Category/UpdateCategory', categoryData);
//            console.log('Update category response:', response.data);

//            if (response.data?.Result === true) {
//                return response.data.Data;
//            } else {
//                throw new Error(response.data?.Message || 'فشل تحديث التصنيف');
//            }
//        } catch (error) {
//            this.handleError(error, 'updateCategory');
//            throw error;
//        }
//    }

//    // 🗑️ حذف تصنيف
//    async deleteCategory(id) {
//        try {
//            const response = await api.delete('/Category/DeleteCategory', {
//                params: { id }
//            });
//            console.log('Delete category response:', response.data);

//            if (response.data?.Result === true) {
//                return response.data.Data;
//            } else {
//                throw new Error(response.data?.Message || 'فشل حذف التصنيف');
//            }
//        } catch (error) {
//            this.handleError(error, `deleteCategory:${id}`);
//            throw error;
//        }
//    }
//}

//// ✅ تصدير كائن واحد (Singleton)
//export default new CategoryService();


// src/services/CategoryService.js (النسخة المصححة)
import api from '../api/apiConfig';

class CategoryService {

    // 🧹 معالجة الأخطاء
    handleError(error, context = '') {
        if (error.response) {
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

    // 📥 جلب كل التصنيفات
    async getAllCategories() {
        try {
            const response = await api.get('/Category/GetAllCategories');
            console.log('Get all categories response:', response.data);

            if (response.data?.Result === true) {
                return response.data.Data || [];
            }
            else if (response.data?.Data && Array.isArray(response.data.Data)) {
                return response.data.Data;
            }
            else if (Array.isArray(response.data)) {
                return response.data;
            }
            else {
                console.warn('⚠️ Unexpected API response format:', response.data);
                return [];
            }
        } catch (error) {
            this.handleError(error, 'getAllCategories');
            return [];
        }
    }

    // 🔍 جلب تصنيف حسب ID
    async getCategoryById(id) {
        try {
            const response = await api.get('/Category/GetCategoryByID', {
                params: { id }
            });
            console.log('Get category by id response:', response.data);

            if (response.data?.Result === true) {
                return response.data.Data;
            } else {
                return null;
            }
        } catch (error) {
            this.handleError(error, `getCategoryById:${id}`);
            return null;
        }
    }

    // ➕ إنشاء تصنيف جديد - ✅ تم التصحيح
    async createCategory(categoryData) {
        try {
            // ✅ تحويل البيانات إلى الشكل الذي يطلبه الـ API
            const apiData = {
                Name: categoryData.name,        // name → Name
                Description: categoryData.description // description → Description
            };

            console.log('Sending create category data:', apiData);

            const response = await api.post('/Category/CreateCategory', apiData);
            console.log('Create category response:', response.data);

            if (response.data?.Result === true) {
                return response.data.Data;
            } else {
                throw new Error(response.data?.Message || 'فشل إنشاء التصنيف');
            }
        } catch (error) {
            this.handleError(error, 'createCategory');
            throw error;
        }
    }

    // ✏️ تحديث تصنيف - ✅ تم التصحيح
    async updateCategory(categoryData) {
        try {
            // ✅ تحويل البيانات إلى الشكل الذي يطلبه الـ API
            const apiData = {
                Id: categoryData.id,            // id → Id
                Name: categoryData.name,        // name → Name
                Description: categoryData.description // description → Description
            };

            console.log('Sending update category data:', apiData);

            const response = await api.put('/Category/UpdateCategory', apiData);
            console.log('Update category response:', response.data);

            if (response.data?.Result === true) {
                return response.data.Data;
            } else {
                throw new Error(response.data?.Message || 'فشل تحديث التصنيف');
            }
        } catch (error) {
            this.handleError(error, 'updateCategory');
            throw error;
        }
    }

    // 🗑️ حذف تصنيف
    async deleteCategory(id) {
        try {
            const response = await api.delete('/Category/DeleteCategory', {
                params: { id }
            });
            console.log('Delete category response:', response.data);

            if (response.data?.Result === true) {
                return response.data.Data;
            } else {
                throw new Error(response.data?.Message || 'فشل حذف التصنيف');
            }
        } catch (error) {
            this.handleError(error, `deleteCategory:${id}`);
            throw error;
        }
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new CategoryService();