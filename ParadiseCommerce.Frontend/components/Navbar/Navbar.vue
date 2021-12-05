<template>
  <div class="bg-white">
    <!-- Mobile menu -->
    <!-- <MobileNavbar :open="menuOpen" :navigation="navigation" /> -->

    <header class="relative overflow-hidden">
      <!-- Top navigation -->
      <nav aria-label="Top" class="relative z-20 bg-white bg-opacity-90 backdrop-filter backdrop-blur-xl ">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 border-b border-gray-200">
          <div class="h-16 flex items-center">
            <button type="button" class="bg-white p-2 rounded-md text-gray-400 lg:hidden" @click="menuOpen = true">
              <span class="sr-only">Open menu</span>
              <MenuIcon class="h-6 w-6" aria-hidden="true" />
            </button>

            <!-- Logo -->
            <div class="ml-4 flex lg:ml-0">
              <a href="/">
                <img class="h-8 w-auto" :src="storefront.logo" alt="" />
              </a>
            </div>

            <div class="hidden lg:ml-8 lg:block lg:self-stretch">
              <div class="h-full flex space-x-8">
              <NuxtLink v-for="category in productStore?.navbarCategories" :key="category.name" :to="`/category/${category.slug}`" class="flex items-center text-sm font-medium text-gray-700 hover:text-gray-800">{{ category.name }}</NuxtLink>
              </div>
            </div>

            <!-- Flyout menus -->
            <!-- <PopoverGroup class="hidden lg:ml-8 lg:block lg:self-stretch">
              <div class="h-full flex space-x-8">
                <Popover v-for="category in navigation.categories" :key="category.name" class="flex" v-slot="{ open }">
                  <div class="relative flex">
                    <PopoverButton :class="[open ? 'border-primary-600 text-primary-600' : 'border-transparent text-gray-700 hover:text-gray-800', 'relative z-10 flex items-center transition-colors ease-out duration-200 text-sm font-medium border-b-2 -mb-px pt-px']">
                      {{ category.name }}
                    </PopoverButton>
                  </div>

                  <transition enter-active-class="transition ease-out duration-200" enter-from-class="opacity-0" enter-to-class="opacity-100" leave-active-class="transition ease-in duration-150" leave-from-class="opacity-100" leave-to-class="opacity-0">
                    <PopoverPanel class="absolute top-full inset-x-0 bg-white text-sm text-gray-500">
                      
                      <div class="absolute inset-0 top-1/2 bg-white shadow" aria-hidden="true" />
                      <div class="absolute inset-0 top-0 h-px max-w-7xl mx-auto px-8" aria-hidden="true">
                        <div :class="[open ? 'bg-gray-200' : 'bg-transparent', 'w-full h-px transition-colors ease-out duration-200']" />
                      </div>
                      

                      <div class="relative">
                        <div class="max-w-7xl mx-auto px-8">
                          <div class="grid grid-cols-2 gap-y-10 gap-x-8 py-16">
                            <div class="col-start-2 grid grid-cols-2 gap-x-8">
                              <div v-for="item in category.featured" :key="item.name" class="group relative text-base sm:text-sm">
                                <div class="aspect-w-1 aspect-h-1 rounded-lg bg-gray-100 overflow-hidden group-hover:opacity-75">
                                  <img :src="item.imageSrc" :alt="item.imageAlt" class="object-center object-cover" />
                                </div>
                                <a :href="item.href" class="mt-6 block font-medium text-gray-900">
                                  <span class="absolute z-10 inset-0" aria-hidden="true" />
                                  {{ item.name }}
                                </a>
                                <p aria-hidden="true" class="mt-1">Shop now</p>
                              </div>
                            </div>
                            <div class="row-start-1 grid grid-cols-3 gap-y-10 gap-x-8 text-sm">
                              <div v-for="section in category.sections" :key="section.name">
                                <p :id="`${section.name}-heading`" class="font-medium text-gray-900">
                                  {{ section.name }}
                                </p>
                                <ul role="list" :aria-labelledby="`${section.name}-heading`" class="mt-6 space-y-6 sm:mt-4 sm:space-y-4">
                                  <li v-for="item in section.items" :key="item.name" class="flex">
                                    <a :href="item.href" class="hover:text-gray-800">
                                      {{ item.name }}
                                    </a>
                                  </li>
                                </ul>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </PopoverPanel>
                  </transition>
                </Popover>

                <a v-for="page in navigation.pages" :key="page.name" :href="page.href" class="flex items-center text-sm font-medium text-gray-700 hover:text-gray-800">{{ page.name }}</a>
              </div>
            </PopoverGroup> -->

            <div class="ml-auto flex items-center">
              <div v-if="!auth.loggedIn" class="hidden lg:flex lg:flex-1 lg:items-center lg:justify-end lg:space-x-6">
                <NuxtLink to="/account/signin" class="text-sm font-medium text-gray-700 hover:text-gray-800">
                  Sign in
                </NuxtLink>
                <span class="h-6 w-px bg-gray-200" aria-hidden="true" />
                <NuxtLink to="/account/signup" class="text-sm font-medium text-gray-700 hover:text-gray-800">
                  Create account
                </NuxtLink>
              </div>
              <div v-else>
                <NuxtLink class="text-gray-700 font-medium" to="/account/info">
                  {{ auth.name }}
                </NuxtLink>
              </div>

              <!-- <div class="hidden lg:ml-8 lg:flex">
                <a href="#" class="text-gray-700 hover:text-gray-800 flex items-center">
                  <img src="https://tailwindui.com/img/flags/flag-canada.svg" alt="" class="w-5 h-auto block flex-shrink-0" />
                  <span class="ml-3 block text-sm font-medium">
                    CAD
                  </span>
                  <span class="sr-only">, change currency</span>
                </a>
              </div> -->

              <!-- Search -->
              <!-- <div class="flex lg:ml-6">
                <a href="#" class="p-2 text-gray-400 hover:text-gray-500">
                  <span class="sr-only">Search</span>
                  <SearchIcon class="w-6 h-6" aria-hidden="true" />
                </a>
              </div> -->

              <!-- Cart -->
              <div class="ml-4 flow-root lg:ml-6">
                <a href="#" class="group -m-2 p-2 flex items-center">
                  <ShoppingBagIcon class="flex-shrink-0 h-6 w-6 text-gray-400 group-hover:text-gray-500" aria-hidden="true" />
                  <span class="ml-2 text-sm font-medium text-gray-700 group-hover:text-gray-800">0</span>
                  <span class="sr-only">items in cart, view bag</span>
                </a>
              </div>
            </div>
          </div>
        </div>
      </nav>

      <slot />

    </header>

    
  </div>
</template>

<script>
import { useStorefrontStore } from '@/stores/storefront'
import { useAuthStore } from '@/stores/auth'
import { useProductsStore } from '@/stores/products'

import { ref } from 'vue'
import {
  Dialog,
  DialogOverlay,
  Popover,
  PopoverButton,
  PopoverGroup,
  PopoverPanel,
  Tab,
  TabGroup,
  TabList,
  TabPanel,
  TabPanels,
  TransitionChild,
  TransitionRoot,
} from '@headlessui/vue'
import { MenuIcon, SearchIcon, ShoppingBagIcon, XIcon } from '@heroicons/vue/outline'


export default {
  components: {
    Dialog,
    DialogOverlay,
    Popover,
    PopoverButton,
    PopoverGroup,
    PopoverPanel,
    Tab,
    TabGroup,
    TabList,
    TabPanel,
    TabPanels,
    TransitionChild,
    TransitionRoot,
    MenuIcon,
    SearchIcon,
    ShoppingBagIcon,
    XIcon,
  },
  setup() {
    const menuOpen = ref(false)

    const storefront = useStorefrontStore()
    const productStore = useProductsStore()
    const auth = useAuthStore()

    return {
      menuOpen,
      auth,
      productStore,
      storefront
    }
  },
}
</script>