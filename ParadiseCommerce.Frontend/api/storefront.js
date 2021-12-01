import { get, post } from '@/api/index'

export default {
    async fetchData() {
        const response = await get("store/storefront")
        const data = await response.json()
        return data
    },

    async updateStore(dat) {
        await post("store/update", dat)
    }
}