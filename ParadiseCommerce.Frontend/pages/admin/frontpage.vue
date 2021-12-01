<template>
    <AdminLayout>
        
        <Card>
            <CardContent title="Hero" subtitle="Front page hero settings.">

                <div class="mt-6 grid grid-cols-4 gap-6">
                    
                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>Hero Title</InputLabel>
                        <InputDescription>The title of the large text on the front page</InputDescription>
                        <TextInput v-model="frontPage.title" />
                    </div> 
                    
                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>Hero Subtitle</InputLabel>
                        <InputDescription>The subtitle of the large text on the front page</InputDescription>
                        <TextInput v-model="frontPage.subtitle" />
                    </div> 
                    
                </div>

            </CardContent>

            <CardFooter>
                <Button :loading="storeLoading" @click="save">Save</Button>
            </CardFooter>
        </Card>


    </AdminLayout>
</template>

<script>
import { ref } from 'vue'
import { storeToRefs } from 'pinia'
import { useStorefrontStore } from '@/stores/storefront'

import storefront from '@/api/storefront'

export default {
    setup() {
        const storefront = useStorefrontStore()
        const { frontPage } = storeToRefs(storefront)
        const storeLoading = ref(false)

        return {
            frontPage,
            storeLoading
        }
    },

    methods: {
        async save() {
            this.storeLoading = true

            await storefront.updateStore({
                frontPage: this.frontPage
            })

            this.storeLoading = false
        }
    }
}


</script>