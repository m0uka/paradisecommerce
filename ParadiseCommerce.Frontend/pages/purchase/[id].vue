<template>
    <RootComponent>
        <Navbar />
        <!-- Background color split screen for large screens -->
        <div class="hidden lg:block fixed top-0 left-0 w-1/2 h-full bg-white" aria-hidden="true" />
        <div
            class="hidden lg:block fixed top-0 right-0 w-1/2 h-full bg-primary-900"
            aria-hidden="true"
        />

        <div
            class="relative grid grid-cols-1 gap-x-16 max-w-7xl mx-auto lg:px-8 lg:grid-cols-2 lg:pt-16"
        >
            <h1 class="sr-only">Checkout</h1>

            <section
                aria-labelledby="summary-heading"
                class="bg-primary-900 text-primary-300 py-12 md:px-10 lg:max-w-lg lg:w-full lg:mx-auto lg:px-0 lg:pt-0 lg:pb-24 lg:bg-transparent lg:row-start-1 lg:col-start-2"
            >
                <div class="max-w-2xl mx-auto px-4 lg:max-w-none lg:px-0">
                    <h2 id="summary-heading" class="sr-only">Order summary</h2>

                    <dl>
                        <dt class="text-sm font-medium">Amount due</dt>
                        <dd class="mt-1 text-3xl font-extrabold text-white">{{ subtotal }}</dd>
                    </dl>

                    <ul
                        role="list"
                        class="text-sm font-medium divide-y divide-white divide-opacity-10"
                    >
                        <li
                            v-for="product in productsList"
                            :key="product?.id"
                            class="flex items-start py-6 space-x-4"
                        >
                            <img
                                :src="product?.images?.listingSmall"
                                class="flex-none w-20 h-20 rounded-md object-center object-cover"
                            />
                            <div class="flex-auto space-y-1">
                                <h3 class="text-white">{{ product?.name }}</h3>
                                <p>{{ getProductCategory(product)?.name }}</p>
                                <p>{{ product?.shortDescription }}</p>
                            </div>
                            <p
                                class="flex-none text-base font-medium text-white"
                            >{{ getProductPrice(product) }}</p>
                        </li>
                    </ul>

                    <dl
                        class="text-sm font-medium space-y-6 border-t border-white border-opacity-10 pt-6"
                    >
                        <div class="flex items-center justify-between">
                            <dt>Subtotal</dt>
                            <dd>{{ subtotal }}</dd>
                        </div>

                        <div
                            class="flex items-center justify-between border-t border-white border-opacity-10 text-white pt-6"
                        >
                            <dt class="text-base">Total</dt>
                            <dd class="text-base">{{ subtotal }}</dd>
                        </div>
                    </dl>
                </div>
            </section>

            <section
                aria-labelledby="payment-and-shipping-heading"
                class="py-16 lg:max-w-lg lg:w-full lg:mx-auto lg:pt-0 lg:pb-24 lg:row-start-1 lg:col-start-1"
            >
                <h2 id="payment-and-shipping-heading" class="sr-only">Payment and shipping details</h2>

                    <div class="max-w-2xl mx-auto px-4 lg:max-w-none lg:px-0">
                        <div v-if="!authStore?.loggedIn">
                            <h3
                                id="contact-info-heading"
                                class="text-lg font-medium text-gray-900"
                            >Contact information</h3>

                            <div class="mt-6">
                                <label
                                    for="email-address"
                                    class="block text-sm font-medium text-gray-700"
                                >Email address</label>
                                <div class="mt-1">
                                    <input
                                        type="email"
                                        id="email-address"
                                        name="email-address"
                                        autocomplete="email"
                                        class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                    />
                                </div>
                            </div>
                        </div>

                        <div class="mt-10">
                            <h3 class="text-lg font-medium text-gray-900">Payment method</h3>

                            <RadioGroup v-model="selectedPaymentMethod">
                                <div
                                    class="mt-4 grid grid-cols-1 gap-y-6 sm:grid-cols-2 sm:gap-x-4"
                                >
                                    <RadioGroupOption
                                        as="template"
                                        v-for="paymentMethod in paymentMethods"
                                        :key="paymentMethod.id"
                                        :value="paymentMethod"
                                        v-slot="{ checked, active }"
                                    >
                                        <div
                                            :class="[checked ? 'border-transparent' : 'border-gray-300', active ? 'ring-2 ring-primary-500' : '', 'relative bg-white border rounded-lg shadow-sm p-4 flex cursor-pointer focus:outline-none']"
                                        >
                                            <div class="flex-1 flex">
                                                <div class="flex flex-col">
                                                    <RadioGroupLabel
                                                        as="span"
                                                        class="block text-sm font-medium text-gray-900"
                                                    >{{ paymentMethod.title }}</RadioGroupLabel>
                                                    <RadioGroupDescription
                                                        as="span"
                                                        class="mt-1 flex items-center text-sm text-gray-500"
                                                    >{{ paymentMethod.description }}</RadioGroupDescription>
                                                </div>
                                            </div>
                                            <CheckCircleIcon
                                                v-if="checked"
                                                class="h-5 w-5 text-primary-600"
                                                aria-hidden="true"
                                            />
                                            <div
                                                :class="[active ? 'border' : 'border-2', checked ? 'border-primary-500' : 'border-transparent', 'absolute -inset-px rounded-lg pointer-events-none']"
                                                aria-hidden="true"
                                            />
                                        </div>
                                    </RadioGroupOption>
                                </div>
                            </RadioGroup>
                        </div>

                        <div class="mt-10" v-if="productsList?.some(product => product?.productType == 'Physical')">
                            <h3 class="text-lg font-medium text-gray-900">Shipping address</h3>

                            <div class="mt-6 grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-3">
                                <div class="sm:col-span-3">
                                    <label
                                        for="address"
                                        class="block text-sm font-medium text-gray-700"
                                    >Address</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="address"
                                            name="address"
                                            autocomplete="street-address"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                        />
                                    </div>
                                </div>

                                <div>
                                    <label
                                        for="city"
                                        class="block text-sm font-medium text-gray-700"
                                    >City</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="city"
                                            name="city"
                                            autocomplete="address-level2"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                        />
                                    </div>
                                </div>

                                <div>
                                    <label
                                        for="region"
                                        class="block text-sm font-medium text-gray-700"
                                    >State / Province</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="region"
                                            name="region"
                                            autocomplete="address-level1"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                        />
                                    </div>
                                </div>

                                <div>
                                    <label
                                        for="postal-code"
                                        class="block text-sm font-medium text-gray-700"
                                    >Postal code</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="postal-code"
                                            name="postal-code"
                                            autocomplete="postal-code"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                        />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="mt-10 flex justify-end pt-6 border-t border-gray-200">
                            <Button :loading="loading" @click="finishPayment" color="bg-primary-600 hover:bg-primary-700 text-white">Pay now</Button>
                        </div>
                    </div>
            </section>
        </div>
    </RootComponent>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router'
