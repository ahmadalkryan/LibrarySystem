



//// src/pages/RegisterPage.jsx
//import React, { useState } from 'react';
//import { useNavigate, Link } from 'react-router-dom';
//import { register } from '../api/authService';
//import communityImage from '../assets/istockphoto-1498878143-612x612.jpg';

//const RegisterPage = () => {
//    const [formData, setFormData] = useState({
//        username: '',
//        email: '',
//        password: '',
//        confirmPassword: '',
//        agreeTerms: false
//    });
//    const [error, setError] = useState('');
//    const [loading, setLoading] = useState(false);

//    const navigate = useNavigate();

//    const handleChange = (e) => {
//        const { name, value, type, checked } = e.target;
//        setFormData(prev => ({
//            ...prev,
//            [name]: type === 'checkbox' ? checked : value
//        }));
//    };

//    const handleSubmit = async (e) => {
//        e.preventDefault();
//        if (formData.password !== formData.confirmPassword) {
//            setError('Passwords do not match');
//            return;
//        }
//        if (!formData.agreeTerms) {
//            setError('Please agree to the terms and conditions');
//            return;
//        }

//        setLoading(true);
//        setError('');
//        try {
//            await register(formData.username, formData.email, formData.password, formData.confirmPassword);
//            alert('Account created successfully! Please log in.');
//            navigate('/login');
//        } catch (err) {
//            setError(err.message || 'Registration failed. The email may already be in use.');
//        } finally {
//            setLoading(false);
//        }
//    };

//    // ”‰⁄ „œ Indigo 600 ﬂ‹ Primary Ê Pink 600 ﬂ‹ Secondary
//    const primary = 'indigo'; // Indigo-600
//    const secondary = 'pink'; // Pink-600

//    return (
//        <div className="min-h-screen bg-gray-50 flex items-center justify-center p-4 sm:p-6 lg:p-8">
//            <div className="w-full max-w-6xl mx-auto">
//                <div className="bg-white shadow-2xl rounded-3xl overflow-hidden flex flex-col md:flex-row-reverse border border-gray-100">

//                    {/* Left Side (Image & Content) - Reversed order for Registration layout */}
//                    <div className={`hidden md:block md:w-1/2 relative bg-gradient-to-br from-${primary}-600 to-${secondary}-600`}>
//                        <div className="absolute inset-0 bg-black/30"></div>
//                        <img
//                            src={communityImage}
//                            alt="Community collaboration"
//                            className="w-full h-full object-cover"
//                        />
//                        <div className="absolute inset-0 flex items-center justify-center p-12">
//                            <div className="text-center text-white">
//                                <h3 className="text-4xl font-extrabold mb-4 drop-shadow-lg">Start Your Journey</h3>
//                                <p className="text-xl opacity-95 drop-shadow-md max-w-md">
//                                    Join thousands of users who share their stories and connect with others.
//                                </p>
//                            </div>
//                        </div>
//                    </div>

//                    {/* Right Side: Form */}
//                    <div className="w-full md:w-1/2 p-8 lg:p-12 flex flex-col justify-center">
//                        <div className="text-center mb-8">
//                            {/* «·√ÌﬁÊ‰… */}
//                            <div className={`w-16 h-16 bg-gradient-to-r from-${primary}-600 to-${secondary}-600 rounded-full flex items-center justify-center mx-auto mb-4
//                                            shadow-xl shadow-${primary}-500/30 transform hover:scale-105 transition-transform duration-300`}>
//                                <svg className="w-8 h-8 text-white" fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
//                                </svg>
//                            </div>
//                            <h2 className={`text-4xl font-extrabold bg-gradient-to-r from-${primary}-600 to-${secondary}-600 bg-clip-text text-transparent mb-3 tracking-tight`}>
//                                Create Account
//                            </h2>
//                            <p className="text-gray-600 text-lg">Join our community today</p>
//                        </div>

//                        <form onSubmit={handleSubmit} className="space-y-5">
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

//                            {/* Inputs Grid */}
//                            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
//                                {/* Username */}
//                                <div>
//                                    <label htmlFor="username" className="block text-sm font-semibold text-gray-700">Username</label>
//                                    <input
//                                        type="text"
//                                        id="username"
//                                        name="username"
//                                        value={formData.username}
//                                        onChange={handleChange}
//                                        required
//                                        className={`w-full px-4 py-3 bg-white border border-gray-300 rounded-xl text-sm placeholder-gray-400
//                                                 focus:outline-none focus:border-transparent focus:ring-4 focus:ring-${primary}-500/50 transition-all duration-300`}
//                                        placeholder="Choose a username"
//                                    />
//                                </div>

