import { get, post } from '@/api/index'

export default {
    async placeOrder(dat) {
        return await post("order/place", dat)
    }
}