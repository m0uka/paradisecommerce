import { get, post } from '@/api/index'

export default {
    async fetchData() {
        const response = await get("products/get")
        const data = await response.json()
        return data
    },
}