<template>
  <RootComponent>
  <Navbar>
  <div class="bg-gray-50">
    <div class="max-w-2xl mx-auto pt-16 sm:py-24 sm:px-6 lg:max-w-7xl lg:px-8">

      <div v-if="route.query.result === 'success'" class="mb-8 -mt-12">
        <Alert icon="CheckCircleIcon" color="green" title="Payment successful" description="We have successfully received your payment for this order." />
      </div>

      <div v-if="route.query.result === 'fail'" class="mb-8 -mt-12">
        <Alert icon="ExclamationCircleIcon" color="red" title="Payment unsuccessful" description="We couldn't process your payment for this order. Please try to pay the order again by clicking the 'Pay Now' button below." />
      </div>

      <div class="px-4 space-y-2 sm:px-0 sm:flex sm:items-baseline sm:justify-between sm:space-y-0">
        <div class="flex sm:items-baseline sm:space-x-4">
          <h1 class="text-2xl font-extrabold tracking-tight text-gray-900 sm:text-3xl">Order #{{order?.id}}</h1>
        </div>
        <p class="text-sm text-gray-600">Order placed <time datetime="2021-03-22" class="font-medium text-gray-900">{{ formatDate(order?.createdAt) }}</time></p>
      </div>

    

      <!-- Products -->
      <div class="mt-6">
        <div class="space-y-8">
          <div v-if="products" v-for="product in products" :key="product?.id" :class="['bg-white border-t border-b border-gray-200 shadow-sm sm:border sm:rounded-lg', loading ? 'animate-pulse bg-gray-300' : '']">
            <div class="py-6 px-4 sm:px-6 lg:grid lg:grid-cols-12 lg:gap-x-8 lg:p-8">
              <div class="sm:flex lg:col-span-7">
                <div class="flex-shrink-0 w-full aspect-w-1 aspect-h-1 rounded-lg overflow-hidden sm:aspect-none sm:w-40 sm:h-40">
                  <img v-if="!loading" :src="product?.images?.listing" class="w-full h-full object-center object-cover sm:w-full sm:h-full" />
                  <div v-else class="w-full h-full bg-gray-400 min-h-full" />
                </div>

                <div class="mt-6 sm:mt-0 sm:ml-6">
                  <h3 class="text-base font-medium text-gray-900">
                    <NuxtLink v-if="!loading" :to="`/product/${product?.slug}`">{{ product?.name }}</NuxtLink>
                    <div v-else class="animate-pulse w-48 h-7 bg-gray-400 rounded-md"></div>
                  </h3>

                  <p v-if="!loading" class="mt-2 text-sm font-medium text-gray-900">{{ getProductPrice(product) }}</p>
                  <div v-else class="animate-pulse w-10 mt-2 h-6 bg-gray-400 rounded-md"></div>

                  <p v-if="!loading" class="mt-3 text-sm text-gray-500">
                    {{ product?.shortDescription }}
                  </p>
                  <div class="mt-4" v-else>
                      <div class="animate-pulse w-80 mt-2 h-4 bg-gray-400 rounded-md" />
                      <div class="animate-pulse w-72 mt-2 h-4 bg-gray-400 rounded-md" />
                      <div class="animate-pulse w-48 mt-2 h-4 bg-gray-400 rounded-md" />
                  </div>

                </div>
              </div>

              <div class="mt-6 lg:mt-0 lg:col-span-5">
                <dl v-if="product?.productType === 'Physical'" class="grid grid-cols-2 gap-x-6 text-sm">
                  <div>
                    <dt class="font-medium text-gray-900">Delivery address</dt>
                    <dd class="mt-3 text-gray-500">
                      <span class="block">{{ product?.shippingAddress?.streetAddress }}</span>
                      <span class="block">{{ product?.shippingAddress?.state }}</span>
                      <span class="block">{{ product?.shippingAddress?.zipCode }}</span>
                    </dd>
                  </div>
                  
                </dl>
              </div>
            </div>

            <div class="border-t border-gray-200 py-6 px-4 sm:px-6 lg:p-8">
              <h4 class="sr-only">Status</h4>
              <p class="text-sm font-medium text-gray-900 flex items-center">
                {{ humanReadableStatus(order.status) }}
                <div class="ml-4" v-if="order.status === 'WaitingForPayment'">
                  <Button @click="pay()">
                    Pay here
                  </Button>
                </div>
              </p>
              <div class="mt-6" aria-hidden="true">
                <div class="bg-gray-200 rounded-full overflow-hidden">
                  <div class="h-2 bg-primary-600 rounded-full" :style="{ width: `calc((${getOrderStep(order.status)} * 2 + 1) / 8 * 100%)` }" />
                </div>
                <div class="hidden sm:grid grid-cols-4 text-sm font-medium text-gray-600 mt-6">
                  <div class="text-primary-600">Order placed</div>
                  <div :class="[getOrderStep(order.status) > 0 ? 'text-primary-600' : '', 'text-center']">Payment received</div>
                  <div :class="[getOrderStep(order.status) > 1 ? 'text-primary-600' : '', 'text-center']">{{ product?.productType === 'Physical' ? 'Shipped' : 'Order handled' }}</div>
                  <div :class="[getOrderStep(order.status) > 2 ? 'text-primary-600' : '', 'text-right']">{{ product?.productType === 'Physical' ? 'Delivered' : order.status === 'Cancelled' || 'Active' }}</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Billing -->
      <div class="mt-16">
        <h2 class="sr-only">Billing Summary</h2>

        <div class="bg-gray-100 py-6 px-4 sm:px-6 sm:rounded-lg lg:px-8 lg:py-8 lg:grid lg:grid-cols-12 lg:gap-x-8">
          <dl class="grid grid-cols-2 gap-6 text-sm sm:grid-cols-2 md:gap-x-8 lg:col-span-7">

          </dl>

          <dl class="mt-8 divide-y divide-gray-200 text-sm lg:mt-0 lg:col-span-5">
            <div class="pb-4 flex items-center justify-between">
              <dt class="text-gray-600">Subtotal</dt>
              <dd class="font-medium text-gray-900">{{ formatCurrency(order?.amount, order?.currency) }}</dd>
            </div>
            <div class="pt-4 flex items-center justify-between">
              <dt class="font-medium text-gray-900">Order total</dt>
              <dd class="font-medium text-primary-600">{{ formatCurrency(order?.amount, order?.currency) }}</dd>
            </div>
          </dl>
        </div>
      </div>
    </div>
  </div>
  </Navbar>

  <Footer />
  </RootComponent>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router'
