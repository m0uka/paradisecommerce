import { defineStore } from 'pinia'
import products from '@/api/products'

export const useProductsStore = defineStore('products', {
    state: () => ({
        loaded: false,
        categoriesLoaded: false,
        products: [],
        categories: []
    }),

    getters: {
        navbarCategories: (state) => {
            return state.categories.filter(x => x.showInNavbar)
        }
    },

    actions: {
        async fetch() {
            const data = await products.fetchData()
            
            this.loaded = true
            this.products = data
        },

        async fetchCategories() {
            const data = await products.fetchCategories()
            
            this.categoriesLoaded = true
            this.categories = data
        }
    }
})