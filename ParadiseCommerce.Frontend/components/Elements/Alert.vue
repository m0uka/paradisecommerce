<template>
  <div class="rounded-md p-4" :class="`bg-${color}-50`">
    <div class="flex">
      <div class="flex-shrink-0">
        <component :is="icon ?? CheckCircleIcon" class="h-5 w-5" :class="`text-${color}-400`" aria-hidden="true" />
      </div>
      <div class="ml-3">
        <h3 class="text-sm font-medium" :class="`text-${color}-800`">
          {{ title }}
        </h3>
        <div class="mt-2 text-sm" :class="`text-${color}-700`">
          <p>
            {{ description }}
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { CheckCircleIcon, ExclamationCircleIcon } from '@heroicons/vue/solid'

export default {
  components: {
    CheckCircleIcon,
    ExclamationCircleIcon
  },
  props: {
    title: String,
    description: String,
    color: String,
    icon: String
  }
}
</script>