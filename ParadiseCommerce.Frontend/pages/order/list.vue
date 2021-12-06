<template>
  <RootComponent>
  <Navbar>
    <div class="bg-white min-h-screen">
    <div class="py-16 sm:py-24">
      <div class="max-w-7xl mx-auto sm:px-2 lg:px-8">
        <div class="max-w-2xl mx-auto px-4 lg:max-w-4xl lg:px-0">
          <h1 class="text-2xl font-extrabold tracking-tight text-gray-900 sm:text-3xl">Order history</h1>
          <p class="mt-2 text-sm text-gray-500">Check the status of recent orders, manage returns, and discover similar products.</p>
        </div>
      </div>

      <div class="mt-16">
        <h2 class="sr-only">Recent orders</h2>
        <div class="max-w-7xl mx-auto sm:px-2 lg:px-8">
          <div class="max-w-2xl mx-auto space-y-8 sm:px-4 lg:max-w-4xl lg:px-0">
            <div v-for="order in sortedOrders" :key="order.id" class="bg-white border-t border-b border-gray-200 shadow-sm sm:rounded-lg sm:border">
              <h3 class="sr-only">
                Order placed on <time :datetime="order.createdDatetime">{{ order.createdAt }}</time>
              </h3>

              <div class="flex items-center p-4 border-b border-gray-200 sm:p-6 sm:grid sm:grid-cols-4 sm:gap-x-6">
                <dl class="flex-1 grid grid-cols-2 gap-x-6 text-sm sm:col-span-3 sm:grid-cols-3 lg:col-span-3">
                  <div>
                    <dt class="font-medium text-gray-900">Order ID</dt>
                    <dd class="mt-1 text-gray-500">
                      {{ order.id }}
                    </dd>
                  </div>
                  <div class="hidden sm:block">
                    <dt class="font-medium text-gray-900">Date placed</dt>
                    <dd class="mt-1 text-gray-500">
                      <time :datetime="order.createdAt">{{ formatDate(order.createdAt) }}</time>
                    </dd>
                  </div>
                  <div>
                    <dt class="font-medium text-gray-900">Total amount</dt>
                    <dd class="mt-1 font-medium text-gray-900">
                      {{ formatCurrency(order.amount, order.currency) }}
                    </dd>
                  </div>
                </dl>

                <Menu as="div" class="relative flex justify-end lg:hidden">
                  <div class="flex items-center">
                    <MenuButton class="-m-2 p-2 flex items-center text-gray-400 hover:text-gray-500">
                      <span class="sr-only">Options for order {{ order.number }}</span>
                      <DotsVerticalIcon class="w-6 h-6" aria-hidden="true" />
                    </MenuButton>
                  </div>

                  <transition enter-active-class="transition ease-out duration-100" enter-from-class="transform opacity-0 scale-95" enter-to-class="transform opacity-100 scale-100" leave-active-class="transition ease-in duration-75" leave-from-class="transform opacity-100 scale-100" leave-to-class="transform opacity-0 scale-95">
                    <MenuItems class="origin-bottom-right absolute right-0 mt-2 w-40 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none">
                      <div class="py-1">
                        <MenuItem v-slot="{ active }">
                          <a :href="order.href" :class="[active ? 'bg-gray-100 text-gray-900' : 'text-gray-700', 'block px-4 py-2 text-sm']">
                            View
                          </a>
                        </MenuItem>
                      </div>
                    </MenuItems>
                  </transition>
                </Menu>

                <div class="hidden lg:col-span-1 lg:flex lg:items-center lg:justify-end lg:space-x-4">
                  <NuxtLink :to="`/order/${order.id}`" class="flex items-center justify-center bg-white py-2 px-2.5 border border-gray-300 rounded-md shadow-sm text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500">
                    <span>View Order</span>
                  </NuxtLink>
                </div>
              </div>

              <!-- Products -->
              <h4 class="sr-only">Items</h4>
              <ul role="list" class="divide-y divide-gray-200">
                <li v-for="product in getOrderProducts(order)" :key="product?.id" class="p-4 sm:p-6">
                  <div class="flex items-center sm:items-start">
                    <div class="flex-shrink-0 w-20 h-20 bg-gray-200 rounded-lg overflow-hidden sm:w-40 sm:h-40">
                      <img :src="product?.images?.listing" class="w-full h-full object-center object-cover" />
                    </div>
                    <div class="flex-1 ml-6 text-sm">
                      <div class="font-medium text-gray-900 sm:flex sm:justify-between">
                        <h5>
                          {{ product?.name }}
                        </h5>
                        <p class="mt-2 sm:mt-0">
                          {{ getProductPrice(product) }}
                        </p>
                      </div>
                      <p class="hidden text-gray-500 sm:block sm:mt-2">
                        {{ product?.shortDescription }}
                      </p>
                    </div>
                  </div>

                  <div class="mt-6 sm:flex sm:justify-between">
                    <div class="flex items-center">
                      <CheckCircleIcon class="w-5 h-5 text-green-500" aria-hidden="true" />
                      <p class="ml-2 text-sm font-medium text-gray-500">
                        {{ humanReadableStatus(order.status) }}
                      </p>
                    </div>

                    <div class="mt-6 border-t border-gray-200 pt-4 flex items-center space-x-4 divide-x divide-gray-200 text-sm font-medium sm:mt-0 sm:ml-4 sm:border-none sm:pt-0">
                      <div class="flex-1 flex justify-center">
                        <NuxtLink :to="`/product/${product?.slug}`" class="text-primary-600 whitespace-nowrap hover:text-primary-500">View product</NuxtLink>
                      </div>
                      <div class="flex-1 pl-4 flex justify-center">
                        <NuxtLink :to="`/purchase/${product?.slug}`" class="text-primary-600 whitespace-nowrap hover:text-primary-500">Buy again</NuxtLink>
                      </div>
                    </div>
                  </div>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
    </div>
    </Navbar>

    <Footer />
    </RootComponent>
