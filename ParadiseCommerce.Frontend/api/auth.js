import { post } from '@/api/index'

export default {
    async login(username, password) {
        const response = await post("auth/login", { username, password })
        const data = await response.json()
        return data
    }
}