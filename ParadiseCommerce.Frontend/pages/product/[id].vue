<template>
    <RootComponent>
        <Navbar>
  <div class="bg-white">
    <div class="max-w-2xl mx-auto py-16 px-4 sm:py-24 sm:px-6 lg:max-w-7xl lg:px-8">
      <div class="lg:grid lg:grid-cols-2 lg:gap-x-8 lg:items-start">
        <!-- Image gallery -->
        <TabGroup as="div" class="flex flex-col-reverse">
          <!-- Image selector -->
          <div class="hidden mt-6 w-full max-w-2xl mx-auto sm:block lg:max-w-none">
            <TabList class="grid grid-cols-4 gap-6">
              <Tab v-for="image in product?.images?.carousel" :key="image.id" class="relative h-24 bg-white rounded-md flex items-center justify-center text-sm font-medium uppercase text-gray-900 cursor-pointer hover:bg-gray-50 focus:outline-none focus:ring focus:ring-offset-4 focus:ring-opacity-50" v-slot="{ selected }">
                <span class="absolute inset-0 rounded-md overflow-hidden">
                  <img :src="image" alt="" class="w-full h-full object-center object-cover" />
                </span>
                <span :class="[selected ? 'ring-primary-500' : 'ring-transparent', 'absolute inset-0 rounded-md ring-2 ring-offset-2 pointer-events-none']" aria-hidden="true" />
              </Tab>
            </TabList>
          </div>

          <TabPanels class="w-full aspect-w-1 aspect-h-1">
            <TabPanel v-for="image in product?.images?.carousel" :key="image.id">
              <img :src="image" class="w-full h-full object-center object-cover sm:rounded-lg" />
            </TabPanel>
          </TabPanels>
        </TabGroup>

        <!-- Product info -->
        <div class="mt-10 px-4 sm:px-0 sm:mt-16 lg:mt-0">
          <h1 class="text-3xl font-extrabold tracking-tight text-gray-900">{{ product?.name }}</h1>

          <div class="mt-3">
            <h2 class="sr-only">Product information</h2>
            <p class="text-3xl text-gray-900">{{ price }} {{ currency }}</p>
          </div>

          <!-- Reviews -->
          <!-- <div class="mt-3">
            <h3 class="sr-only">Reviews</h3>
            <div class="flex items-center">
              <div class="flex items-center">
                <StarIcon v-for="rating in [0, 1, 2, 3, 4]" :key="rating" :class="[product.rating > rating ? 'text-primary-500' : 'text-gray-300', 'h-5 w-5 flex-shrink-0']" aria-hidden="true" />
              </div>
              <p class="sr-only">{{ product.rating }} out of 5 stars</p>
            </div>
          </div> -->

          <div class="mt-6">
            <h3 class="sr-only">Description</h3>

            <div class="text-base text-gray-700 space-y-6" v-html="product?.description" />
          </div>

          <form class="mt-6">
        
            <div class="mt-10 flex sm:flex-col1">
              <button type="submit" class="max-w-xs flex-1 bg-primary-600 border border-transparent rounded-md py-3 px-8 flex items-center justify-center text-base font-medium text-white hover:bg-primary-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-50 focus:ring-primary-500 sm:w-full">Add to bag</button>
            </div>
          </form>

          <section aria-labelledby="details-heading" class="mt-12">
            <h2 id="details-heading" class="sr-only">Additional details</h2>

            <div class="border-t divide-y divide-gray-200">
              <!-- <Disclosure as="div" v-for="detail in product.details" :key="detail.name" v-slot="{ open }">
                <h3>
                  <DisclosureButton class="group relative w-full py-6 flex justify-between items-center text-left">
                    <span :class="[open ? 'text-primary-600' : 'text-gray-900', 'text-sm font-medium']">
                      {{ detail.name }}
                    </span>
                    <span class="ml-6 flex items-center">
                      <PlusSmIcon v-if="!open" class="block h-6 w-6 text-gray-400 group-hover:text-gray-500" aria-hidden="true" />
                      <MinusSmIcon v-else class="block h-6 w-6 text-primary-400 group-hover:text-primary-500" aria-hidden="true" />
                    </span>
                  </DisclosureButton>
                </h3>
                <DisclosurePanel as="div" class="pb-6 prose prose-sm">
                  <ul role="list">
                    <li v-for="item in detail.items" :key="item">{{ item }}</li>
                  </ul>
                </DisclosurePanel>
              </Disclosure> -->
            </div>
          </section>
        </div>
      </div>
    </div>
  </div>
  </Navbar>

  <Footer />
  </RootComponent>
</template>


<script setup>
import {
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
  Tab,
  TabGroup,
  TabList,
  TabPanel,
  TabPanels,
} from '@headlessui/vue'
import { MinusSmIcon, PlusSmIcon } from '@heroicons/vue/outline'

import { useRoute, useRouter } from 'vue-router'
import { useProductsStore } from "@/stores/products"
import { storeToRefs } from 'pinia'

const route = useRoute()
const router = useRouter()

const productsStore = useProductsStore()
const { products } = storeToRefs(productsStore)

const product = computed( () => products?.value?.find(x => x.slug === route.params.id) )
const currency = computed( () => Object.keys(product.value?.pricing?.currencyPrices ?? {})[0] )
const price = computed( () => product.value?.pricing?.currencyPrices[currency.value] )

</script>