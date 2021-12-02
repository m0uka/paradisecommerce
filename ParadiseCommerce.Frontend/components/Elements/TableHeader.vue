<template>
    <thead class="bg-gray-50">
        <tr>
            <th v-for="item in items" :id="item" scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                {{ item }}
            </th>

            <slot />
        </tr>
    </thead>
</template>

<script>
export default {
    props: {
        items: Array
    }
}
</script>