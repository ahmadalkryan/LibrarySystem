

//// src/pages/LoginPage.jsx
//import React, { useState } from 'react';
//import { useNavigate, Link } from 'react-router-dom';
//import { login } from '../api/authService';
//import { useAuth } from '../context/AuthContext';
//import Libimage from '../assets/images.jfif';

//const LoginPage = () => {
//    const [email, setEmail] = useState('');
//    const [password, setPassword] = useState('');
//    const [error, setError] = useState('');
//    const [loading, setLoading] = useState(false);
//    const [rememberMe, setRememberMe] = useState(false);

//    const navigate = useNavigate();
//    const { handleLogin } = useAuth();

//    const handleSubmit = async (e) => {
//        e.preventDefault();
//        setLoading(true);
//        setError('');
//        try {
//            // قم بتخزين التوكن والمستخدم والرول هنا قبل التوجيه
//            // (ال AuthContext يفرض عليك تخزينهم في localStorage في دالة handleLogin)
//            await login(email, password);
//            handleLogin();
//            navigate('/');
//        } catch (err) {
//            // استخدام رسالة الخطأ الواردة من API
//            setError(err.message || 'Login failed. Please check your credentials.');
//        } finally {
//            setLoading(false);
//        }
//    };

//    // سنعتمد Indigo 600 كـ Primary و Pink 600 كـ Secondary
//    const primary = 'indigo'; // Indigo-600
//    const secondary = 'pink'; // Pink-600

//    return (
//        <div className="min-h-screen bg-gray-50 flex items-center justify-center p-4 sm:p-6 lg:p-8">
//            <div className="w-full max-w-6xl mx-auto">
//                <div className="bg-white shadow-2xl rounded-3xl overflow-hidden flex flex-col md:flex-row border border-gray-100">

//                    {/* Left Side: Form */}
//                    <div className="w-full md:w-1/2 p-8 lg:p-12 flex flex-col justify-center">
//                        <div className="text-center mb-8">
//                            {/* الأيقونة المُحسَّنة */}
//                            <div className={`w-16 h-16 bg-gradient-to-r from-${primary}-600 to-${secondary}-600 rounded-full flex items-center justify-center mx-auto mb-4
//                                            shadow-xl shadow-${primary}-500/30 transform hover:scale-105 transition-transform duration-300`}>
//                                <svg className="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
//                                </svg>
//                            </div>
//                            <h2 className={`text-4xl font-extrabold bg-gradient-to-r from-${primary}-600 to-${secondary}-600 bg-clip-text text-transparent mb-3 tracking-tight`}>
//                                Welcome Back
//                            </h2>
//                            <p className="text-gray-600 text-lg">Securely sign in to your dashboard</p>
//                        </div>

//                        <form onSubmit={handleSubmit} className="space-y-6">
//                            {/* رسالة الخطأ المُحسَّنة */}
//                            {error && (
//                                <div className="bg-red-50 border-l-4 border-red-500 p-4 rounded-xl shadow-md" role="alert">
//                                    <div className="flex items-center">
//                                        <svg className="w-5 h-5 text-red-500 mr-2" fill="currentColor" viewBox="0 0 20 20">
//                                            <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd" />
//                                        </svg>
//                                        <span className="text-red-700 font-medium text-sm">{error}</span>
//                                    </div>
//                                </div>
//                            )}

//                            {/* Email Input */}
//                            <div className="space-y-2">
//                                <label htmlFor="email" className="block text-sm font-semibold text-gray-700">Email Address</label>
//                                <div className="relative">
//                                    <input
//                                        type="email"
//                                        id="email"
//                                        value={email}
//                                        onChange={(e) => setEmail(e.target.value)}
//                                        required
//                                        className={`w-full px-4 py-3 bg-white border border-gray-300 rounded-xl text-sm placeholder-gray-400
//                                                 focus:outline-none focus:border-transparent focus:ring-4 focus:ring-${primary}-500/50 transition-all duration-300
//                                                 hover:border-gray-400 shadow-sm`}
//                                        placeholder="Enter your email"
//                                    />
//                                    <svg className="w-5 h-5 text-gray-400 absolute right-3 top-3.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 12a4 4 0 10-8 0 4 4 0 008 0zm0 0v1.5a2.5 2.5 0 005 0V12a9 9 0 10-9 9m4.5-1.206a8.959 8.959 0 01-4.5 1.207" />
//                                    </svg>
//                                </div>
//                            </div>

