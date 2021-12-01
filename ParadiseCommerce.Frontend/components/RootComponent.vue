<template>
    <div>
        <slot />
    </div>
</template>

<script>
import { useStorefrontStore } from "@/stores/storefront"
import { useAuthStore } from "@/stores/auth"

export default {
    setup() {
        const storefrontStore = useStorefrontStore()
        const authStore = useAuthStore()

        storefrontStore.fetch()
        authStore.loadUser()
    }
}
</script>