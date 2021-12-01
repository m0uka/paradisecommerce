<template>
    <RootComponent class="h-screen">
        <AdminNavbar />

        <main class="max-w-7xl mx-auto pb-10 lg:py-12 lg:px-8">
            <div class="lg:grid lg:grid-cols-12 lg:gap-x-5">
                <AdminSidebar />

                <div class="space-y-6 sm:px-6 lg:px-0 lg:col-span-9">
                    <slot />
                </div>
            </div>
        </main>

    </RootComponent>
</template>