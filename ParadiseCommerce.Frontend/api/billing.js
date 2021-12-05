import { get, post } from '@/api/index'

export default {
    async getByOrder(id) {
        return await get("billing/invoices/order/" + id)
    }
}