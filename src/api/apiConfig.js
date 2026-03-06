
//import axios from 'axios';

//const API_BASE_URL = 'https://localhost:7060/api';

//const api = axios.create({
//    baseUrl =API_BASE_URL,
//    headers: {
//        'Accept': "application/json",
//        'Content-Type':'application/json',
//    },

//});

//api.interceptors.request.use((config) => {

//    const token = localStorage.getItem('authToken');

//    if (token) {
//        config.headers.Authorization = `Bearer ${token}`;
//    }
//    return config;
//});

//api.interceptors.request.use(
//    response => response,
//    error => {
//        if ( error.resopnse.status === 401){
//      localStorage.removeItem("authToken");
//       localStorage.removeItem("role");
//       localStorage.removeItem("userName");
//       localStorage.removeItem("userId");
//        localStorage.removeItem("expires");



//    window.location.href = '/login';

//}
//return Promise.reject(error);
//    }

//);

//export default api;

// src/api/apiConfig.js
import axios from 'axios';

const API_BASE_URL = 'https://localhost:7060/api';

const api = axios.create({
    baseURL: API_BASE_URL,  // ✅ تصحيح: baseURL وليس baseUrl
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    },
});

// Interceptor للطلب (إضافة التوكن)
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('authToken');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// ✅ تصحيح: response interceptor (كان مكتوب request بالخطأ)
api.interceptors.response.use(
    (response) => response,  // نجاح
    (error) => {             // خطأ
        if (error.response && error.response.status === 401) {
            // غير مصرح - طرد المستخدم
            localStorage.removeItem('authToken');
            localStorage.removeItem('role');
            localStorage.removeItem('userName');
            localStorage.removeItem('userId');
            localStorage.removeItem('expires');

            // توجيه لصفحة تسجيل الدخول
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

export default api;