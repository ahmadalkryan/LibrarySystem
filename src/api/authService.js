

//import api from './apiConfig';

//export const register = async (username, email, password) => {

//    try {
//        const response = await api.post('/Authentication/Register', {


//            Name: username
//            Email:email,
//            Password:password
//        });
//        return response.data;

//    }
//    catch (error) {

//        console.log('Register Api error', error.response?.data);

//        throw error.response?.data || error;
//    }

//};


//export const login = async (email, password) => {
//    try {
//        const response = await api.post('/Authentication/Login', {
//            Email: email,
//            Password: password
//        });
//        console.log('Login response:', response.data);
//        console.log(response.data.data.id);
//        console.log(response.data.data.token);
//        console.log(response.data.data.name);
//        console.log(response.data.data.role);
//        if (response.data) {
//            // تخزين البيانات في localStorage بعد تسجيل الدخول الناجح
//            localStorage.setItem('authToken', response.data.data.token);
//            localStorage.setItem('userId', response.data.data.id);
//            localStorage.setItem('role', response.data.data.Role);
//            localStorage.setItem('userName', response.data.data.name);
//            localStorage.setItem('expires', response.data.data.expires);
//        }
//        return response.data;
//    } catch (error) {
//        console.error('Login API error:', error.response?.data || error.message);
//        throw error.response?.data || error;
//    }


//};



//export const logout = async () => {
//    try {

//        localStorage.removeItem('authToken');
//        localStorage.removeItem('role');
//        localStorage.removeItem('userId');
//        localStorage.removeItem('userName');
//        localStorage.removeItem('expires');


//    } catch (error) {
//        console.error('Logout API error:', error.message);

//    }
//};




// src/api/authService.js
import api from './apiConfig';

export const register = async (username, email, password) => {
    try {
        const response = await api.post('/Authentication/Register', {
            Name: username,
            Email: email,
            Password: password
        });
        console.log('Register response:', response.data);
        return response.data;
    } catch (error) {
        console.log('Register API error', error.response?.data);
        throw error.response?.data || error;
    }
};

export const login = async (email, password) => {
    try {
        const response = await api.post('/Authentication/Login', {
            Email: email,
            Password: password
        });
        console.log('Login response:', response.data);

        // ✅ التأكد من وجود البيانات قبل التخزين
        // ملاحظة: API يرجع { Code, Message, Result, Data }
        if (response.data?.Result && response.data?.Data) {
            const userData = response.data.Data;

            // ✅ تخزين البيانات في localStorage (بنفس أسماء الحقول من API)
            localStorage.setItem('authToken', userData.Token);      // Token وليس token
            localStorage.setItem('userId', userData.Id);            // Id وليس id
            localStorage.setItem('userName', userData.UserName);    // UserName وليس userName
            localStorage.setItem('role', userData.Role);            // Role وليس role
            localStorage.setItem('expires', userData.Expires);      // Expires وليس expires

            console.log('User logged in:', {
                id: userData.Id,
                name: userData.UserName,
                role: userData.Role,
                token: userData.Token,
                expires: userData.Expires
            });
        } else {
            console.warn('Login response missing data:', response.data);
        }

        return response.data;
    } catch (error) {
        console.error('Login API error:', error.response?.data || error.message);
        throw error.response?.data || error;
    }
};

export const logout = async () => {
    try {
        // ✅ يمكن إضافة استدعاء API لتسجيل الخروج إذا كان موجوداً
        // await api.post('/Authentication/Logout');

        localStorage.removeItem('authToken');
        localStorage.removeItem('userId');
        localStorage.removeItem('userName');
        localStorage.removeItem('role');
        localStorage.removeItem('expires');

        console.log('User logged out successfully');
    } catch (error) {
        console.error('Logout error:', error.message);
    }
};

// ✅ دالة إضافية مفيدة للتحقق من التوكن
export const getCurrentUser = () => {
    return {
        id: localStorage.getItem('userId'),
        name: localStorage.getItem('userName'),
        role: localStorage.getItem('role'),
        token: localStorage.getItem('authToken'),
        expires: localStorage.getItem('expires')
    };
};

// ✅ دالة للتحقق من صلاحية التوكن
export const isAuthenticated = () => {
    const token = localStorage.getItem('authToken');
    const expires = localStorage.getItem('expires');

    if (!token || !expires) return false;

    // التحقق من انتهاء الصلاحية
    const expiryDate = new Date(expires);
    const now = new Date();

    return expiryDate > now;
};