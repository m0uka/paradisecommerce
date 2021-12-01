<template>
    <div class="bg-white py-6 px-4 sm:p-6">
        <div v-if="title">
            <h2 id="payment-details-heading" class="text-lg leading-6 font-medium text-gray-900">{{ title }}</h2>
            <p class="mt-1 text-sm text-gray-500">{{ subtitle }}</p>
        </div>
        <slot />
    </div>
</template>

<script>
    export default {
        props: {
            title: String,
            subtitle: String
        }
    }
</script>