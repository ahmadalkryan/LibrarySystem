/* eslint-disable no-unused-vars */

// src/pages/CategoriesPage.jsx
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import CategoryService from '../api/CategoryService';
import { PencilIcon, TrashIcon, PlusIcon, MagnifyingGlassIcon } from '@heroicons/react/24/outline';

const CategoriesPage = () => {
    // State management for categories data
    const [categories, setCategories] = useState([]);
    const [filteredCategories, setFilteredCategories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [searchTerm, setSearchTerm] = useState('');
    const [showModal, setShowModal] = useState(false);
    const [modalMode, setModalMode] = useState('add'); // 'add' or 'edit'
    const [currentCategory, setCurrentCategory] = useState({
        id: null,
        name: '',
        description: ''
    });
    const [error, setError] = useState('');
    const [successMessage, setSuccessMessage] = useState('');

    // Check user permissions from auth context
    const { user, isAdmin, isLibrarian } = useAuth();

    // ✅ Debug: عرض صلاحيات المستخدم
    console.log('Current user:', user, localStorage.getItem('role'));

    console.log('isAdmin:', isAdmin, 'isLibrarian:', isLibrarian);
 
    const canModify = isAdmin || isLibrarian;
    console.log('canModify:', canModify );

    // Load categories on component mount
    useEffect(() => {
        loadCategories();
    }, []);

    // Filter categories based on search term
    useEffect(() => {
        if (searchTerm.trim() === '') {
            setFilteredCategories(categories);
        } else {
            const filtered = categories.filter(cat =>
                cat.name?.toLowerCase().includes(searchTerm.toLowerCase()) ||
                cat.description?.toLowerCase().includes(searchTerm.toLowerCase())
            );
            setFilteredCategories(filtered);
        }
    }, [searchTerm, categories]);

    // Fetch all categories from API
    const loadCategories = async () => {
        setLoading(true);
        setError('');
        try {
            const data = await CategoryService.getAllCategories();
            console.log('Categories loaded:', data);

            // ✅ تحويل البيانات من { Id, Name, Description } إلى { id, name, description }
            const formattedData = data.map(item => ({
                id: item.Id,
                name: item.Name,
                description: item.Description
            }));

            setCategories(formattedData);
            setFilteredCategories(formattedData);
        } catch (err) {
            setError('Failed to load categories');
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    // Open modal for adding new category
    const handleAddClick = () => {
        setModalMode('add');
        setCurrentCategory({ id: null, name: '', description: '' });
        setShowModal(true);
        setError('');
    };

    // Open modal for editing existing category
    const handleEditClick = (category) => {
        setModalMode('edit');
        setCurrentCategory({
            id: category.id,
            name: category.name || '',
            description: category.description || ''
        });
        setShowModal(true);
        setError('');
    };

    // Delete category after confirmation
    const handleDelete = async (id) => {
        if (!window.confirm('Are you sure you want to delete this category?')) return;

        try {
            await CategoryService.deleteCategory(id);
            setSuccessMessage('Category deleted successfully');
            await loadCategories();
            setTimeout(() => setSuccessMessage(''), 3000);
        } catch (err) {
            setError('Failed to delete category');
            setTimeout(() => setError(''), 3000);
        }
    };

    // Handle form submission for add/edit
    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!currentCategory.name?.trim()) {
            setError('Category name is required');
            return;
        }

        setLoading(true);
        try {
            if (modalMode === 'add') {
                await CategoryService.createCategory({
                    name: currentCategory.name,
                    description: currentCategory.description
                });
                setSuccessMessage('Category added successfully');
            } else {
                await CategoryService.updateCategory({
                    id: currentCategory.id,
                    name: currentCategory.name,
                    description: currentCategory.description
                });
                setSuccessMessage('Category updated successfully');
            }

            setShowModal(false);
            await loadCategories();
            setTimeout(() => setSuccessMessage(''), 3000);
        } catch (err) {
            setError('Operation failed. Please try again.');
            setTimeout(() => setError(''), 3000);
        } finally {
            setLoading(false);
        }
    };

    // Safe function to get first character of name
    const getInitial = (name) => {
        return name && name.length > 0 ? name.charAt(0) : '?';
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-slate-50 to-slate-100 p-4 md:p-6">
            <div className="max-w-7xl mx-auto">
                {/* Page Header */}
                <div className="mb-8">
                    <h1 className="text-3xl font-bold text-slate-800 mb-2">🏷️ Categories Management</h1>
                    <p className="text-slate-600">Add, edit and delete book categories</p>
                </div>

                {/* Error and Success Messages */}
                {error && (
                    <div className="mb-4 bg-red-50 border border-red-200 text-red-600 px-4 py-3 rounded-xl">
                        {error}
                    </div>
                )}
                {successMessage && (
                    <div className="mb-4 bg-green-50 border border-green-200 text-green-600 px-4 py-3 rounded-xl">
                        {successMessage}
                    </div>
                )}

                {/* Search and Add Bar - ✅ الأزرار تظهر حسب canModify */}
                <div className="bg-white/80 backdrop-blur-xl rounded-2xl shadow-lg p-4 mb-6 border border-white/20">
                    <div className="flex flex-col md:flex-row gap-4">
                        <div className="flex-1 relative">
                            <MagnifyingGlassIcon className="w-5 h-5 absolute right-3 top-3.5 text-slate-400" />
                            <input
                                type="text"
                                placeholder="Search categories..."
                                value={searchTerm}
                                onChange={(e) => setSearchTerm(e.target.value)}
                                className="w-full pr-10 pl-4 py-3 bg-white border border-slate-200 rounded-xl text-sm
                                         focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10
                                         transition-all duration-200"
                            />
                        </div>
                        {canModify && (
                            <button
                                onClick={handleAddClick}
                                className="px-6 py-3 bg-gradient-to-r from-blue-600 to-indigo-600 text-white font-medium rounded-xl
                                         hover:from-blue-700 hover:to-indigo-700 transition-all duration-200
                                         flex items-center justify-center gap-2 shadow-lg shadow-blue-500/30"
                            >
                                <PlusIcon className="w-5 h-5" />
                                <span>Add New Category</span>
                            </button>
                        )}
                    </div>
                </div>

                {/* Categories Grid */}
                {loading ? (
                    <div key="loading-spinner" className="flex justify-center items-center py-20">
                        <div className="w-12 h-12 border-4 border-blue-600 border-t-transparent rounded-full animate-spin"></div>
                    </div>
                ) : filteredCategories.length === 0 ? (
                    <div key="no-categories" className="bg-white/80 backdrop-blur-xl rounded-2xl shadow-lg p-12 text-center border border-white/20">
                        <p className="text-slate-500 text-lg">No categories to display</p>
                    </div>
                ) : (
                    <div key="categories-grid" className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                        {filteredCategories.map((category) => (
                            <div
                                key={category.id}
                                className="bg-white/80 backdrop-blur-xl rounded-2xl shadow-lg overflow-hidden border border-white/20
                                         hover:shadow-xl transition-all duration-300 transform hover:-translate-y-1"
                            >
                                <div className="p-6">
                                    <div className="flex items-start justify-between mb-4">
                                        <div className="flex items-center gap-3">
                                            <div className="w-10 h-10 bg-gradient-to-r from-blue-600 to-indigo-600 rounded-xl flex items-center justify-center">
                                                <span className="text-white font-bold text-lg">
                                                    {getInitial(category.name)}
                                                </span>
                                            </div>
                                            <h3 className="text-lg font-semibold text-slate-800">
                                                {category.name}
                                            </h3>
                                        </div>
                                        {/* ✅ أزرار Edit/Delete تظهر حسب canModify */}
                                        {canModify && (
                                            <div className="flex gap-2">
                                                <button
                                                    onClick={() => handleEditClick(category)}
                                                    className="p-2 text-blue-600 hover:bg-blue-50 rounded-lg transition-colors"
                                                    title="Edit"
                                                >
                                                    <PencilIcon className="w-5 h-5" />
                                                </button>
                                                <button
                                                    onClick={() => handleDelete(category.id)}
                                                    className="p-2 text-red-600 hover:bg-red-50 rounded-lg transition-colors"
                                                    title="Delete"
                                                >
                                                    <TrashIcon className="w-5 h-5" />
                                                </button>
                                            </div>
                                        )}
                                    </div>

                                    {category.description && (
                                        <p className="text-slate-600 text-sm leading-relaxed">
                                            {category.description}
                                        </p>
                                    )}

                                    <div className="mt-4 pt-4 border-t border-slate-100">
                                        <Link
                                            to={`/books?category=${category.id}`}
                                            className="inline-flex items-center gap-2 text-blue-600 hover:text-blue-700 text-sm font-medium
                                                     transition-colors duration-200"
                                        >
                                            <span>View books in this category</span>
                                            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                                            </svg>
                                        </Link>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>

            {/* Add/Edit Modal */}
            {showModal && (
                <div className="fixed inset-0 bg-black/50 flex items-center justify-center p-4 z-50">
                    <div className="bg-white rounded-2xl max-w-md w-full p-6 shadow-2xl">
                        <h2 className="text-2xl font-bold text-slate-800 mb-6">
                            {modalMode === 'add' ? 'Add New Category' : 'Edit Category'}
                        </h2>

                        <form onSubmit={handleSubmit}>
                            <div className="space-y-4">
                                <div>
                                    <label className="block text-sm font-medium text-slate-700 mb-2">
                                        Category Name <span className="text-red-500">*</span>
                                    </label>
                                    <input
                                        type="text"
                                        value={currentCategory.name}
                                        onChange={(e) => setCurrentCategory({
                                            ...currentCategory,
                                            name: e.target.value
                                        })}
                                        className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm
                                                 focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10"
                                        placeholder="Enter category name"
                                        required
                                    />
                                </div>

                                <div>
                                    <label className="block text-sm font-medium text-slate-700 mb-2">
                                        Description
                                    </label>
                                    <textarea
                                        value={currentCategory.description}
                                        onChange={(e) => setCurrentCategory({
                                            ...currentCategory,
                                            description: e.target.value
                                        })}
                                        rows="4"
                                        className="w-full px-4 py-3 bg-white border border-slate-200 rounded-xl text-sm
                                                 focus:outline-none focus:border-blue-500 focus:ring-4 focus:ring-blue-500/10"
                                        placeholder="Enter category description (optional)"
                                    />
                                </div>
                            </div>

                            <div className="flex gap-3 mt-6">
                                <button
                                    type="submit"
                                    disabled={loading}
                                    className="flex-1 py-3 bg-gradient-to-r from-blue-600 to-indigo-600 text-white font-medium rounded-xl
                                             hover:from-blue-700 hover:to-indigo-700 transition-all duration-200
                                             disabled:opacity-50 disabled:cursor-not-allowed"
                                >
                                    {loading ? 'Saving...' : 'Save'}
                                </button>
                                <button
                                    type="button"
                                    onClick={() => setShowModal(false)}
                                    className="flex-1 py-3 bg-slate-100 text-slate-700 font-medium rounded-xl
                                             hover:bg-slate-200 transition-all duration-200"
                                >
                                    Cancel
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default CategoriesPage;