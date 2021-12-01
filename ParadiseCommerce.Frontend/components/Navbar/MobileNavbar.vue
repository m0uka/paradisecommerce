<template>
    <TransitionRoot as="template" :show="open">
        <Dialog as="div" class="fixed inset-0 flex z-40 lg:hidden" @close="open = false">
            <TransitionChild
                as="template"
                enter="transition-opacity ease-linear duration-300"
                enter-from="opacity-0"
                enter-to="opacity-100"
                leave="transition-opacity ease-linear duration-300"
                leave-from="opacity-100"
                leave-to="opacity-0"
            >
                <DialogOverlay class="fixed inset-0 bg-black bg-opacity-25" />
            </TransitionChild>

            <TransitionChild
                as="template"
                enter="transition ease-in-out duration-300 transform"
                enter-from="-translate-x-full"
                enter-to="translate-x-0"
                leave="transition ease-in-out duration-300 transform"
                leave-from="translate-x-0"
                leave-to="-translate-x-full"
            >
                <div
                    class="relative max-w-xs w-full bg-white shadow-xl pb-12 flex flex-col overflow-y-auto"
                >
                    <div class="px-4 pt-5 pb-2 flex">
                        <button
                            type="button"
                            class="-m-2 p-2 rounded-md inline-flex items-center justify-center text-gray-400"
                            @click="open = false"
                        >
                            <span class="sr-only">Close menu</span>
                            <XIcon class="h-6 w-6" aria-hidden="true" />
                        </button>
                    </div>

                    <!-- Links -->
                    <TabGroup as="div" class="mt-2">
                        <div class="border-b border-gray-200">
                            <TabList class="-mb-px flex px-4 space-x-8">
                                <Tab
                                    as="template"
                                    v-for="category in navigation.categories"
                                    :key="category.name"
                                    v-slot="{ selected }"
                                >
                                    <button
                                        :class="[selected ? 'text-indigo-600 border-indigo-600' : 'text-gray-900 border-transparent', 'flex-1 whitespace-nowrap py-4 px-1 border-b-2 text-base font-medium']"
                                    >{{ category.name }}</button>
                                </Tab>
                            </TabList>
                        </div>
                        <TabPanels as="template">
                            <TabPanel
                                v-for="category in navigation.categories"
                                :key="category.name"
                                class="pt-10 pb-8 px-4 space-y-10"
                            >
                                <div class="grid grid-cols-2 gap-x-4">
                                    <div
                                        v-for="item in category.featured"
                                        :key="item.name"
                                        class="group relative text-sm"
                                    >
                                        <div
                                            class="aspect-w-1 aspect-h-1 rounded-lg bg-gray-100 overflow-hidden group-hover:opacity-75"
                                        >
                                            <img
                                                :src="item.imageSrc"
                                                :alt="item.imageAlt"
                                                class="object-center object-cover"
                                            />
                                        </div>
                                        <a
                                            :href="item.href"
                                            class="mt-6 block font-medium text-gray-900"
                                        >
                                            <span class="absolute z-10 inset-0" aria-hidden="true" />
                                            {{ item.name }}
                                        </a>
                                        <p aria-hidden="true" class="mt-1">Shop now</p>
                                    </div>
                                </div>
                                <div v-for="section in category.sections" :key="section.name">
                                    <p
                                        :id="`${category.id}-${section.id}-heading-mobile`"
                                        class="font-medium text-gray-900"
                                    >{{ section.name }}</p>
                                    <ul
                                        role="list"
                                        :aria-labelledby="`${category.id}-${section.id}-heading-mobile`"
                                        class="mt-6 flex flex-col space-y-6"
                                    >
                                        <li
                                            v-for="item in section.items"
                                            :key="item.name"
                                            class="flow-root"
                                        >
                                            <a
                                                :href="item.href"
                                                class="-m-2 p-2 block text-gray-500"
                                            >{{ item.name }}</a>
                                        </li>
                                    </ul>
                                </div>
                            </TabPanel>
                        </TabPanels>
                    </TabGroup>

                    <div class="border-t border-gray-200 py-6 px-4 space-y-6">
                        <div v-for="page in navigation.pages" :key="page.name" class="flow-root">
                            <a
                                :href="page.href"
                                class="-m-2 p-2 block font-medium text-gray-900"
                            >{{ page.name }}</a>
                        </div>
                    </div>

                    <div class="border-t border-gray-200 py-6 px-4 space-y-6">
                        <div class="flow-root">
                            <a href="#" class="-m-2 p-2 block font-medium text-gray-900">Sign in</a>
                        </div>
                        <div class="flow-root">
                            <a
                                href="#"
                                class="-m-2 p-2 block font-medium text-gray-900"
                            >Create account</a>
                        </div>
                    </div>

                    <div class="border-t border-gray-200 py-6 px-4">
                        <a href="#" class="-m-2 p-2 flex items-center">
                            <img
                                src="https://tailwindui.com/img/flags/flag-canada.svg"
                                alt
                                class="w-5 h-auto block flex-shrink-0"
                            />
                            <span class="ml-3 block text-base font-medium text-gray-900">CAD</span>
                            <span class="sr-only">, change currency</span>
                        </a>
                    </div>
                </div>
            </TransitionChild>
        </Dialog>
    </TransitionRoot>
</template>

<script>
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
    props: {
        open: Boolean,
        navigation: Object
    }
}
</script>