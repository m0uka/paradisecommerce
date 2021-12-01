import { defineStore } from 'pinia'
import auth from '@/api/auth'

function parseJwt(token) {
    var base64Url = token.split('.')[1]
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
    }).join(''))

    return JSON.parse(jsonPayload)
}

export const useAuthStore = defineStore('auth', {
    state: () => ({
        loaded: false,
        loggedIn: false,

        jwt: '',
        expiration: '',

        name: '',
        email: '',

        id: '',
    }),

    actions: {
        async login(username, password) {
            const data = await auth.login(username, password)
            
            this.jwt = data.token
            this.expiration = data.expiration

            this.checkJWT(data.token)

            localStorage.setItem('token', JSON.stringify({
                jwt: data.token,
                expiration: data.expiration,
            }))

            return data
        },

        async register(username, password, email) {
            const result = await auth.register(username, password, email)
            await this.login(username, password)
            return result
        },

        checkJWT(token) {
            try {
                const parsedToken = parseJwt(token.jwt)
                const expire = new Date(token.expiration)

                if (expire > new Date()) {
                    this.jwt = token.jwt
                    this.expiration = token.expiration
                    this.name = parsedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
                    this.email = parsedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']
                    this.id = parsedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
                    this.loggedIn = true
                } else {
                    console.log('JWT token expired!')
                    this.loggedIn = false
                }
            }
            catch (e) {

            }

        },

        loadUser() {
            const token = localStorage.getItem('token')
            console.log(token)
            if (token) {
                this.checkJWT(JSON.parse(token))
            }
        }
    }
})