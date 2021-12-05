import { useAuthStore } from '@/stores/auth'

const API_URL = 'https://localhost:4000/api/'

export async function get(url) {
    const authStore = useAuthStore()

    return fetch(API_URL + url, {
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

    return fetch(API_URL + url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + authStore.jwt
        },
        body: JSON.stringify(data)
    })
}