import { useOrdersStore } from '@/stores/orders'
import { useAuthStore } from '@/stores/auth'
import { useProductsStore } from '@/stores/products'
import billingAPI from '@/api/billing'

const route = useRoute()
const router = useRouter()

const loading = ref(true)

const ordersStore = useOrdersStore()
const productsStore = useProductsStore()
const authStore = useAuthStore()

let invoice = ref({})

const order = computed( () => ordersStore?.orders?.find(x => x.id == route.params.id) )
const products = computed( () => getOrderProducts(order.value) )

authStore.$subscribe( async (mutation, state) => {
  if (mutation.events.key == 'loaded') {
    await ordersStore.fetch()
  }
})

ordersStore.$subscribe( async (mutation, state) => {
  if (mutation.events.key == 'orders') {
    loading.value = false

    const response = await billingAPI.getByOrder(order.value.id)
    const data = await response.json()

    invoice.value = data
    console.log(invoice.value)
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

// const products = [
//   {
//     id: 1,
//     name: 'Nomad Tumbler',
//     description:
//       'This durable and portable insulated tumbler will keep your beverage at the perfect temperature during your next adventure.',
//     href: '#',
//     price: '35.00',
//     status: 'Preparing to ship',
//     step: 1,
//     date: 'March 24, 2021',
//     datetime: '2021-03-24',
//     address: ['Floyd Miles', '7363 Cynthia Pass', 'Toronto, ON N3Y 4H8'],
//     email: 'f•••@example.com',
//     phone: '1•••••••••40',
//     imageSrc: 'https://tailwindui.com/img/ecommerce-images/confirmation-page-03-product-01.jpg',
//     imageAlt: 'Insulated bottle with white base and black snap lid.',
//   },
//   {
//     id: 2,
//     name: 'Minimalist Wristwatch',
//     description: 'This contemporary wristwatch has a clean, minimalist look and high quality components.',
//     href: '#',
//     price: '149.00',
//     status: 'Shipped',
//     step: 0,
//     date: 'March 23, 2021',
//     datetime: '2021-03-23',
//     address: ['Floyd Miles', '7363 Cynthia Pass', 'Toronto, ON N3Y 4H8'],
//     email: 'f•••@example.com',
//     phone: '1•••••••••40',
//     imageSrc: 'https://tailwindui.com/img/ecommerce-images/confirmation-page-03-product-02.jpg',
//     imageAlt:
//       'Arm modeling wristwatch with black leather band, white watch face, thin watch hands, and fine time markings.',
//   },
// ]

</script>