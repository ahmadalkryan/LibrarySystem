

//// src/components/ProtectedRoute.js
//import React from 'react';
//import { Navigate, Outlet } from 'react-router-dom';
//import { useAuth } from '../context/AuthContext';

//const ProtectedRoute = ({ allowedRoles }) => {
//    const { isAuthenticated, user, isLoading } = useAuth();

//    if (isLoading) {
//        return <div>Loading...</div>; // أو أي شاشة تحميل
//    }

//    if (!isAuthenticated) {
//        return <Navigate to="/login" replace />;
//    }

//    if (allowedRoles && !allowedRoles.includes(user.role)) {
//        // يمكن توجيهه لصفحة "غير مصرح به"
//        return <Navigate to="/" replace />;
//    }

//    return <Outlet />;
//};

//export default ProtectedRoute;







// src/components/ProtectedRoute.js
import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const ProtectedRoute = ({ allowedRoles, children }) => {
    const { isAuthenticated, user, isLoading } = useAuth();

    if (isLoading) {
        return (
            <div className="min-h-screen flex items-center justify-center">
                <div className="text-center">
                    <div className="w-16 h-16 border-4 border-indigo-600 border-t-transparent rounded-full animate-spin mx-auto mb-4"></div>
                    <p className="text-gray-600">جاري التحميل...</p>
                </div>
            </div>
        );
    }

    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    // التحقق من الصلاحيات
    if (allowedRoles && !allowedRoles.includes(user?.role)) {
        return <Navigate to="/" replace />;
    }

    // إذا كان children موجود، نرجعه، وإلا نرجع Outlet
    return children || <Outlet />;
};

export default ProtectedRoute;