<template>
    <!-- Favorites section -->
    <section aria-labelledby="favorites-heading">
        <div class="max-w-7xl mx-auto py-24 px-4 sm:py-32 sm:px-6 lg:px-8">
            <div class="sm:flex sm:items-baseline sm:justify-between">
                <h2
                    id="favorites-heading"
                    class="text-2xl font-extrabold tracking-tight text-gray-900"
                >Our Favorites</h2>
                <a
                    href="#"
                    class="hidden text-sm font-semibold text-primary-600 hover:text-primary-500 sm:block"
                >
                    Browse all favorites
                    <span aria-hidden="true">&rarr;</span>
                </a>
            </div>

            <div
                class="mt-6 grid grid-cols-1 gap-y-10 sm:grid-cols-3 sm:gap-y-0 sm:gap-x-6 lg:gap-x-8"
            >
                <!-- <div v-for="favorite in favorites" :key="favorite.id" class="group relative">
                    <div
                        class="w-full h-96 rounded-lg overflow-hidden group-hover:opacity-75 sm:h-auto sm:aspect-w-2 sm:aspect-h-3"
                    >
                        <img
                            :src="favorite.imageSrc"
                            :alt="favorite.imageAlt"
                            class="w-full h-full object-center object-cover"
                        />
                    </div>
                    <h3 class="mt-4 text-base font-semibold text-gray-900">
                        <a :href="favorite.href">
                            <span class="absolute inset-0" />
                            {{ favorite.name }}
                        </a>
                    </h3>
                    <p class="mt-1 text-sm text-gray-500">{{ favorite.price }}</p>
                </div> -->
            </div>

            <div class="mt-6 sm:hidden">
                <a
                    href="#"
                    class="block text-sm font-semibold text-primary-600 hover:text-primary-500"
                >
                    Browse all favorites
                    <span aria-hidden="true">&rarr;</span>
                </a>
            </div>
        </div>
    </section>
</template>