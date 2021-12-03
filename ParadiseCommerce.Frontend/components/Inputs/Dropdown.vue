<template>
  <Listbox as="div" v-model="internalValue" @update:modelValue="changeSelected">
    <div class="mt-1 relative">
      <ListboxButton class="bg-white relative w-full border border-gray-300 rounded-md shadow-sm pl-3 pr-10 py-2 text-left cursor-default focus:outline-none focus:ring-1 focus:ring-primary-500 focus:border-primary-500 sm:text-sm">
        <span class="block truncate">{{ internalValue }}</span>
        <span class="absolute inset-y-0 right-0 flex items-center pr-2 pointer-events-none">
          <SelectorIcon class="h-5 w-5 text-gray-400" aria-hidden="true" />
        </span>
      </ListboxButton>

      <transition leave-active-class="transition ease-in duration-100" leave-from-class="opacity-100" leave-to-class="opacity-0">
        <ListboxOptions class="absolute z-10 mt-1 w-full bg-white shadow-lg max-h-60 rounded-md py-1 text-base ring-1 ring-black ring-opacity-5 overflow-auto focus:outline-none sm:text-sm">
          <ListboxOption as="template" v-for="item in data" :key="item.id" :value="item" v-slot="{ active }">
            <li :class="[active ? 'text-white bg-primary-600' : 'text-gray-900', 'cursor-default select-none relative py-2 pl-3 pr-9']">
              <span :class="[internalValue == item.name ? 'font-semibold' : 'font-normal', 'block truncate']">
                {{ item.name }}
              </span>

              <span v-if="internalValue == item.name" :class="[active ? 'text-white' : 'text-primary-600', 'absolute inset-y-0 right-0 flex items-center pr-4']">
                <CheckIcon class="h-5 w-5" aria-hidden="true" />
              </span>
            </li>
          </ListboxOption>
        </ListboxOptions>
      </transition>
    </div>
  </Listbox>
</template>

<script>
import { Listbox, ListboxButton, ListboxLabel, ListboxOption, ListboxOptions } from '@headlessui/vue'
import { CheckIcon, SelectorIcon } from '@heroicons/vue/solid'

export default {
    components: {
        Listbox,
        ListboxButton,
        ListboxLabel,
        ListboxOption,
        ListboxOptions,
        CheckIcon,
        SelectorIcon,
    },
    props: {
        data: Array,
        modelValue: String
    },
    emits: ['update:modelValue'],
    methods: {
        changeSelected(sel) {
            this.internalValue = sel.name
            this.$emit('update:modelValue', sel.name) 
        }
    },
    watch: {
        modelValue: function (val) {
            this.internalValue = val
        }
    },
    data() {
        return {
            internalValue: null
        }
    }
}
</script>