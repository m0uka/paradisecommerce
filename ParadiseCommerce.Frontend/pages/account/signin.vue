<template>
    <RootComponent class="h-screen bg-gray-50">
        <AuthCard title="Sign in to your account" subtitle="create an account" :subtitleLocation="`/account/signup${$route.query.redirect ? ('?redirect=' + $route.query.redirect) : ''}`">
            <form @submit.prevent="logIn" class="space-y-6" action="#" method="POST">
                <div>
                    <InputLabel forName="username">
                        Username or E-mail
                    </InputLabel>
                    <div class="mt-1">
                        <TextInput name="username" type="username" autocomplete="username" v-model="username" />
                    </div>
                </div>

                <div>
                    <InputLabel forName="password">
                        Password
                    </InputLabel>
                    <div class="mt-1">
                        <TextInput name="password" type="password" :error="error" autocomplete="current-password" v-model="password" />
                    </div>
                </div>

                <div class="flex items-center justify-between">
                    <div class="flex items-center">
                    <Checkbox name="remember-me" />
                        <InputLabel forName="remember-me" class="ml-2 block text-sm text-gray-900">
                            Remember me
                        </InputLabel>
                    </div>

                    <div class="text-sm">
                        <a href="#" class="font-medium text-primary-600 hover:text-primary-500">
                            Forgot your password?
                        </a>
                    </div>
                </div>

                <div>
                    <button type="submit" class="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-primary-600 hover:bg-primary-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500">
                        Sign in <Spinner v-if="loading" class="ml-3" noMargins />
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
        const password = ref('')
        const error = ref('')

        return {
            username,
            password,
            loading,
            error
        }
    },

    methods: {
        async logIn() {
            this.loading = true
            this.error = null

            const result = await authStore.login(this.username, this.password)
            if (result.status == 401) {
                this.error = 'Incorrect username or password.'
            }

            this.loading = false

            if (!result.status || result.status == 200)
                this.$router.push(this.$route.query.redirect ?? '/')
        }
    }
}
</script>