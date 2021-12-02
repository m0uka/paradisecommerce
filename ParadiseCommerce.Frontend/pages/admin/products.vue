<template>
    <AdminLayout>

        <Heading>
            <HeadingTitle>Products</HeadingTitle>
        </Heading>
        
        <Table>
            <TableHeader :items="['Product', 'Status', 'Type', 'Category']" />
            <TableBody>
                <tr v-for="product in products" :key="product.id">
                    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        {{ product.name }}
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        <span :class="['px-2 inline-flex text-xs leading-5 font-semibold rounded-full', product.isHidden ? 'bg-red-100 text-red-900' : 'bg-green-100 text-green-800']">
                            {{ product.isHidden ? 'Hidden' : 'Active' }}
                        </span>
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        {{ product.productType }}
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        Test
                    </td>
                </tr>
                
            </TableBody>
        </Table>

    </AdminLayout>
</template>

<script setup>
import { useProductsStore } from "@/stores/products"
import { storeToRefs } from 'pinia'

const productsStore = useProductsStore()
productsStore.fetch()

const { products } = storeToRefs(productsStore)
</script>