//                                {/* Email */}
//                                <div>
//                                    <label htmlFor="email" className="block text-sm font-semibold text-gray-700">Email Address</label>
//                                    <input
//                                        type="email"
//                                        id="email"
//                                        name="email"
//                                        value={formData.email}
//                                        onChange={handleChange}
//                                        required
//                                        className={`w-full px-4 py-3 bg-white border border-gray-300 rounded-xl text-sm placeholder-gray-400
//                                                 focus:outline-none focus:border-transparent focus:ring-4 focus:ring-${primary}-500/50 transition-all duration-300`}
//                                        placeholder="Enter your email"
//                                    />
//                                </div>
//                            </div>

//                            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
//                                {/* Password */}
//                                <div>
//                                    <label htmlFor="password" className="block text-sm font-semibold text-gray-700">Password</label>
//                                    <input
//                                        type="password"
//                                        id="password"
//                                        name="password"
//                                        value={formData.password}
//                                        onChange={handleChange}
//                                        required
//                                        className={`w-full px-4 py-3 bg-white border border-gray-300 rounded-xl text-sm placeholder-gray-400
//                                                 focus:outline-none focus:border-transparent focus:ring-4 focus:ring-${primary}-500/50 transition-all duration-300`}
//                                        placeholder="Create password"
//                                    />
//                                </div>
//                                {/* Confirm Password */}
//                                <div>
//                                    <label htmlFor="confirmPassword" className="block text-sm font-semibold text-gray-700">Confirm Password</label>
//                                    <input
//                                        type="password"
//                                        id="confirmPassword"
//                                        name="confirmPassword"
//                                        value={formData.confirmPassword}
//                                        onChange={handleChange}
//                                        required
//                                        className={`w-full px-4 py-3 bg-white border border-gray-300 rounded-xl text-sm placeholder-gray-400
//                                                 focus:outline-none focus:border-transparent focus:ring-4 focus:ring-${primary}-500/50 transition-all duration-300`}
//                                        placeholder="Confirm password"
//                                    />
//                                </div>
//                            </div>

//                            {/* Terms and Conditions */}
//                            <label className="flex items-start space-x-3 cursor-pointer pt-2">
//                                <input
//                                    type="checkbox"
//                                    name="agreeTerms"
//                                    checked={formData.agreeTerms}
//                                    onChange={handleChange}
//                                    className={`w-4 h-4 text-${primary}-600 border-gray-300 rounded focus:ring-${primary}-500/50 mt-1`}
//                                />
//                                <span className="text-sm text-gray-600 flex-1">
//                                    I agree to the{' '}
//                                    <a href="#" className={`text-${primary}-600 hover:text-${secondary}-600 font-medium transition-colors`}>
//                                        Terms of Service
//                                    </a>{' '}
//                                    and{' '}
//                                    <a href="#" className={`text-${primary}-600 hover:text-${secondary}-600 font-medium transition-colors`}>
//                                        Privacy Policy
//                                    </a>
//                                </span>
//                            </label>

//                            {/* Create Account Button */}
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
//                                        <span>Creating Account...</span>
//                                    </div>
//                                ) : (
//                                    'Create Account'
//                                )}
//                            </button>

//                            {/* Separator */}
//                            <div className="relative my-6">
//                                <div className="absolute inset-0 flex items-center">
//                                    <div className="w-full border-t border-gray-200"></div>
//                                </div>
//                                <div className="relative flex justify-center text-sm">
//                                    <span className="px-2 bg-white text-gray-500">Already have an account?</span>
//                                </div>
//                            </div>

//                            {/* Sign In Link Button */}
//                            <Link
//                                to="/login"
//                                className={`w-full py-3 px-6 border-2 border-gray-300 text-gray-700 font-semibold rounded-xl
//                                         hover:border-${primary}-500 hover:text-${primary}-600 transition-all duration-300
//                                         flex items-center justify-center space-x-2 shadow-sm hover:shadow-md`}
//                            >
//                                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10 19l-7-7m0 0l7-7m-7 7h18" />
//                                </svg>
//                                <span>Sign in to your account</span>
//                            </Link>
//                        </form>
//                    </div>
//                </div>
//            </div>
//        </div>
//    );
//};

//export default RegisterPage;


// src/pages/Auth/RegisterPage.jsx
import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { register } from '../../api/authService';
import communityImage from '../../assets/istockphoto-1498878143-612x612.jpg';

