<template>
    <div class="min-h-full flex flex-col justify-center py-12 sm:px-6 lg:px-8">
    <div class="sm:mx-auto sm:w-full sm:max-w-md">
      <img class="mx-auto h-12 w-auto" :src="storefront?.logo" />
      <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
        {{ title }}
      </h2>
      <p class="mt-2 text-center text-sm text-gray-600">
        Or
        {{ ' ' }}
        <NuxtLink :to="subtitleLocation" class="font-medium text-primary-600 hover:text-primary-500">
            {{ subtitle }}
        </NuxtLink>
      </p>
    </div>

    <div class="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
      <div class="bg-white py-8 px-4 shadow sm:rounded-lg sm:px-10">
        <slot />
      </div>
    </div>
  </div>
</template>

<script>
import { useStorefrontStore } from '@/stores/storefront'

export default {
    props: {
        title: String,
        subtitle: String,
        subtitleLocation: String,
    },
    setup() {
        const storefront = useStorefrontStore()

        return {
            storefront
        }
    }
}
</script>