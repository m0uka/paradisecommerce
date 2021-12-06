<template>
    <SettingsLayout>
        
        <Card>
            <CardContent title="Hero" subtitle="Front page hero settings.">

                <div class="mt-6 grid grid-cols-4 gap-6">
                    
                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>Hero Title</InputLabel>
                        <InputDescription>The title of the large text on the front page</InputDescription>
                        <TextInput v-if="frontPageData" v-model="frontPageData.title" />
                    </div> 
                    
                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>Hero Subtitle</InputLabel>
                        <InputDescription>The subtitle of the large text on the front page</InputDescription>
                        <TextInput v-if="frontPageData" v-model="frontPageData.subtitle" />
                    </div>

                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>Hero Image</InputLabel>
                        <InputDescription>The URL of the large image on the front page</InputDescription>
                        <TextInput v-if="frontPageData" v-model="frontPageData.imageUrl" />
                    </div>

                    <div class="col-span-4">
                        
                    </div>
                    
                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>CTA button text</InputLabel>
                        <InputDescription>The text of the large Call-to-Action button</InputDescription>
                        <TextInput v-if="frontPageData" v-model="frontPageData.mainButtonText" />
                    </div>

                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>CTA button location</InputLabel>
                        <InputDescription>The redirect location of the large Call-to-Action button</InputDescription>
                        <TextInput v-if="frontPageData" v-model="frontPageData.mainButtonLocation" />
                    </div>
                    
                </div>

            </CardContent>
        </Card>

        <Card>
            <CardContent title="Featured Categories" subtitle="Front page featured categories.">

                <div class="mt-6 grid grid-cols-3 gap-6">
                    
                    <div class="col-span-3 md:col-span-1">
                        <InputLabel>First</InputLabel>
                        <InputDescription>The first featured category</InputDescription>
                        <Dropdown v-if="featuredCategories" :data="categories" v-model="featuredCategories[0]" />
                    </div> 
                    
                    <div class="col-span-3 md:col-span-1">
                        <InputLabel>Second</InputLabel>
                        <InputDescription>The second featured category</InputDescription>
                        <Dropdown v-if="featuredCategories" :data="categories" v-model="featuredCategories[1]" />
                    </div>

                    <div class="col-span-3 md:col-span-1">
                        <InputLabel>Third</InputLabel>
                        <InputDescription>The third featured category</InputDescription>
                        <Dropdown v-if="featuredCategories" :data="categories" v-model="featuredCategories[2]" />
                    </div>
                    
                </div>

            </CardContent>
        </Card>

        <div class="mt-5 md:mt-0 md:col-span-2">
            <Card>
                <CardFooter>
                    <Button :loading="storeLoading" @click="save">Save</Button>
                </CardFooter>
            </Card>
        </div>


    </SettingsLayout>
</template>

<script>
import { ref } from 'vue'
import { storeToRefs } from 'pinia'

import { useProductsStore } from "@/stores/products"
import { useStorefrontStore } from '@/stores/storefront'

import storefront from '@/api/storefront'

export default {
    setup() {
        const storefront = useStorefrontStore()
        const productsStore = useProductsStore()

        const { frontPage } = storeToRefs(storefront)
        const { categories } = storeToRefs(productsStore)

        const featuredCategories = ref([ '', '', '' ])

        const frontPageData = ref({
            title: '',
        })
        const storeLoading = ref(false)

        return {
            frontPage,
            categories,
            featuredCategories,
            storeLoading,
            frontPageData
        }
    },

    watch: {
        frontPage: function (val) {
            if (val) {
                this.frontPageData = val
                this.featuredCategories = val.featuredCategories
            }
        }
    },

    methods: {
        async save() {
            this.storeLoading = true

            const frontPage = this.frontPageData
            frontPage.featuredCategories = this.featuredCategories

            console.log(frontPage.featuredCategories)

            await storefront.updateStore({
                frontPage: frontPage
            })

            this.storeLoading = false
        }
    }
}


</script>