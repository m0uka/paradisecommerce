import { defineStore } from 'pinia'
import orders from '@/api/ordering'

export const useOrdersStore = defineStore('orders', {
    state: () => ({
        loaded: false,
        orders: [],
    }),

    actions: {
        async fetch() {
            const response = await orders.fetch()
            const data = await response.json()
            
            this.loaded = true
            this.orders = data
        },
    }
})