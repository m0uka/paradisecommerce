<template>
    <input :name="name" type="checkbox" class="h-4 w-4 text-primary-600 focus:ring-primary-500 border-gray-300 rounded" />
</template>

<script>
export default {
    props: {
        name: String,
    }
}
</script>