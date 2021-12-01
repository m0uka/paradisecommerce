<template>
    <label :for="forName" class="block text-xs text-gray-400">
        <slot />
    </label>
</template>

<script>

export default {
    props: {
        forName: String,
        error: String
    }
}
</script>