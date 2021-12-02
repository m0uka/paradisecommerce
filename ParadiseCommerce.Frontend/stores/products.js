import { defineStore } from 'pinia'
import products from '@/api/products'

export const useProductsStore = defineStore('products', {
    state: () => ({
        loaded: false,
        products: [],
    }),

    actions: {
        async fetch() {
            const data = await products.fetchData()
            console.log(data)
            
            this.loaded = true
            this.products = data
        },
    }
})