<template>
    <AdminLayout>
        
        <Heading>
            <HeadingTitle>{{ !isCreating ? (product?.name ?? "Loading..") : 'Create new product' }}</HeadingTitle>
        </Heading>

        <Modal v-model:open="deleteModal">
            <template v-slot:icon>
                <ExclamationIcon class="h-6 w-6 text-red-600" aria-hidden="true" />
            </template>

            <template v-slot:content>
                <DialogTitle as="h3" class="text-lg leading-6 font-medium text-gray-900">
                    Delete product
                </DialogTitle>
                <div class="mt-2">
                    <p class="text-sm text-gray-500">
                        Are you sure you want to delete your product? This action is irreversible.
                    </p>
                </div>
            </template>

            <template v-slot:footer>
                <Button @click="deleteProduct()" color="bg-red-700 hover:bg-red-800 text-white">Delete</Button>
                <ButtonSecondary @click="deleteModal = false">Cancel</ButtonSecondary>
            </template>
        </Modal>

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
                                <InputLabel>Short description</InputLabel>
                                <InputDescription>A short optional description for your product</InputDescription>
                                <TextInput v-model="productGeneral.shortDescription" placeholder="Something short about this product." />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Slug</InputLabel>
                                <InputDescription>SEO-friendly URL slug</InputDescription>
                                <TextInput v-model="productGeneral.slug" placeholder="my-cool-tshirt" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Type</InputLabel>
                                <Dropdown :data="[ { id: 1, name: 'Physical'}, { id: 2, name: 'Digital' }, { id: 3, name: 'Service' } ]" :modelValue="productGeneral.productType ?? 'Physical'" v-model="productGeneral.productType" />
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
                    </Card>
                </div>


                <CardDescription name="Image Carousel" description="The images present in the carousel. Leave the input empty to not display the image." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardContent>

                        <div class="grid grid-cols-2 gap-6">
                            
                            <div v-for="(entry, index) in carousel" class="col-span-4 sm:col-span-2">
                                <InputLabel>Image #{{ index + 1 }}</InputLabel>
                                <TextInput v-model="carousel[index]" @update:modelValue="carouselChange(index, $event)" placeholder="None" />
                            </div>

                        </div>

                        </CardContent>

                        <CardFooter>
                            <Button @click="addCarousel()">Add image</Button>
                        </CardFooter>
                    </Card>
                </div>


                <CardDescription name="Finish" description="Finish setting up the product here." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardFooter>
                            <ButtonSecondary @click="router.push('/admin/products')">Go back</ButtonSecondary>
                            <Button color="bg-red-700 hover:bg-red-800 text-white mr-3" v-if="!isCreating" @click="deleteModal = true">Delete</Button>
                            <Button :loading="loading" @click="update">{{ isCreating ? 'Create product' : 'Update product' }}</Button>
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
import { useRoute, useRouter } from 'vue-router'
import { computed, ref, watch } from 'vue'
import productsAPI from '@/api/products'

import { DialogTitle } from '@headlessui/vue'
import { ExclamationIcon } from '@heroicons/vue/outline'

const route = useRoute()
const router = useRouter()

const isCreating = route.params.id == 'create'

let productGeneral = ref({})
let productImages = ref({})

let deleteModal = ref(false)
let loading = ref(false)

let price = ref()
let currency = ref('USD')

let categoryName = ref('')

let carousel = ref([])

const productsStore = useProductsStore()
const { products, categories } = storeToRefs(productsStore)

// maybe having all products in the store is not a good idea? fetch products from the backend maybe??
const product = computed( () => products?.value?.find(x => x.id === route.params.id) )

let oneDone = false

watch(product, (val) => {
    if (val) {
        productGeneral.value = val
        productImages.value = val.images ?? {}
        carousel.value = val.images?.carousel ?? [ '' ]
        chooseCurrency()

        if (oneDone) {
            setCategoryValue()
        }

        oneDone = true
    }
})

watch(categories, (val) => {
    if (oneDone) {
        setCategoryValue()
    }

    oneDone = true
})

function setCategoryValue() {
    categoryName.value = categories.value?.find(category => category.id == productGeneral.value.groupId)?.name ?? categories.value?.name
}


function chooseCurrency(val) {
    if (product.value) {
        const pricing = product.value.pricing || {}
        const firstKey = Object.keys(pricing.currencyPrices)[0]

        currency.value = firstKey
        price.value = pricing.currencyPrices[firstKey ?? 'CZK']
    }
}

watch(currency, (val) => {
    chooseCurrency(val)
})

function carouselChange(index, val) {
    if (val == '') {
        carousel.value.splice(index)
    }
}

function addCarousel() {
    carousel.value[carousel.value.length] = ''
}

async function update() {
    loading.value = true

    let productCopy = {}
    Object.assign(productCopy, productGeneral.value)

    productCopy.groupId = productsStore.categories.find(category => category.name == categoryName.value)?.id
    productCopy.pricing = {}

    Object.assign(productCopy.pricing, {
        currencyPrices: {
            [currency.value]: Number(price.value)
        }
    })

    productCopy.images = {}
    Object.assign(productCopy.images, productImages.value)
    productCopy.images.carousel = carousel.value

    if (!isCreating)
        await productsAPI.updateProduct(productCopy)
    else
        await productsAPI.createProduct(productCopy)

    loading.value = false
}

async function deleteProduct() {
    await productsAPI.deleteProduct(route.params.id)
    router.push(`/admin/products`)
}

</script>