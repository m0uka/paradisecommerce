<template>
    <TextInput :modelValue="modelValue + ''" type="number" placeholder="0.00" @update:modelValue="$emit('update:modelValue', $event)" :name="name" :placeholder="placeholder" :error="error">
        <!-- <template v-slot:left>
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <span class="text-gray-500 sm:text-sm">
                $
                </span>
            </div>
        </template> -->

        <template v-slot:right>
            <div class="absolute inset-y-0 right-0 flex items-center">
                <label for="currency" class="sr-only">Currency</label>
                <select v-model="currency" @change="$emit('update:currency', $event.target.value)" id="currency" name="currency" class="focus:ring-primary-500 focus:border-primary-500 h-full py-0 pl-2 pr-7 border-transparent bg-transparent text-gray-500 sm:text-sm rounded-md">
                    <option v-for="(currency, index) in currencies">{{ currency }}</option>
                </select>
            </div>
        </template>
    </TextInput>
</template>

<script>
export default {
    props: {
        name: String,
        placeholder: String,

        error: String,
        currencies: Array,

        modelValue: Number,
        currency: String
    },
}
</script>