<template>
  <SwitchGroup as="div" class="flex items-center justify-between">
    <span class="flex-grow flex flex-col">
      <InputLabel>{{ title }}</InputLabel>
      <SwitchDescription as="span" class="text-sm text-gray-500">{{ description }}</SwitchDescription>
    </span>
    <Switch @update:modelValue="update" v-model="modelValue" :class="[modelValue ? 'bg-primary-600' : 'bg-gray-200', 'relative inline-flex flex-shrink-0 h-6 w-11 border-2 border-transparent rounded-full cursor-pointer transition-colors ease-in-out duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500']">
      <span aria-hidden="true" :class="[modelValue ? 'translate-x-5' : 'translate-x-0', 'pointer-events-none inline-block h-5 w-5 rounded-full bg-white shadow transform ring-0 transition ease-in-out duration-200']" />
    </Switch>
  </SwitchGroup>
</template>

<script>
import { ref } from 'vue'
import { Switch, SwitchDescription, SwitchGroup, SwitchLabel } from '@headlessui/vue'

export default {
  components: {
    Switch,
    SwitchDescription,
    SwitchGroup,
    SwitchLabel,
  },
  props: {
    title: String,
    description: String,
    modelValue: Boolean
  },
  emits: ['update:modelValue'],
  methods: {
      update (val) {
          this.$emit('update:modelValue', val)
      }
  },
}
</script>