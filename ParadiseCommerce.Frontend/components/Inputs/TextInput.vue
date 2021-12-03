<template>
    <div>
        <div class="mt-1 relative rounded-md shadow-sm">
            <input :placeholder="placeholder" :value="modelValue" @input="$emit('update:modelValue', $event.target.value)" :name="name" :type="type ?? 'text'" :autocomplete="autocomplete" required="" class="appearance-none block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-primary-500 focus:border-primary-500 sm:text-sm" :class="error ? 'border-red-300 text-red-900 placeholder-red-300 focus:outline-none focus:ring-red-500 focus:border-red-500' : ''" />
            <div v-if="error" class="absolute inset-y-0 right-0 pr-3 flex items-center pointer-events-none">
                <ExclamationCircleIcon class="h-5 w-5 text-red-500" aria-hidden="true" />
            </div>
        </div>
        <p v-if="error" class="mt-2 text-sm text-red-600">{{ error }}</p>
    </div>
</template>

<script>
import { ExclamationCircleIcon } from '@heroicons/vue/solid'

export default {
    props: {
        name: String,
        type: String,
        autocomplete: String,
        placeholder: String,

        error: String,

        modelValue: String
    },

    components: {
        ExclamationCircleIcon,
    },
}
</script>