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
                                <InputLabel>Category</InputLabel>
                                <Dropdown :data="categories" v-model="categoryName" />
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

                <CardDescription name="Pricing" description="The pricing of the product." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardContent>

                        <div class="grid grid-cols-12 gap-6">
                            
                            <div class="col-span-4">
                                <InputLabel>Price</InputLabel>
                                <PriceInput v-model="price" v-model:currency="currency" :currencies="[ 'USD', 'CZK' ]" />
                            </div>

                        </div>

                        </CardContent>

                        <CardFooter>
                            <Button :loading="loadingPrices" @click="updatePrices">Update</Button>
                        </CardFooter>
                    </Card>
                </div>


                <CardDescription name="Images" description="The image presentation of the product." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardContent>

                        <div class="grid grid-cols-2 gap-6">
                            
                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Listing</InputLabel>
                                <TextInput v-model="productImages.listing" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Listing Small</InputLabel>
                                <TextInput v-model="productImages.listingSmall" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Bigspot</InputLabel>
                                <TextInput v-model="productImages.bigspot" />
                            </div>

                        </div>

                        </CardContent>

                        <CardFooter>
                            <Button :loading="loadingImages" @click="updateImages">Update</Button>
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
let productImages = ref({})

let loadingGeneral = ref(false)
let loadingImages = ref(false)
let loadingPrices = ref(false)

let price = ref()
let currency = ref('USD')

let categoryName = ref('')

const route = useRoute()

const productsStore = useProductsStore()
const { products, categories } = storeToRefs(productsStore)
const product = computed( () => products?.value?.find(x => x.id === route.params.id) )

watch(product, (val) => {
    productGeneral.value = val
    if (val) {
        productImages.value = val.images ?? {}
        chooseCurrency()
    }
})

watch(categories, (val) => {
    categoryName.value = val.find(category => category.id == productGeneral.value.groupId)?.name
})

function chooseCurrency(val) {
    const pricing = product.value.pricing || {}
    const firstKey = Object.keys(pricing.currencyPrices)[0]

    currency.value = firstKey
    price.value = pricing.currencyPrices[firstKey ?? 'CZK']
}

watch(currency, (val) => {
    chooseCurrency(val)
})

async function updateGeneral() {
    loadingGeneral.value = true

    let productCopy = {}
    Object.assign(productCopy, productGeneral.value)
    productCopy.groupId = productsStore.categories.find(category => category.name == categoryName.value)?.id

    await productsAPI.updateProduct(productCopy)
    loadingGeneral.value = false
}

async function updatePrices() {
    loadingPrices.value = true

    let productCopy = {}
    Object.assign(productCopy, productGeneral.value)
    productCopy.prices = {}

    Object.assign(productCopy.pricing, {
        currencyPrices: {
            [currency.value]: Number(price.value)
        }
    })

    console.log(productCopy)

    await productsAPI.updateProduct(productCopy)
    loadingPrices.value = false
}

async function updateImages() {
    loadingImages.value = true

    let productCopy = {}
    Object.assign(productCopy, productGeneral.value)
    productCopy.images = {}

    Object.assign(productCopy.images, productImages.value)

    await productsAPI.updateProduct(productCopy)
    loadingImages.value = false
}

</script>