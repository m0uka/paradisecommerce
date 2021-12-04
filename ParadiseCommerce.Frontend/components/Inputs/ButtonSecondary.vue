<template>
    <button type="submit" :class="['mr-3 border border-gray-300 text-gray-700 bg-white hover:bg-gray-50 rounded-md shadow-sm py-2 px-4 inline-flex justify-center text-sm font-medium focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-900']">
        <slot />

        <Spinner v-if="loading" class="ml-3" noMargins />
    </button>
</template>

<script>
export default {
    props: {
        loading: Boolean,
    }
}
</script>