//                            {/* Password Input */}
//                            <div className="space-y-2">
//                                <label htmlFor="password" className="block text-sm font-semibold text-gray-700">Password</label>
//                                <div className="relative">
//                                    <input
//                                        type="password"
//                                        id="password"
//                                        value={password}
//                                        onChange={(e) => setPassword(e.target.value)}
//                                        required
//                                        className={`w-full px-4 py-3 bg-white border border-gray-300 rounded-xl text-sm placeholder-gray-400
//                                                 focus:outline-none focus:border-transparent focus:ring-4 focus:ring-${primary}-500/50 transition-all duration-300
//                                                 hover:border-gray-400 shadow-sm`}
//                                        placeholder="Enter your password"
//                                    />
//                                    <svg className="w-5 h-5 text-gray-400 absolute right-3 top-3.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
//                                    </svg>
//                                </div>
//                            </div>

//                            {/* Remember Me & Forgot Password */}
//                            <div className="flex items-center justify-between">
//                                <label className="flex items-center space-x-2 cursor-pointer">
//                                    <input
//                                        type="checkbox"
//                                        checked={rememberMe}
//                                        onChange={(e) => setRememberMe(e.target.checked)}
//                                        className={`w-4 h-4 text-${primary}-600 border-gray-300 rounded focus:ring-${primary}-500/50`}
//                                    />
//                                    <span className="text-sm text-gray-600">Remember me</span>
//                                </label>
//                                <a href="#" className={`text-sm text-${primary}-600 hover:text-${secondary}-600 font-medium transition-colors duration-200`}>
//                                    Forgot password?
//                                </a>
//                            </div>

//                            {/* زر تسجيل الدخول المُحسَّن */}
//                            <button
//                                type="submit"
//                                disabled={loading}
//                                className={`w-full py-4 px-6 bg-gradient-to-r from-${primary}-600 to-${secondary}-600 text-white font-semibold rounded-xl
//                                         shadow-lg shadow-${primary}-500/40 hover:shadow-2xl hover:shadow-${primary}-500/50 transform hover:-translate-y-1 transition-all duration-300
//                                         focus:outline-none focus:ring-4 focus:ring-${primary}-500/40 focus:ring-offset-2
//                                         disabled:opacity-50 disabled:cursor-not-allowed disabled:transform-none disabled:shadow-lg`}
//                            >
//                                {loading ? (
//                                    <div className="flex items-center justify-center space-x-2">
//                                        <div className="w-5 h-5 border-2 border-white/30 border-t-white rounded-full animate-spin"></div>
//                                        <span>Authenticating...</span>
//                                    </div>
//                                ) : (
//                                    'Sign In Securely'
//                                )}
//                            </button>

//                            {/* Separator */}
//                            <div className="relative my-6">
//                                <div className="absolute inset-0 flex items-center">
//                                    <div className="w-full border-t border-gray-200"></div>
//                                </div>
//                                <div className="relative flex justify-center text-sm">
//                                    <span className="px-2 bg-white text-gray-500">New to our platform?</span>
//                                </div>
//                            </div>

//                            {/* زر إنشاء حساب مُحسَّن */}
//                            <Link
//                                to="/register"
//                                className={`w-full py-3 px-6 border-2 border-gray-300 text-gray-700 font-semibold rounded-xl
//                                         hover:border-${primary}-500 hover:text-${primary}-600 transition-all duration-300
//                                         flex items-center justify-center space-x-2 shadow-sm hover:shadow-md`}
//                            >
//                                <span>Create an Account</span>
//                                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M14 5l7 7m0 0l-7 7m7-7H3" />
//                                </svg>
//                            </Link>
//                        </form>
//                    </div>

