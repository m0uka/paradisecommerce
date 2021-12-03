<template>
  <button
    class="w-7 h-7 text-black hover:bg-gray-100 border-none bg-transparent rounded-md p-1 mr-1"
    :class="{ 'bg-gray-200': isActive ? isActive(): null }"
    @click="action"
    :title="title"
  >
    <svg class="remix w-full h-full fill-current">
      <use :xlink:href="`${remixiconUrl}#ri-${icon}`" />
    </svg>
  </button>
</template>

<script>
import remixiconUrl from 'remixicon/fonts/remixicon.symbol.svg'

export default {
  props: {
    icon: {
      type: String,
      required: true,
    },

    title: {
      type: String,
      required: true,
    },

    action: {
      type: Function,
      required: true,
    },

    isActive: {
      type: Function,
      default: null,
    },
  },

  data() {
    return {
      remixiconUrl,
    }
  },
}
</script>