<template>
    <p :class="[color ?? 'bg-primary-600', 'h-10 flex items-center justify-center text-sm font-medium text-white px-4 sm:px-6 lg:px-8']">
        <slot />
    </p>
</template>

<script>
export default {
    props: {
        color: String,
    }
}
</script>