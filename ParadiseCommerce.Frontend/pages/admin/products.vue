<template>
    <AdminLayout>

        <Heading>
            <HeadingTitle>Products</HeadingTitle>
        </Heading>
        
        <Table>
            <TableHeader :items="['Product', 'Status', 'Type', 'Category']" />
            <TableBody>
                <tr v-for="product in products" :key="product.id">
                    <td class="px-6 py-4 whitespace-nowrap text-lg font-semibold text-gray-800">
                        {{ product.name }}
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-md text-gray-500">
                        <span :class="['px-3 inline-flex text-sm leading-5 font-semibold rounded-full', product.isHidden ? 'bg-red-100 text-red-900' : 'bg-green-100 text-green-800']">
                            {{ product.isHidden ? 'Hidden' : 'Active' }}
                        </span>
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-md text-gray-600">
                        {{ product.productType }}
                    </td>

                    <td class="px-6 py-4 whitespace-nowrap text-md text-gray-600">
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