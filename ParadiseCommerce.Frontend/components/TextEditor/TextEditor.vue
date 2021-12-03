<template>
  <div class="w-full flex flex-col max-h-80 text-gray-100 border border-gray-300 rounded-md shadow-sm" v-if="editor">
    <TextEditorMenu class="flex align-middle flex-wrap p-1" :editor="editor" />
    <EditorContent class="editor-content w-full p-1 text-black overflow-x-hidden overflow-y-auto" :editor="editor" />
  </div>
</template>

<script>
import { Editor, EditorContent } from '@tiptap/vue-3'
import StarterKit from '@tiptap/starter-kit'

export default {
  components: {
    EditorContent,
  },

  props: {
    modelValue: {
      type: String,
      default: '',
    },
  },

  data() {
    return {
      editor: null,
    }
  },

  watch: {
    modelValue(value) {
      const isSame = this.editor.getHTML() === value

      if (isSame) {
        return
      }

      this.editor.commands.setContent(value, false)
    },
  },

  mounted() {
    this.editor = new Editor({
      content: this.modelValue,
      extensions: [
        StarterKit,
      ],
      onUpdate: () => {
        this.$emit('update:modelValue', this.editor.getHTML())
      },
    })
  },

  beforeUnmount() {
    this.editor.destroy()
  },
}
</script>

<style>
.editor-content div {
  @apply prose p-1;
  max-width: 100% !important;
}

.editor-content div > * {
  margin-bottom: 0.2em !important;
  margin-top: 0.2em !important;
}

.editor-content div > ul > li > *:last-child {
  margin-bottom: 0.2em;
  margin-top: 0.2em;
}

.editor-content div > ul > li > *:first-child {
  margin-bottom: 0.2em;
  margin-top: 0.2em;
}

</style>