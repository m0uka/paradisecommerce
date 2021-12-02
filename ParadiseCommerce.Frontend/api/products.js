import { get, post } from '@/api/index'

export default {
    async fetchData() {
        const response = await get("products/get")
        const data = await response.json()
        return data
    },

    async fetchCategories() {
        const response = await get("categories/get")
        const data = await response.json()
        return data
    },

    async createCategory(category) {
        const response = await post("categories/create", category)
        return response
    }
}