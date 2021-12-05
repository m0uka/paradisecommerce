<template>
    <RootComponent>
        <Navbar />

        <div class="bg-white">
            <div class="max-w-2xl mx-auto px-4 lg:max-w-7xl lg:px-8">
                <Breadcrumb :pages="breadcrumbPages" class="pt-10 -mb-16" />

                <div class="border-b border-gray-200 pt-24 pb-10">
                <h1 class="text-4xl font-extrabold tracking-tight text-gray-900">{{ category?.name }}</h1>
                    <p class="mt-4 text-base text-gray-500">{{ category?.description }}</p>
                </div>
            </div>
        </div>
        
        <ProductList :products="productsComputed" />

        <Footer />
    </RootComponent>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router'
import { useProductsStore } from "@/stores/products"
import { storeToRefs } from 'pinia'

const route = useRoute()

const productsStore = useProductsStore()
const { products, categories } = storeToRefs(productsStore)

const category = computed( () => categories?.value?.find(x => x.slug === route.params.id) )
const productsComputed = computed( () => products?.value?.filter(x => x.groupId === category?.value?.id) )

const breadcrumbPages = computed( () => [
  { name: (category?.value?.name ?? 'Category'), href: (`#`) },
])

</script>
