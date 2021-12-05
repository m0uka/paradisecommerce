<template>
    <!-- Category section -->
    <section aria-labelledby="category-heading" class="bg-gray-50">
        <div class="max-w-7xl mx-auto py-24 px-4 sm:py-32 sm:px-6 lg:px-8">
            <div class="sm:flex sm:items-baseline sm:justify-between">
                <h2
                    id="category-heading"
                    class="text-2xl font-extrabold tracking-tight text-gray-900"
                >Shop by Category</h2>
                <!-- <a
                    href="#"
                    class="hidden text-sm font-semibold text-primary-600 hover:text-primary-500 sm:block"
                >
                    Browse all categories
                    <span aria-hidden="true">&rarr;</span>
                </a> -->
            </div>

            <div
                class="mt-6 grid grid-cols-1 gap-y-6 sm:grid-cols-2 sm:grid-rows-2 sm:gap-x-6 lg:gap-8"
            >
                <div
                    class="group aspect-w-2 aspect-h-1 rounded-lg overflow-hidden sm:aspect-h-1 sm:aspect-w-1 sm:row-span-2"
                >
                    <img
                        :src="firstFeatured?.bigspotImage"
                        class="object-center object-cover group-hover:opacity-75"
                    />
                    <div
                        aria-hidden="true"
                        class="bg-gradient-to-b from-transparent to-black opacity-50"
                    />
                    <div class="p-6 flex items-end">
                        <div>
                            <h3 class="font-semibold text-white">
                                <NuxtLink :to="`/category/${firstFeatured?.slug}`">
                                    <span class="absolute inset-0" />
                                    {{ firstFeatured?.name }}
                                </NuxtLink>
                            </h3>
                            <p aria-hidden="true" class="mt-1 text-sm text-white">Shop now</p>
                        </div>
                    </div>
                </div>
                <div
                    class="group aspect-w-2 aspect-h-1 rounded-lg overflow-hidden sm:relative sm:aspect-none sm:h-full"
                >
                    <img
                        :src="secondFeatured?.bigspotImage"
                        class="object-center object-cover group-hover:opacity-75 sm:absolute sm:inset-0 sm:w-full sm:h-full"
                    />
                    <div
                        aria-hidden="true"
                        class="bg-gradient-to-b from-transparent to-black opacity-50 sm:absolute sm:inset-0"
                    />
                    <div class="p-6 flex items-end sm:absolute sm:inset-0">
                        <div>
                            <h3 class="font-semibold text-white">
                                <NuxtLink :to="`/category/${firstFeatured?.slug}`">
                                    <span class="absolute inset-0" />
                                    {{ secondFeatured?.name }}
                                </NuxtLink>
                            </h3>
                            <p aria-hidden="true" class="mt-1 text-sm text-white">Shop now</p>
                        </div>
                    </div>
                </div>
                <div
                    class="group aspect-w-2 aspect-h-1 rounded-lg overflow-hidden sm:relative sm:aspect-none sm:h-full"
                >
                    <img
                        :src="thirdFeatured?.bigspotImage"
                        class="object-center object-cover group-hover:opacity-75 sm:absolute sm:inset-0 sm:w-full sm:h-full"
                    />
                    <div
                        aria-hidden="true"
                        class="bg-gradient-to-b from-transparent to-black opacity-50 sm:absolute sm:inset-0"
                    />
                    <div class="p-6 flex items-end sm:absolute sm:inset-0">
                        <div>
                            <h3 class="font-semibold text-white">
                                <NuxtLink :to="`/category/${thirdFeatured?.slug}`">
                                    <span class="absolute inset-0" />
                                    {{ thirdFeatured?.name }}
                                </NuxtLink>
                            </h3>
                            <p aria-hidden="true" class="mt-1 text-sm text-white">Shop now</p>
                        </div>
                    </div>
                </div>
            </div>

            <!-- <div class="mt-6 sm:hidden">
                <a
                    href="#"
                    class="block text-sm font-semibold text-primary-600 hover:text-primary-500"
                >
                    Browse all categories
                    <span aria-hidden="true">&rarr;</span>
                </a>
            </div> -->
        </div>
    </section>
</template>

<script setup>
import { useProductsStore } from "@/stores/products"
import { useStorefrontStore } from '@/stores/storefront'
import { storeToRefs } from 'pinia'

const storefront = useStorefrontStore()
const productsStore = useProductsStore()

const { frontPage } = storeToRefs(storefront)
const { categories } = storeToRefs(productsStore)

const firstFeatured = computed( () => categories.value?.find(x => x.name == frontPage?.value?.featuredCategories?.[0]) )
const secondFeatured = computed( () => categories.value?.find(x => x.name == frontPage?.value?.featuredCategories?.[1]) )
const thirdFeatured = computed( () => categories.value?.find(x => x.name == frontPage?.value?.featuredCategories?.[2]) )

</script>