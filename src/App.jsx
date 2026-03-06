

import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import CategoriesPage from './pages/CategoriesPage';


// الصفحات المطلوبة فقط
import LoginPage from './pages/Auth/LoginPage';
import RegisterPage from './pages/Auth/RegisterPage';

function App() {
    return (
        <AuthProvider>
            <Router>
                <Routes>
                    {/* ✅ الصفحات العامة (بدون حماية) */}
                    <Route path="/categories" element={<CategoriesPage />} />
                   
                    <Route path="/" element={<Navigate to="/categories" />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route path="/login" element={<LoginPage />} />

                   

                    {/* ✅ أي مسار غير معروف يروح للـ Login */}
                    <Route path="*" element={<Navigate to="/login" replace />} />
                </Routes>
            </Router>
        </AuthProvider>
    );
}

export default App;