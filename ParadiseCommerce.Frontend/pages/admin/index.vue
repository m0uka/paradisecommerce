<template>
    <AdminLayout>
        
        <Card>
            <CardContent title="Store Settings" subtitle="General store settings.">

                <div class="mt-6 grid grid-cols-4 gap-6">
                    
                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>Store Name</InputLabel>
                        <InputDescription>The name of your store that will be displayed</InputDescription>
                        <TextInput v-model="storeName" />
                    </div> 
                    
                    <div class="col-span-4 sm:col-span-2">
                        <InputLabel>Store Logo</InputLabel>
                        <InputDescription>The URL of your store logo</InputDescription>
                        <TextInput v-model="storeLogo" />
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
        const { name, logo } = storeToRefs(storefront)
        const storeLoading = ref(false)

        return {
            storeName: name,
            storeLogo: logo,
            storeLoading
        }
    },

    methods: {
        async save() {
            this.storeLoading = true

            await storefront.updateStore({
                storeName: this.storeName,
                storeLogo: this.storeLogo
            })

            this.storeLoading = false
        }
    }
}


</script>