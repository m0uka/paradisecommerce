<template>
    <RootComponent class="h-screen bg-gray-50">
        <AuthCard title="Create an account" subtitle="log in to your account" subtitleLocation="/account/signin">
            <form @submit.prevent="register" class="space-y-6" action="#" method="POST">
                <div>
                    <InputLabel forName="username">
                        Username
                    </InputLabel>
                    <div class="mt-1">
                        <TextInput name="username" type="text" :error="userError" v-model="username" />
                    </div>
                </div>

                <div>
                    <InputLabel forName="email">
                        E-mail
                    </InputLabel>
                    <div class="mt-1">
                        <TextInput name="email" type="email" autocomplete="email" :error="emailError" v-model="email" />
                    </div>
                </div>

                <div>
                    <InputLabel forName="password">
                        Password
                    </InputLabel>
                    <div class="mt-1">
                        <TextInput name="password" type="password" autocomplete="current-password" :error="passError" v-model="password" />
                    </div>
                </div>

                <div class="flex items-center justify-between">
                    <div class="flex items-center">
                    <Checkbox name="remember-me" />
                        <InputLabel forName="remember-me" class="ml-2 block text-sm text-gray-900">
                            I agree with the Terms of Service
                        </InputLabel>
                    </div>
                </div>

                <div>
                    <button type="submit" class="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-primary-600 hover:bg-primary-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500">
                        Create an account
                    </button>
                </div>
            </form>
        </AuthCard>
    </RootComponent>
</template>

<script>
import { ref } from 'vue'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()

export default {
    setup() {
        const loading = ref(false)
        const username = ref('')
        const email = ref('')
        const password = ref('')

        const userError = ref('')
        const emailError = ref('')
        const passError = ref('')

        return {
            username,
            password,
            email,
            loading,

            userError,
            emailError,
            passError
        }
    },

    methods: {
        async register() {
            this.loading = true
            this.error = null

            const result = await authStore.register(this.username, this.password, this.email)
            console.log(result)
            if (result.status != 200) {
                if (result.errors) {
                    Object.values(result.errors).forEach( (error) => {
                        this.passError = error
                    })
                }
            }

            this.loading = false

            if (result.status == 200)
                this.$router.push(this.$route.query.redirect ?? '/')
        }
    }
}
</script>