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
    created() {
        const storefrontStore = useStorefrontStore()
        const authStore = useAuthStore()
        const productsStore = useProductsStore()

        storefrontStore.fetch()
        authStore.loadUser()

        productsStore.fetchCategories()
        productsStore.fetch()
    }
}
</script>