import { defineNuxtConfig } from 'nuxt3'

// https://v3.nuxtjs.org/docs/directory-structure/nuxt.config
export default defineNuxtConfig({
    head: {
        link: [
            {
                rel: "stylesheet",
                href: "https://rsms.me/inter/inter.css"
            }
        ]
    },

    components: [
        {
            path: '~/components',
            pathPrefix: false
        }
    ],

    css: ['tailwindcss/tailwind.css'],
    build: {
        postcss: {
            postcssOptions: require('./postcss.config.js'),
        },
    },

    publicRuntimeConfig: {
        baseUrl: process.env.BASE_URL || 'https://localhost:4000/api/'
    },

    buildModules: [
        ['@pinia/nuxt', { disableVuex: true }]
    ],

    ssr: false,
    // target: 'static',
})
