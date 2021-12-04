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
    },

    async updateProduct(product) {
        const response = await post("products/update", product)
        return response
    },

    async createProduct(product) {
        const response = await post("products/create", product)
        return response
    },

    async deleteProduct(id) {
        const response = await post("products/delete", id)
        return response
    }
}