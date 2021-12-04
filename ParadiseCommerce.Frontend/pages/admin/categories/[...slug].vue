<template>
    <AdminLayout>

        <Heading>
            <HeadingTitle>Categories</HeadingTitle>
            <HeadingActions>
                <Button @click="router.push(`/admin/categories/create`)">
                    Create new category
                </Button>
            </HeadingActions>
        </Heading>
        
        <Table>
            <TableHeader :items="['ID', 'Name', 'Total Products']" />
            <TableBody>
                <tr v-for="category in categories" :key="category.id" class="cursor-pointer" v-on:click="selectCategory(category)">
                    <td class="px-6 py-4 whitespace-nowrap text-gray-800 text-lg font-semibold">
                        {{ category.name }}
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-gray-800">
                        {{ category.id }}
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-md text-gray-600">
                        0
                    </td>
                </tr>
                
            </TableBody>
        </Table>

    </AdminLayout>
</template>

<script setup>
import { useProductsStore } from "@/stores/products"
import { storeToRefs } from 'pinia'
import { useRouter } from 'vue-router'

const router = useRouter()
const productsStore = useProductsStore()
const { categories } = storeToRefs(productsStore)

function selectCategory(category) {
    router.push(`/admin/categories/${category.id}`)
}

</script>