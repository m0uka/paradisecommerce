<template>
    <AdminLayout>
        
        <Heading>
            <HeadingTitle>{{ product?.name ?? "Loading.." }}</HeadingTitle>
        </Heading>

        <div>
            <div class="md:grid md:grid-cols-3 md:gap-6">

                <CardDescription name="General" description="The general information of the product." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardContent>

                        <div class="grid grid-cols-2 gap-6">
                            
                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Name</InputLabel>
                                <TextInput v-model="productGeneral.name" placeholder="My cool t-shirt" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Slug</InputLabel>
                                <InputDescription>SEO-friendly URL slug</InputDescription>
                                <TextInput v-model="productGeneral.slug" placeholder="my-cool-tshirt" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Type</InputLabel>
                                <Dropdown :data="[ { id: 1, name: 'Physical'}, { id: 2, name: 'Digital' }, { id: 3, name: 'Service' } ]" v-model="productGeneral.productType" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <ToggleInput v-model="productGeneral.isHidden" title="Hidden" description="Should the product be hidden from the store?" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Description</InputLabel>
                                <TextEditor v-model="productGeneral.description" />
                            </div>

                        </div>

                        </CardContent>

                        <CardFooter>
                            <Button :loading="loadingGeneral" @click="updateGeneral">Update</Button>
                        </CardFooter>
                    </Card>
                </div>

            </div>
        </div>

    </AdminLayout>
</template>

<script setup>
import { useProductsStore } from "@/stores/products"
import { storeToRefs } from 'pinia'
import { useRoute } from 'vue-router'
import { computed, ref, watch } from 'vue'
import productsAPI from '@/api/products'

let productGeneral = ref({})
let loadingGeneral = ref(false)

const route = useRoute()

const productsStore = useProductsStore()
const { products } = storeToRefs(productsStore)
const product = computed( () => products?.value?.find(x => x.id === route.params.id) )

watch(product, (val) => {
    productGeneral.value = val
})

async function updateGeneral() {
    loadingGeneral.value = true

    let productCopy = {}
    Object.assign(productCopy, productGeneral.value)
    console.log(productCopy)

    await productsAPI.updateProduct(productCopy)
    loadingGeneral.value = false
}

</script>