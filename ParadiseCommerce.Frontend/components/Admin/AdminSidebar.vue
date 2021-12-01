<template>
    <aside class="py-6 px-2 sm:px-6 lg:py-0 lg:px-0 lg:col-span-3">
        <nav class="space-y-1">
            <NuxtLink
                v-for="item in subNavigation"
                :key="item.name"
                :to="item.href"
                :class="[$route.path == item.href ? 'bg-gray-50 text-primary-600 hover:bg-white' : 'text-gray-900 hover:text-gray-900 hover:bg-gray-50', 'group rounded-md px-3 py-2 flex items-center text-sm font-medium']"
                :aria-current="$route.path == item.href ? 'page' : undefined"
            >
                <component
                    :is="item.icon"
                    :class="[$route.path == item.href ? 'text-primary-500' : 'text-gray-400 group-hover:text-gray-500', 'flex-shrink-0 -ml-1 mr-3 h-6 w-6']"
                    aria-hidden="true"
                />
                <span class="truncate">{{ item.name }}</span>
            </NuxtLink>
        </nav>
    </aside>
</template>

<script setup>
import { ref } from 'vue'
import {
    BellIcon,
    CogIcon,
    CreditCardIcon,
    KeyIcon,
    DocumentDuplicateIcon,
    ViewGridAddIcon,
} from '@heroicons/vue/outline'

const subNavigation = ref([
    { name: 'General', href: '/admin', icon: CogIcon },
    { name: 'Front Page', href: '/admin/frontpage', icon: DocumentDuplicateIcon },
])
</script>