<template>
    <RootComponent>
        <Navbar>

  <div class="bg-white">
    <div class="max-w-2xl mx-auto py-16 px-4 sm:py-24 sm:px-6 lg:max-w-7xl lg:px-8">

      <Breadcrumb :pages="breadcrumbPages" class="-mt-14 mb-12" />

      <div class="lg:grid lg:grid-cols-2 lg:gap-x-8 lg:items-start">
      
        <!-- Image gallery -->
        <TabGroup v-if="carousel" :defaultIndex="0" as="div" class="flex flex-col-reverse">
          <!-- Image selector -->
          <div class="hidden mt-6 w-full max-w-2xl mx-auto sm:block lg:max-w-none">
            <TabList class="grid grid-cols-4 gap-6">
              <Tab v-for="image in carousel" :key="image" class="relative h-24 bg-white rounded-md flex items-center justify-center text-sm font-medium uppercase text-gray-900 cursor-pointer hover:bg-gray-50 focus:outline-none focus:ring focus:ring-offset-4 focus:ring-opacity-50" v-slot="{ selected }">
                <span class="absolute inset-0 rounded-md overflow-hidden">
                  <img :src="image" alt="" class="w-full h-full object-center object-cover" />
                </span>
                <span :class="[selected ? 'ring-primary-500' : 'ring-transparent', 'absolute inset-0 rounded-md ring-2 ring-offset-2 pointer-events-none']" aria-hidden="true" />
              </Tab>
            </TabList>
          </div>

          <TabPanels class="w-full aspect-w-1 aspect-h-1">
            <TabPanel v-for="image in carousel" :key="image">
              <img :src="image" class="w-full h-full object-center object-cover sm:rounded-lg" />
            </TabPanel>
          </TabPanels>
        </TabGroup>

        <!-- Product info -->
        <div class="mt-10 px-4 sm:px-0 sm:mt-16 lg:mt-0">
          <h1 class="text-3xl font-extrabold tracking-tight text-gray-900">{{ product?.name }}</h1>

          <div class="mt-3">
            <h2 class="sr-only">Product information</h2>
            <p class="text-3xl text-gray-900">{{ formatCurrency(price, currency) }}</p>
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

            <div class="prose text-base text-gray-700 space-y-6" v-html="product?.description" />
          </div>

          <form class="mt-6">
        
            <div class="mt-10 flex sm:flex-col1">
              <NuxtLink :to="`/purchase/${product?.slug}`" type="submit" class="max-w-xs flex-1 bg-primary-600 border border-transparent rounded-md py-3 px-8 flex items-center justify-center text-base font-medium text-white hover:bg-primary-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-50 focus:ring-primary-500 sm:w-full">Purchase</NuxtLink>
            </div>
          </form>

          <section aria-labelledby="details-heading" class="mt-12">
            <h2 id="details-heading" class="sr-only">Additional details</h2>

            <div class="border-t divide-y divide-gray-200">
              <Disclosure as="div" v-for="detail in details" :key="detail.name" v-slot="{ open }">
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
              </Disclosure>
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
import { ref } from 'vue'
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

const productsStore = useProductsStore()
const { products, categories } = storeToRefs(productsStore)

const product = computed( () => products?.value?.find(x => x.slug === route.params.id) )
const category = computed( () => categories?.value?.find(x => x.id === product?.value?.groupId) )

const currency = computed( () => Object.keys(product.value?.pricing?.currencyPrices ?? {})[0] )
const price = computed( () => product.value?.pricing?.currencyPrices[currency.value] )

const carousel = computed( () => product?.value?.images?.carousel ?? null )

const breadcrumbPages = computed( () => [
  { name: (category?.value?.name ?? 'Category'), href: (`/category/${category?.value?.slug}`) },
  { name: (product?.value?.name ?? 'Product'), href: (`#`) }
])

const details = [
  { name: 'Payment Methods', items: [ 'We support all major credit card companies, including but not limited to:', 'Visa', 'Mastercard', 'American Express', 'Discover & Diners', 'China UnionPay (CUP)', 'Japan Credit Bureau (JCB)' ] },
  { name: 'Returns', items: [ 'We offer a two-year warranty for physical products', '14-day withdrawal period for all products or services' ] }
]

function formatCurrency(value, currency) {
  if (typeof value !== "number") {
    return value;
  }

  var formatter = new Intl.NumberFormat(navigator.language, {
    style: 'currency',
    currency: currency
  })

  return formatter.format(value)
}

</script>