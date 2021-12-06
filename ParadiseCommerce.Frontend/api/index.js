import { useAuthStore } from '@/stores/auth'

export async function get(url) {
    const authStore = useAuthStore()
    const config = useRuntimeConfig()

    return fetch(config.baseUrl + url, {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + authStore.jwt
        },
    })
}

export function post(url, data) {
    const authStore = useAuthStore()
    const config = useRuntimeConfig()

    return fetch(config.baseUrl + url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + authStore.jwt
        },
        body: JSON.stringify(data)
    })
}