import { defineStore } from 'pinia'
import storefront from '@/api/storefront'

export const useStorefrontStore = defineStore('storefront', {
    state: () => ({
        loaded: false,
        name: '',
        logo: '',
        frontPage: {}
    }),

    actions: {
        async fetch() {
            const data = await storefront.fetchData()
            console.log(data)
            
            this.loaded = true
            this.name = data.storeName
            this.logo = data.storeLogo
            this.frontPage = data.frontPage

            document.title = this.name
        },
    }
})