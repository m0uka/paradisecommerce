<template>
    <Disclosure as="header" class="bg-white shadow" v-slot="{ open }">
      <div class="max-w-7xl mx-auto px-2 sm:px-4 lg:divide-y lg:divide-gray-200 lg:px-8">
        <div class="relative h-16 flex justify-between">
          <div class="relative z-10 px-2 flex lg:px-0">
            <div class="flex-shrink-0 flex items-center">
              <img class="block h-8 w-auto" :src="storefront.logo" />
            </div>
          </div>

          <!-- <div class="relative z-0 flex-1 px-2 flex items-center justify-center sm:absolute sm:inset-0">
            <div class="max-w-xs w-full">
              <label for="search" class="sr-only">Search</label>
              <div class="relative">
                <div class="pointer-events-none absolute inset-y-0 left-0 pl-3 flex items-center">
                  <SearchIcon class="flex-shrink-0 h-5 w-5 text-gray-400" aria-hidden="true" />
                </div>
                <input name="search" id="search" class="block w-full bg-white border border-gray-300 rounded-md py-2 pl-10 pr-3 text-sm placeholder-gray-500 focus:outline-none focus:text-gray-900 focus:placeholder-gray-400 focus:ring-1 focus:ring-gray-900 focus:border-gray-900 sm:text-sm" placeholder="Search" type="search" />
              </div>
            </div>
          </div> -->

          
        </div>
        <nav class="hidden lg:py-2 lg:flex lg:space-x-8" aria-label="Global">
          <NuxtLink v-for="item in navigation" :key="item.name" :to="item.href" :class="[ (item.exact ? item.href == $route.path : $route.path.startsWith(item.href)) ? 'bg-gray-100 text-gray-900' : '', 'rounded-md py-2 px-3 inline-flex items-center text-sm font-medium text-gray-900 hover:bg-gray-50 hover:text-gray-900']">
            {{ item.name }}
          </NuxtLink>
        </nav>
      </div>

      
    </Disclosure>
</template>

<script>
import { useStorefrontStore } from '@/stores/storefront'
const storefront = useStorefrontStore()

import {
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
  Menu,
  MenuButton,
  MenuItem,
  MenuItems,
  RadioGroup,
  RadioGroupDescription,
  RadioGroupLabel,
  RadioGroupOption,
  Switch,
  SwitchGroup,
  SwitchLabel,
} from '@headlessui/vue'
import { QuestionMarkCircleIcon, SearchIcon } from '@heroicons/vue/solid'
import {
  BellIcon,
  MenuIcon,
  XIcon,
} from '@heroicons/vue/outline'

const navigation = [
  { name: 'Dashboard', href: '/admin', exact: true },
  { name: 'Products', href: '/admin/products' },
  { name: 'Categories', href: '/admin/categories' },
  { name: 'Settings', href: '/admin/settings' },
]

export default {
  components: {
    Disclosure,
    DisclosureButton,
    DisclosurePanel,
    Menu,
    MenuButton,
    MenuItem,
    MenuItems,
    RadioGroup,
    RadioGroupDescription,
    RadioGroupLabel,
    RadioGroupOption,
    Switch,
    SwitchGroup,
    SwitchLabel,
    BellIcon,
    MenuIcon,
    QuestionMarkCircleIcon,
    SearchIcon,
    XIcon,
  },
  setup() {
    return {
      navigation,
      storefront
    }
  },
}
</script>