</template>

<script setup>
import { Menu, MenuButton, MenuItem, MenuItems } from '@headlessui/vue'
import { DotsVerticalIcon } from '@heroicons/vue/outline'
import { CheckCircleIcon } from '@heroicons/vue/solid'

import { useRoute, useRouter } from 'vue-router'
import { useOrdersStore } from '@/stores/orders'
import { useAuthStore } from '@/stores/auth'
import { useProductsStore } from '@/stores/products'
import billingAPI from '@/api/billing'
import { storeToRefs } from 'pinia'

const route = useRoute()
const router = useRouter()

const ordersStore = useOrdersStore()
const productsStore = useProductsStore()
const authStore = useAuthStore()

const { orders } = storeToRefs(ordersStore)

const sortedOrders = computed( () => orders?.value?.sort( (a, b) => new Date(b.createdAt) - new Date(a.createdAt) ) )

ordersStore.fetch()
authStore.$subscribe( async (mutation, state) => {
  if (mutation?.events?.key == 'loaded') {
    await ordersStore.fetch()
  }
})

function getOrderProducts(order) {
  let products = []
  order?.products?.forEach(productOrder => {
    // again, later an API request might be better here
    const product = productsStore?.products?.find(x => x.id == productOrder.productId)
    products.push(product)
  })
  return products
}

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

function getProductPrice(product, noFormat) {
    const currency = Object.keys(product?.pricing?.currencyPrices ?? {})[0]
    const price = product?.pricing?.currencyPrices[currency]

    return !noFormat ? formatCurrency(price, currency) : price
}

function getOrderStep(status) {
  switch (status) {
    case "WaitingForPayment":
      return 0

    case "ReadyToShip":
      return 2

    case "Shipped":
      return 3

    case "Delivered":
      return 4

    case "Active":
      return 4

    case "Cancelled":
      return 4
  }
}

function humanReadableStatus(status) {
  if (status == 'WaitingForPayment') return 'Waiting for payment'
  if (status == 'ReadyToShip') return 'Ready to ship' 
  return status
}

function pay() {
  window.location.href = invoice?.value?.paymentLink
}

function formatDate(date) {
  return new Date(date)?.toLocaleDateString(navigator.language)
}
</script>