import { useProductsStore } from "@/stores/products"
import { useAuthStore } from '@/stores/auth'
import { storeToRefs } from 'pinia'

import { CheckCircleIcon, TrashIcon } from '@heroicons/vue/solid'
import { RadioGroup, RadioGroupDescription, RadioGroupLabel, RadioGroupOption } from '@headlessui/vue'

import orderAPI from '@/api/ordering'
import billingAPI from '@/api/billing'

const paymentMethods = [
    { id: 'stripe', title: 'Card', description: 'Handled via Stripe' },
    //   { id: 'paypal', title: 'PayPal', description: 'Handled via PayPal' },
]
const selectedPaymentMethod = ref(paymentMethods[0])

const route = useRoute()
let loading = ref(false)

const productsStore = useProductsStore()
const authStore = useAuthStore()

const { products, categories } = storeToRefs(productsStore)

const product = computed(() => products?.value?.find(x => x.slug === route.params.id))
const category = computed(() => categories?.value?.find(x => x.id === product?.value?.groupId))

const currency = computed(() => Object.keys(product.value?.pricing?.currencyPrices ?? {})[0])
const price = computed(() => product.value?.pricing?.currencyPrices[currency.value])

const productsList = computed(() => [
    product?.value
])

// if multiple products have different currencies, this will fuck up, lol
const subtotal = computed(() => formatCurrency(productsList?.value?.map(product => getProductPrice(product, true)).reduce((prev, curr) => prev + curr), currency.value))

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

function getProductCategory(product) {
    return categories?.value?.find(x => x.id == product?.groupId)
}

let poller

async function finishPayment() {
    loading.value = true

    let productInfos = []
    productsList.value.forEach(product => {
        productInfos.push({productId: product.id})
    })


    const orderPlaceModel = {
        productInfos: productInfos,
        currency: currency.value,
        paymentMethod: selectedPaymentMethod.value.id,
        // TODO: shippingAddress
    }

    const response = await orderAPI.placeOrder(orderPlaceModel)
    const data = await response.text()

    if (response.status == 200) {
        // poll billing api until we get a payment url
        poller = setInterval( () => pollForPayment(data), 500 )
    } else {
        // todo: maybe better handling of errors later
        alert(data)
    }
    
    loading.value = false
}

async function pollForPayment(orderId) {
    const response = await billingAPI.getByOrder(orderId)
    const data = await response.json()

    if (data.paymentLink) {
        // we are done here

        window.location.href = data.paymentLink
        clearInterval(poller)
    }
}

</script>