const RegisterPage = () => {
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
        agreeTerms: false
    });
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (formData.password !== formData.confirmPassword) {
            setError('Passwords do not match');
            return;
        }
        if (!formData.agreeTerms) {
            setError('Please agree to the terms and conditions');
            return;
        }

        setLoading(true);
        setError('');
        try {
            await register(formData.username, formData.email, formData.password);
            alert('Account created successfully! Please log in.');
            navigate('/login');
        } catch (err) {
            setError(err.message || 'Registration failed. The email may already be in use.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-slate-50 to-slate-100 flex items-center justify-center p-4">
            <div className="w-full max-w-6xl mx-auto">
                <div className="bg-white/80 backdrop-blur-xl shadow-2xl rounded-3xl overflow-hidden flex flex-col md:flex-row border border-white/20">

                    {/* Right Side: Image - „À· LoginPage */}
                    <div className="hidden md:block md:w-1/2 relative order-last md:order-first">
                        <img
                            src={communityImage}
                            alt="Community"
                            className="w-full h-full object-cover"
                        />
                        <div className="absolute inset-0 flex items-center justify-center p-12">
                            <div className="text-center text-white">
                                <h3 className="text-4xl font-bold mb-4 drop-shadow-lg">Start Your Journey</h3>
                                <p className="text-lg text-white drop-shadow-md">Join thousands of users who share their stories and connect with others.</p>
                            </div>
                        </div>
                    </div>

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

                            <h1 className="text-3xl font-bold text-slate-800 mb-2">Create Account</h1>
                           
                        </div>

                        <form onSubmit={handleSubmit} className="space-y-5">
                            {error && (
                                <div className="bg-red-50 border border-red-200 text-red-600 px-4 py-3 rounded-xl text-sm">
                                    {error}
                                </div>
                            )}

                            {/* Username */}
                            <div className="space-y-2">
                                <label className="block text-sm font-medium text-slate-700">Username</label>
                                <input
                                    type="text"
                                    name="username"
                                    value={formData.username}
                                    onChange={handleChange}
                                    required
                                    className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm placeholder-slate-400
                                             focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10
                                             transition-all duration-200"
                                    placeholder="Choose a username"
                                />
                            </div>

                            {/* Email */}
                            <div className="space-y-2">
                                <label className="block text-sm font-medium text-slate-700">Email Address</label>
                                <input
                                    type="email"
                                    name="email"
                                    value={formData.email}
                                    onChange={handleChange}
                                    required
                                    className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm placeholder-slate-400
                                             focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10
                                             transition-all duration-200"
                                    placeholder="your@email.com"
                                />
                            </div>

                            {/* Password */}
                            <div className="space-y-2">
                                <label className="block text-sm font-medium text-slate-700">Password</label>
                                <input
                                    type="password"
                                    name="password"
                                    value={formData.password}
                                    onChange={handleChange}
                                    required
                                    className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm placeholder-slate-400
                                             focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10
                                             transition-all duration-200"
                                    placeholder="Create password"
                                />
                            </div>

                            {/* Confirm Password */}
                            <div className="space-y-2">
                                <label className="block text-sm font-medium text-slate-700">Confirm Password</label>
                                <input
                                    type="password"
                                    name="confirmPassword"
                                    value={formData.confirmPassword}
                                    onChange={handleChange}
                                    required
                                    className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm placeholder-slate-400
                                             focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10
                                             transition-all duration-200"
                                    placeholder="Confirm password"
                                />
                            </div>

                            {/* Terms and Conditions */}
                            <label className="flex items-start gap-3 cursor-pointer pt-2">
                                <input
                                    type="checkbox"
                                    name="agreeTerms"
                                    checked={formData.agreeTerms}
                                    onChange={handleChange}
                                    className="w-4 h-4 text-blue-600 border-slate-300 rounded focus:ring-blue-500/20 mt-1"
                                />
                                <span className="text-sm text-slate-600 flex-1">
                                    I agree to the{' '}
                                    <a href="#" className="text-blue-600 hover:text-blue-700 font-medium">
                                        Terms of Service
                                    </a>{' '}
                                    and{' '}
                                    <a href="#" className="text-blue-600 hover:text-blue-700 font-medium">
                                        Privacy Policy
                                    </a>
                                </span>
                            </label>

                            {/* Create Account Button */}
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
                                        <span>Creating Account...</span>
                                    </div>
                                ) : (
                                    'Create Account'
                                )}
                            </button>

                            {/* Divider */}
                            <div className="relative my-8">
                                <div className="absolute inset-0 flex items-center">
                                    <div className="w-full border-t border-slate-200"></div>
                                </div>
                                <div className="relative flex justify-center text-sm">
                                    <span className="px-4 bg-white text-slate-500">Already have an account?</span>
                                </div>
                            </div>

                            {/* Sign In Link Button */}
                            <Link
                                to="/login"
                                className="w-full py-3 px-4 border-2 border-slate-200 text-slate-700 font-medium rounded-xl
                                         hover:border-blue-500 hover:text-blue-600 transition-all duration-200
                                         flex items-center justify-center gap-2"
                            >
                                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10 19l-7-7m0 0l7-7m-7 7h18" />
                                </svg>
                                <span>Sign in to your account</span>
                            </Link>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RegisterPage;