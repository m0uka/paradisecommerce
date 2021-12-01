<template>
    <!-- Hero section -->
    <div class="relative">
        <div aria-hidden="true" class="hidden absolute w-1/2 h-full bg-gray-100 lg:block" />
        <div class="relative bg-gray-100 lg:bg-transparent">
            <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 lg:grid lg:grid-cols-2">
                <div class="max-w-2xl mx-auto py-24 lg:py-64 lg:max-w-none">
                    <div class="lg:pr-16">
                        <h1
                            class="text-4xl font-extrabold tracking-tight text-gray-900 sm:text-5xl xl:text-6xl"
                        >{{ storefront?.frontPage?.title ?? 'Front page title' }}</h1>
                        <p
                            class="mt-4 text-xl text-gray-600"
                        >{{ storefront?.frontPage?.subtitle ?? "This is your store's front page! You can configure the front page in the admin section." }}</p>
                        <div class="mt-6">
                            <a
                                :href="storefront?.frontPage?.mainButtonLocation ?? '/admin'"
                                class="inline-block bg-primary-600 border border-transparent py-3 px-8 rounded-md font-medium text-white hover:bg-primary-700"
                            >{{ storefront?.frontPage?.mainButtonText ?? 'Setup your store' }}</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="w-full h-48 sm:h-64 lg:absolute lg:top-0 lg:right-0 lg:w-1/2 lg:h-full">
            <img
                :src="storefront?.frontPage?.imageUrl ?? 'https://tailwindui.com/img/ecommerce-images/home-page-02-hero-half-width.jpg'"
                alt
                class="w-full h-full object-center object-cover"
            />
        </div>
    </div>
</template>

<script>
import { useStorefrontStore } from '@/stores/storefront'

export default {
    setup() {
        const storefront = useStorefrontStore()
        return {
            storefront
        }
    }
}
</script>