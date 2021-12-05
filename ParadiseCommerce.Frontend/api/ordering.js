import { get, post } from '@/api/index'

export default {
    async placeOrder(dat) {
        return await post("order/place", dat)
    },

    async getOrder(orderId) {
        return await get("order/get/" + orderId)
    },

    async fetch() {
        return await get("order/orders")
    }
}