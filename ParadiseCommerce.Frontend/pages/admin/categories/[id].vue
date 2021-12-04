<template>
    <AdminLayout>
        
        <Heading>
            <HeadingTitle>{{ !isCreating ? (category?.name ?? "Loading..") : 'Create new category' }}</HeadingTitle>
        </Heading>

        <Modal v-model:open="deleteModal">
            <template v-slot:icon>
                <ExclamationIcon class="h-6 w-6 text-red-600" aria-hidden="true" />
            </template>

            <template v-slot:content>
                <DialogTitle as="h3" class="text-lg leading-6 font-medium text-gray-900">
                    Delete category
                </DialogTitle>
                <div class="mt-2">
                    <p class="text-sm text-gray-500">
                        Are you sure you want to delete this category? This action is irreversible.
                    </p>
                </div>
            </template>

            <template v-slot:footer>
                <Button @click="deleteCategory()" color="bg-red-700 hover:bg-red-800 text-white">Delete</Button>
                <ButtonSecondary @click="deleteModal = false">Cancel</ButtonSecondary>
            </template>
        </Modal>

        <div>
            <div class="md:grid md:grid-cols-3 md:gap-6">

                <CardDescription name="General" description="The general information of the category." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardContent>

                        <div class="grid grid-cols-2 gap-6">
                            
                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Name</InputLabel>
                                <TextInput v-model="categoryGeneral.name" placeholder="My cool t-shirt" />
                            </div>

                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Description</InputLabel>
                                <InputDescription>A short description of this category</InputDescription>
                                <TextInput v-model="categoryGeneral.description" />
                            </div>

                        </div>

                        </CardContent>
                    </Card>
                </div>

                <CardDescription name="Images" description="The image presentation of the category." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardContent>

                        <div class="grid grid-cols-2 gap-6">
                            
                            <div class="col-span-4 sm:col-span-2">
                                <InputLabel>Bigspot</InputLabel>
                                <InputDescription>A large image URL of the category</InputDescription>
                                <TextInput v-model="categoryGeneral.bigspotImage" />
                            </div>

                        </div>

                        </CardContent>
                    </Card>
                </div>


                <CardDescription name="Finish" description="Finish setting up the category here." />

                <div class="mt-5 md:mt-0 md:col-span-2">
                    <Card>
                        <CardFooter>
                            <ButtonSecondary @click="router.push('/admin/categories')">Go back</ButtonSecondary>
                            <Button color="bg-red-700 hover:bg-red-800 text-white mr-3" v-if="!isCreating" @click="deleteModal = true">Delete</Button>
                            <Button :loading="loading" @click="updateCategory">{{ isCreating ? 'Create category' : 'Update category' }}</Button>
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

let deleteModal = ref(false)
let loading = ref(false)

let categoryGeneral = ref({})

const productsStore = useProductsStore()
const { categories } = storeToRefs(productsStore)
const category = computed( () => categories?.value?.find(x => x.id === route.params.id) )

watch(category, (val) => {
    categoryGeneral.value = val
})

async function updateCategory() {
    loading.value = true

    let categoryCopy = {}
    Object.assign(categoryCopy, categoryGeneral.value)

    if (!isCreating)
        await productsAPI.updateCategory(categoryCopy)
    else
        await productsAPI.createCategory(categoryCopy)

    loading.value = false
}

async function deleteCategory() {
    await productsAPI.deleteCategory(route.params.id)
    router.push(`/admin/categories`)
}
</script>