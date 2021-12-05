<template>
    <div class="bg-gray-100 min-h-screen h-full">
        <slot />
    </div>
</template>

<script>
import { useStorefrontStore } from "@/stores/storefront"
import { useAuthStore } from "@/stores/auth"
import { useProductsStore } from "@/stores/products"

export default {
    async created() {
        const storefrontStore = useStorefrontStore()
        const authStore = useAuthStore()
        const productsStore = useProductsStore()

        await authStore.loadUser()
        storefrontStore.fetch()

        productsStore.fetchCategories()
        productsStore.fetch()
    }
}
</script>