// src/api/MemberService.js
import api from '../api/apiConfig';

class MemberService {

    // 🧹 معالجة الأخطاء
    handleError(error, context = '') {
        if (error.response) {
            // خطأ من السيرفر
            console.error(`❌ Server Error [${context}]:`, error.response.data);
            console.error('Status:', error.response.status);

            // معالجة خاصة لبعض الحالات
            if (error.response.status === 401) {
                localStorage.removeItem('authToken');
                window.location.href = '/login';
            }
        } else if (error.request) {
            // مشكلة في الشبكة
            console.error(`🌐 Network Error [${context}]: No response from server`);
        } else {
            // خطأ في إعداد الطلب
            console.error(`🔧 Request Error [${context}]:`, error.message);
        }
    }

    // 📥 جلب كل الأعضاء
    async getAllMembers() {
        try {
            const response = await api.get('/Member/GetAllMembers');
            console.log('Get all Members response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الأعضاء');
            }
        } catch (error) {
            this.handleError(error, 'getAllMembers');
            throw error;
        }
    }

    // 🔍 جلب عضو حسب ID
    async getMemberById(id) {
        try {
            const response = await api.get('/Member/GetMembetById', {  // ✅ لاحظ التصحيح الإملائي
                params: { id }
            });
            console.log('Get Member by id response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'العضو غير موجود');
            }
        } catch (error) {
            this.handleError(error, `getMemberById:${id}`);
            throw error;
        }
    }

    // 🔍 البحث عن عضو برقم العضوية
    async getMemberByCode(memberCode) {
        try {
            const response = await api.get('/Member/GetMemberByCode', {
                params: { memberCode }
            });
            console.log('Get Member by code response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'العضو غير موجود');
            }
        } catch (error) {
            this.handleError(error, `getMemberByCode:${memberCode}`);
            throw error;
        }
    }

    // 📋 جلب أعضاء حسب نوع العضوية
    async getMembersByType(membershipType) {
        try {
            const response = await api.get('/Member/GetMembersByType', {
                params: { membershipType }
            });
            console.log('Get Members by type response:', response.data);

            if (response.data?.result) {
                return response.data.data || [];
            } else {
                throw new Error(response.data?.message || 'فشل جلب الأعضاء');
            }
        } catch (error) {
            this.handleError(error, `getMembersByType:${membershipType}`);
            throw error;
        }
    }

    // ➕ إنشاء عضو جديد
    async createMember(memberData) {  // ✅ اسم الدالة مصحح
        try {
            const response = await api.post('/Member/CreateMember', memberData);
            console.log('Create Member response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل إنشاء العضو');
            }
        } catch (error) {
            this.handleError(error, 'createMember');
            throw error;
        }
    }

    // ✏️ تحديث عضو
    async updateMember(memberData) {
        try {
            const response = await api.put('/Member/UpdateMember', memberData);
            console.log('Update member response:', response.data);

            if (response.data?.result) {
                return response.data.data;
            } else {
                throw new Error(response.data?.message || 'فشل تحديث العضو');
            }
        } catch (error) {
            this.handleError(error, 'updateMember');
            throw error;
        }
    }

    // 🗑️ حذف عضو
    async deleteMember(id) {
        try {
            const response = await api.delete('/Member/DeleteMember', {
                params: { id }
            });
            console.log('Delete member response:', response.data);

            if (response.data?.result) {
                return response.data.data; // true/false
            } else {
                throw new Error(response.data?.message || 'فشل حذف العضو');
            }
        } catch (error) {
            this.handleError(error, `deleteMember:${id}`);
            throw error;
        }
    }
}

// ✅ تصدير كائن واحد (Singleton)
export default new MemberService();