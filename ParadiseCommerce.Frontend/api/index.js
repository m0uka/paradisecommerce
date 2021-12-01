const API_URL = 'https://localhost:4000/api/'

export function get(url) {
    return fetch(API_URL + url)
}

export function post(url, data) {
    return fetch(API_URL + url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })
}