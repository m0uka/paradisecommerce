import { defineStore } from 'pinia'
import products from '@/api/products'

export const useProductsStore = defineStore('products', {
    state: () => ({
        loaded: false,
        categoriesLoaded: false,
        products: [],
        categories: []
    }),

    actions: {
        async fetch() {
            const data = await products.fetchData()
            console.log(data)
            
            this.loaded = true
            this.products = data
        },

        async fetchCategories() {
            const data = await products.fetchCategories()
            console.log(data)
            
            this.categoriesLoaded = true
            this.categories = data
        }
    }
})