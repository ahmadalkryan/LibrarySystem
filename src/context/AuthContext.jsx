/* eslint-disable no-unused-vars */


///* eslint-disable react-refresh/only-export-components */
//// src/context/AuthContext.js
//import { createContext, useState, useContext, useEffect } from 'react';
//import { logout as apiLogout } from '../api/authService';

//const AuthContext = createContext(null);

//export const useAuth = () => useContext(AuthContext);

//export const AuthProvider = ({ children }) => {
//    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('authToken'));
//    const [user, setUser] = useState({
//        id: localStorage.getItem('userId'),
//        name: localStorage.getItem('userName'),
//        role: localStorage.getItem('role'),
//    });
//    const [isLoading, setIsLoading] = useState(true);

//    useEffect(() => {
//        // This effect syncs state with localStorage on initial load and tab changes.
//        const syncAuthState = () => {
//            const token = localStorage.getItem('authToken');
//            setIsAuthenticated(!!token);
//            setUser({
//                id: localStorage.getItem('userId'),
//                name: localStorage.getItem('userName'),
//                role: localStorage.getItem('role'),
//            });
//            setIsLoading(false);
//        };
//        syncAuthState();
//        window.addEventListener('storage', syncAuthState);
//        return () => window.removeEventListener('storage', syncAuthState);
//    }, []);

//    const handleLogin = () => {
//        setIsAuthenticated(true);
//        setUser({
//            id: localStorage.getItem('userId'),
//            name: localStorage.getItem('userName'),
//            role: localStorage.getItem('role'),
//        });
//    };

//    const logout = async () => {
//        try {
//            await apiLogout();
//        } catch (error) {
//            console.error("Logout API call failed, but proceeding.", error);
//        } finally {
//            localStorage.clear();
//            setIsAuthenticated(false);
//            setUser({ id: null, name: null, role: null });
//            window.location.href = '/login';
//        }
//    };

//    const value = {
//        isAuthenticated,
//        user,
//        isLoading,
//        logout,
//        handleLogin,
//        // Helper booleans for easy role checking
//        isAdmin: user.role === 'Admin',
//        isLibrarian: user.role === 'Librarian',
//    };

//    if (isLoading) {
//        return <div>Loading session...</div>;
//    }

//    return (
//        <AuthContext.Provider value={value}>
//            {children}
//        </AuthContext.Provider>
//    );
//};



/* eslint-disable react-refresh/only-export-components */
// src/context/AuthContext.js
import { createContext, useState, useContext, useEffect } from 'react';
import { logout as apiLogout, getCurrentUser } from '../api/authService';

const AuthContext = createContext(null);

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('authToken'));
    const [user, setUser] = useState({
        id: localStorage.getItem('userId'),
        name: localStorage.getItem('userName'),
        role: localStorage.getItem('role'),
    });
    const [isLoading, setIsLoading] = useState(true);

    // ✅ تحديث حالة المستخدم من localStorage
    const updateUserFromStorage = () => {
        const token = localStorage.getItem('authToken');
        setIsAuthenticated(!!token);
        setUser({
            id: localStorage.getItem('userId'),
            name: localStorage.getItem('userName'),
            role: localStorage.getItem('role'),
        });
    };

    useEffect(() => {
        // التحقق من صلاحية التوكن عند التحميل
        const token = localStorage.getItem('authToken');
        const expires = localStorage.getItem('expires');

        if (token && expires) {
            const expiryDate = new Date(expires);
            if (expiryDate < new Date()) {
                // التوكن منتهي الصلاحية
                localStorage.clear();
                setIsAuthenticated(false);
                setUser({ id: null, name: null, role: null });
            }
        }

        updateUserFromStorage();
        setIsLoading(false);

        // الاستماع للتغييرات في localStorage (للتحديث بين التبويبات)
        window.addEventListener('storage', updateUserFromStorage);
        return () => window.removeEventListener('storage', updateUserFromStorage);
    }, []);

    const handleLogin = () => {
        updateUserFromStorage();
    };

    const logout = async () => {
        try {
            await apiLogout();
        } catch (error) {
            console.error("Logout API call failed, but proceeding.", error);
        } finally {
            localStorage.clear();
            setIsAuthenticated(false);
            setUser({ id: null, name: null, role: null });
            window.location.href = '/login';
        }
    };

    // ✅ التحقق من الصلاحيات حسب الدور
    const hasPermission = (requiredRoles) => {
        if (!user.role) return false;
        if (user.role === 'Admin') return true; // Admin لديه كل الصلاحيات
        return requiredRoles.includes(user.role);
    };

    const value = {
        isAuthenticated,
        user,
        isLoading,
        logout,
        handleLogin,
        hasPermission,
        // Helper booleans for easy role checking
        isAdmin: user.role === 'Admin', 
        isLibrarian: user.role === 'Librarian',
        isStaff: user.role === 'Staff',
    };

    if (isLoading) {
        return (
            <div className="min-h-screen flex items-center justify-center">
                <div className="text-center">
                    <div className="w-16 h-16 border-4 border-indigo-600 border-t-transparent rounded-full animate-spin mx-auto mb-4"></div>
                    <p className="text-gray-600">جاري تحميل الجلسة...</p>
                </div>
            </div>
        );
    }

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};