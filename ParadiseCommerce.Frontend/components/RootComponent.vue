<template>
    <div class="bg-gray-100">
        <slot />
    </div>
</template>

<script>
import { useStorefrontStore } from "@/stores/storefront"
import { useAuthStore } from "@/stores/auth"
import { useProductsStore } from "@/stores/products"

export default {
    created() {
        const storefrontStore = useStorefrontStore()
        const authStore = useAuthStore()
        const productsStore = useProductsStore()

        storefrontStore.fetch()
        authStore.loadUser()
    }
}
</script>