//                    {/* Right Side: Image & Content */}
//                    <div className={`hidden md:block md:w-1/2 relative bg-gradient-to-br from-${primary}-600 to-${secondary}-600`}>
//                        <div className="absolute inset-0 bg-black/30"></div>
//                        <img
//                            src={Libimage}
//                            alt="Team collaboration and data security"
//                            className="w-full h-full object-cover"
//                        />
//                        <div className="absolute inset-0 flex items-center justify-center p-12">
//                            <div className="text-center text-white">
//                                <h3 className="text-4xl font-extrabold mb-4 drop-shadow-lg">Unlock Your Potential</h3>
//                                <p className="text-xl opacity-95 drop-shadow-md">Access your secure, personalized workspace and start creating.</p>
//                            </div>
//                        </div>
//                    </div>
//                </div>
//            </div>
//        </div>
//    );
//};

//export default LoginPage;




// src/pages/Auth/LoginPage.jsx
import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { login } from '../../api/authService';
import { useAuth } from '../../context/AuthContext';
import Libimage from '../../assets/images.jfif';

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const [rememberMe, setRememberMe] = useState(false);

    const navigate = useNavigate();
    const { handleLogin } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError('');
        try {
            await login(email, password);
            handleLogin();
            navigate('/');
        } catch (err) {
            setError(err.message || 'Login failed. Please check your credentials.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-slate-50 to-slate-100 flex items-center justify-center p-4">
            <div className="w-full max-w-6xl mx-auto">
                <div className="bg-white/80 backdrop-blur-xl shadow-2xl rounded-3xl overflow-hidden flex flex-col md:flex-row border border-white/20">

                    {/* Left Side: Form */}
                    <div className="w-full md:w-1/2 p-8 lg:p-12">
                        {/* Logo and Header */}
                        <div className="mb-10">
                            <div className="flex items-center gap-2 mb-6">
                                <div className="w-8 h-8 bg-gradient-to-r from-blue-600 to-indigo-600 rounded-lg flex items-center justify-center">
                                    <svg className="w-5 h-5 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
                                    </svg>
                                </div>
                                <span className="text-xl font-semibold text-slate-800">Sy Library</span>
                            </div>

                            <h1 className="text-3xl font-bold text-slate-800 mb-2">Sign in</h1>
                            <p className="text-slate-600">Welcome back! Please enter your details.</p>
                        </div>

                        <form onSubmit={handleSubmit} className="space-y-6">
                            {/* Error Message */}
                            {error && (
                                <div className="bg-red-50 border border-red-200 text-red-600 px-4 py-3 rounded-xl text-sm">
                                    {error}
                                </div>
                            )}

                            {/* Email Field */}
                            <div className="space-y-2">
                                <label className="block text-sm font-medium text-slate-700">Email</label>
                                <input
                                    type="email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                    className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm placeholder-slate-400
                                             focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10
                                             transition-all duration-200"
                                    placeholder="your@email.com"
                                />
                            </div>

                            {/* Password Field */}
                            <div className="space-y-2">
                                <label className="block text-sm font-medium text-slate-700">Password</label>
                                <input
                                    type="password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                    className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm placeholder-slate-400
                                             focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10
                                             transition-all duration-200"
                                    placeholder="●●●●●"
                                />
                            </div>

                            {/* Remember Me & Forgot Password */}
                            <div className="flex items-center justify-between">
                                <label className="flex items-center gap-2 cursor-pointer">
                                    <input
                                        type="checkbox"
                                        checked={rememberMe}
                                        onChange={(e) => setRememberMe(e.target.checked)}
                                        className="w-4 h-4 text-blue-600 border-slate-300 rounded focus:ring-blue-500/20"
                                    />
                                    <span className="text-sm text-slate-600">Remember me</span>
                                </label>
                                <a href="#" className="text-sm text-blue-600 hover:text-blue-700 font-medium">
                                    Forgot your password?
                                </a>
                            </div>

                            {/* Sign In Button */}
                            <button
                                type="submit"
                                disabled={loading}
                                className="w-full py-3 px-4 bg-gradient-to-r from-blue-600 to-indigo-600 text-white font-semibold rounded-xl
                                         hover:from-blue-700 hover:to-indigo-700 transform transition-all duration-200
                                         focus:outline-none focus:ring-4 focus:ring-blue-500/30
                                         disabled:opacity-50 disabled:cursor-not-allowed"
                            >
                                {loading ? (
                                    <div className="flex items-center justify-center gap-2">
                                        <div className="w-5 h-5 border-2 border-white/30 border-t-white rounded-full animate-spin"></div>
                                        <span>Signing in...</span>
                                    </div>
                                ) : (
                                    'Sign in'
                                )}
                            </button>

                            {/* Divider */}
                            <div className="relative my-8">
                                <div className="absolute inset-0 flex items-center">
                                    <div className="w-full border-t border-slate-200"></div>
                                </div>
                                <div className="relative flex justify-center text-sm">
                                    <span className="px-4 bg-white text-slate-500">or</span>
                                </div>
                            </div>

                            {/* Social Sign In Options */}
                            <div className="space-y-3">
                                <button
                                    type="button"
                                    className="w-full py-3 px-4 bg-white border border-slate-200 rounded-xl text-slate-700 font-medium
                                             hover:bg-slate-50 transition-all duration-200 flex items-center justify-center gap-3"
                                >
                                    <svg className="w-5 h-5" viewBox="0 0 24 24">
                                        <path fill="#4285F4" d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z" />
                                        <path fill="#34A853" d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z" />
                                        <path fill="#FBBC05" d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z" />
                                        <path fill="#EA4335" d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z" />
                                    </svg>
                                    <span>Sign in with Google</span>
                                </button>

                                <button
                                    type="button"
                                    className="w-full py-3 px-4 bg-[#1877F2] text-white font-medium rounded-xl
                                             hover:bg-[#1869D9] transition-all duration-200 flex items-center justify-center gap-3"
                                >
                                    <svg className="w-5 h-5" fill="currentColor" viewBox="0 0 24 24">
                                        <path d="M24 12.073c0-6.627-5.373-12-12-12s-12 5.373-12 12c0 5.99 4.388 10.954 10.125 11.854v-8.385H7.078v-3.47h3.047V9.43c0-3.007 1.792-4.669 4.533-4.669 1.312 0 2.686.235 2.686.235v2.953H15.83c-1.491 0-1.956.925-1.956 1.874v2.25h3.328l-.532 3.47h-2.796v8.385C19.612 23.027 24 18.062 24 12.073z" />
                                    </svg>
                                    <span>Sign in with Facebook</span>
                                </button>
                            </div>

                            {/* Sign Up Link */}
                            <p className="text-center text-sm text-slate-600 mt-8">
                                Don't have an account?{' '}
                                <Link to="/register" className="text-blue-600 hover:text-blue-700 font-semibold">
                                    Sign up
                                </Link>
                            </p>
                        </form>
                    </div>

                    {/* Right Side: Image */}
                    <div className="hidden md:block md:w-1/2 relative">
                        <div className="absolute inset-0 bg-gradient-to-br from-blue-600/90 to-indigo-600/90 mix-blend-multiply"></div>
                        <img
                            src={Libimage}
                            alt="Library"
                            className="w-full h-full object-cover"
                        />
                        <div className="absolute inset-0 flex items-center justify-center p-12">
                            <div className="text-center text-white">
                                <h3 className="text-4xl font-bold mb-4">Welcome Back!</h3>
                                <p className="text-lg text-white/90">Access your account and continue your journey with us.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;