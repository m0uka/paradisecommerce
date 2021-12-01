<template>
    <label :for="forName" class="block text-sm font-medium text-gray-700">
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