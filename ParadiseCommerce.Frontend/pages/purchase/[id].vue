<template>
    <RootComponent>
        <Navbar />
        <!-- Background color split screen for large screens -->
        <div class="hidden lg:block fixed top-0 left-0 w-1/2 h-full bg-white" aria-hidden="true" />
        <div
            class="hidden lg:block fixed top-0 right-0 w-1/2 h-full bg-primary-900"
            aria-hidden="true"
        />

        <div
            class="relative grid grid-cols-1 gap-x-16 max-w-7xl mx-auto lg:px-8 lg:grid-cols-2 lg:pt-16"
        >
            <h1 class="sr-only">Checkout</h1>

            <section
                aria-labelledby="summary-heading"
                class="bg-primary-900 text-primary-300 py-12 md:px-10 lg:max-w-lg lg:w-full lg:mx-auto lg:px-0 lg:pt-0 lg:pb-24 lg:bg-transparent lg:row-start-1 lg:col-start-2"
            >
                <div class="max-w-2xl mx-auto px-4 lg:max-w-none lg:px-0">
                    <h2 id="summary-heading" class="sr-only">Order summary</h2>

                    <dl>
                        <dt class="text-sm font-medium">Amount due</dt>
                        <dd class="mt-1 text-3xl font-extrabold text-white">{{ subtotal }}</dd>
                    </dl>

                    <ul
                        role="list"
                        class="text-sm font-medium divide-y divide-white divide-opacity-10"
                    >
                        <li
                            v-for="product in productsList"
                            :key="product?.id"
                            class="flex items-start py-6 space-x-4"
                        >
                            <img
                                :src="product?.images?.listingSmall"
                                class="flex-none w-20 h-20 rounded-md object-center object-cover"
                            />
                            <div class="flex-auto space-y-1">
                                <h3 class="text-white">{{ product?.name }}</h3>
                                <p>{{ getProductCategory(product)?.name }}</p>
                                <p>{{ product?.shortDescription }}</p>
                            </div>
                            <p
                                class="flex-none text-base font-medium text-white"
                            >{{ getProductPrice(product) }}</p>
                        </li>
                    </ul>

                    <dl
                        class="text-sm font-medium space-y-6 border-t border-white border-opacity-10 pt-6"
                    >
                        <div class="flex items-center justify-between">
                            <dt>Subtotal</dt>
                            <dd>{{ subtotal }}</dd>
                        </div>

                        <div
                            class="flex items-center justify-between border-t border-white border-opacity-10 text-white pt-6"
                        >
                            <dt class="text-base">Total</dt>
                            <dd class="text-base">{{ subtotal }}</dd>
                        </div>
                    </dl>
                </div>
            </section>

            <section
                aria-labelledby="payment-and-shipping-heading"
                class="py-16 lg:max-w-lg lg:w-full lg:mx-auto lg:pt-0 lg:pb-24 lg:row-start-1 lg:col-start-1"
            >
                <h2 id="payment-and-shipping-heading" class="sr-only">Payment and shipping details</h2>

                    <div class="max-w-2xl mx-auto px-4 lg:max-w-none lg:px-0">
                        <div v-if="!authStore?.loggedIn">
                            <h3
                                id="contact-info-heading"
                                class="text-lg font-medium text-gray-900"
                            >Contact information</h3>

                            <div class="mt-6">
                                <label
                                    for="email-address"
                                    class="block text-sm font-medium text-gray-700"
                                >Email address</label>
                                <div class="mt-1">
                                    <input
                                        type="email"
                                        id="email-address"
                                        name="email-address"
                                        autocomplete="email"
                                        class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                    />
                                </div>
                            </div>
                        </div>

                        <div class="mt-10">
                            <h3 class="text-lg font-medium text-gray-900">Payment method</h3>

                            <RadioGroup v-model="selectedPaymentMethod">
                                <div
                                    class="mt-4 grid grid-cols-1 gap-y-6 sm:grid-cols-2 sm:gap-x-4"
                                >
                                    <RadioGroupOption
                                        as="template"
                                        v-for="paymentMethod in paymentMethods"
                                        :key="paymentMethod.id"
                                        :value="paymentMethod"
                                        v-slot="{ checked, active }"
                                    >
                                        <div
                                            :class="[checked ? 'border-transparent' : 'border-gray-300', active ? 'ring-2 ring-primary-500' : '', 'relative bg-white border rounded-lg shadow-sm p-4 flex cursor-pointer focus:outline-none']"
                                        >
                                            <div class="flex-1 flex">
                                                <div class="flex flex-col">
                                                    <RadioGroupLabel
                                                        as="span"
                                                        class="block text-sm font-medium text-gray-900"
                                                    >{{ paymentMethod.title }}</RadioGroupLabel>
                                                    <RadioGroupDescription
                                                        as="span"
                                                        class="mt-1 flex items-center text-sm text-gray-500"
                                                    >{{ paymentMethod.description }}</RadioGroupDescription>
                                                </div>
                                            </div>
                                            <CheckCircleIcon
                                                v-if="checked"
                                                class="h-5 w-5 text-primary-600"
                                                aria-hidden="true"
                                            />
                                            <div
                                                :class="[active ? 'border' : 'border-2', checked ? 'border-primary-500' : 'border-transparent', 'absolute -inset-px rounded-lg pointer-events-none']"
                                                aria-hidden="true"
                                            />
                                        </div>
                                    </RadioGroupOption>
                                </div>
                            </RadioGroup>
                        </div>

                        <div class="mt-10" v-if="productsList?.some(product => product?.productType == 'Physical')">
                            <h3 class="text-lg font-medium text-gray-900">Shipping address</h3>

                            <div class="mt-6 grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-3">
                                <div class="sm:col-span-3">
                                    <label
                                        for="address"
                                        class="block text-sm font-medium text-gray-700"
                                    >Address</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="address"
                                            name="address"
                                            autocomplete="street-address"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                            v-model="shippingAddress.streetAddress"
                                        />
                                    </div>
                                </div>

                                <div>
                                    <label
                                        for="city"
                                        class="block text-sm font-medium text-gray-700"
                                    >City</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="city"
                                            name="city"
                                            autocomplete="address-level2"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                            v-model="shippingAddress.city"
                                        />
                                    </div>
                                </div>

                                <div>
                                    <label
                                        for="region"
                                        class="block text-sm font-medium text-gray-700"
                                    >State / Province</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="region"
                                            name="region"
                                            autocomplete="address-level1"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                            v-model="shippingAddress.state"
                                        />
                                    </div>
                                </div>

                                <div>
                                    <label
                                        for="postal-code"
                                        class="block text-sm font-medium text-gray-700"
                                    >Postal code</label>
                                    <div class="mt-1">
                                        <input
                                            type="text"
                                            id="postal-code"
                                            name="postal-code"
                                            autocomplete="postal-code"
                                            class="block w-full border-gray-300 rounded-md shadow-sm focus:ring-primary-500 focus:border-primary-500 sm:text-sm"
                                            v-model="shippingAddress.zipCode"
                                        />
                                    </div>
                                </div>

                                <div class="col-span-3"> 
                                    <label
                                        for="postal-code"
                                        class="block text-sm font-medium text-gray-700"
                                    >Country</label>
                                    <div class="mt-1">
                                        <Dropdown :data="countryList" v-model="shippingAddress.country" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="mt-10 flex justify-end pt-6 border-t border-gray-200">
                            <Button :loading="loading" @click="finishPayment" color="bg-primary-600 hover:bg-primary-700 text-white">Pay now</Button>
                        </div>
                    </div>
            </section>
        </div>
    </RootComponent>
</template>

<script setup>
// yikes
const countryList = [ {"code": "AF", "code3": "AFG", "name": "Afghanistan", "number": "004"}, {"code": "AL", "code3": "ALB", "name": "Albania", "number": "008"}, {"code": "DZ", "code3": "DZA", "name": "Algeria", "number": "012"}, {"code": "AS", "code3": "ASM", "name": "American Samoa", "number": "016"}, {"code": "AD", "code3": "AND", "name": "Andorra", "number": "020"}, {"code": "AO", "code3": "AGO", "name": "Angola", "number": "024"}, {"code": "AI", "code3": "AIA", "name": "Anguilla", "number": "660"}, {"code": "AQ", "code3": "ATA", "name": "Antarctica", "number": "010"}, {"code": "AG", "code3": "ATG", "name": "Antigua and Barbuda", "number": "028"}, {"code": "AR", "code3": "ARG", "name": "Argentina", "number": "032"}, {"code": "AM", "code3": "ARM", "name": "Armenia", "number": "051"}, {"code": "AW", "code3": "ABW", "name": "Aruba", "number": "533"}, {"code": "AU", "code3": "AUS", "name": "Australia", "number": "036"}, {"code": "AT", "code3": "AUT", "name": "Austria", "number": "040"}, {"code": "AZ", "code3": "AZE", "name": "Azerbaijan", "number": "031"}, {"code": "BS", "code3": "BHS", "name": "Bahamas (the)", "number": "044"}, {"code": "BH", "code3": "BHR", "name": "Bahrain", "number": "048"}, {"code": "BD", "code3": "BGD", "name": "Bangladesh", "number": "050"}, {"code": "BB", "code3": "BRB", "name": "Barbados", "number": "052"}, {"code": "BY", "code3": "BLR", "name": "Belarus", "number": "112"}, {"code": "BE", "code3": "BEL", "name": "Belgium", "number": "056"}, {"code": "BZ", "code3": "BLZ", "name": "Belize", "number": "084"}, {"code": "BJ", "code3": "BEN", "name": "Benin", "number": "204"}, {"code": "BM", "code3": "BMU", "name": "Bermuda", "number": "060"}, {"code": "BT", "code3": "BTN", "name": "Bhutan", "number": "064"}, {"code": "BO", "code3": "BOL", "name": "Bolivia (Plurinational State of)", "number": "068"}, {"code": "BQ", "code3": "BES", "name": "Bonaire, Sint Eustatius and Saba", "number": "535"}, {"code": "BA", "code3": "BIH", "name": "Bosnia and Herzegovina", "number": "070"}, {"code": "BW", "code3": "BWA", "name": "Botswana", "number": "072"}, {"code": "BV", "code3": "BVT", "name": "Bouvet Island", "number": "074"}, {"code": "BR", "code3": "BRA", "name": "Brazil", "number": "076"}, {"code": "IO", "code3": "IOT", "name": "British Indian Ocean Territory (the)", "number": "086"}, {"code": "BN", "code3": "BRN", "name": "Brunei Darussalam", "number": "096"}, {"code": "BG", "code3": "BGR", "name": "Bulgaria", "number": "100"}, {"code": "BF", "code3": "BFA", "name": "Burkina Faso", "number": "854"}, {"code": "BI", "code3": "BDI", "name": "Burundi", "number": "108"}, {"code": "CV", "code3": "CPV", "name": "Cabo Verde", "number": "132"}, {"code": "KH", "code3": "KHM", "name": "Cambodia", "number": "116"}, {"code": "CM", "code3": "CMR", "name": "Cameroon", "number": "120"}, {"code": "CA", "code3": "CAN", "name": "Canada", "number": "124"}, {"code": "KY", "code3": "CYM", "name": "Cayman Islands (the)", "number": "136"}, {"code": "CF", "code3": "CAF", "name": "Central African Republic (the)", "number": "140"}, {"code": "TD", "code3": "TCD", "name": "Chad", "number": "148"}, {"code": "CL", "code3": "CHL", "name": "Chile", "number": "152"}, {"code": "CN", "code3": "CHN", "name": "China", "number": "156"}, {"code": "CX", "code3": "CXR", "name": "Christmas Island", "number": "162"}, {"code": "CC", "code3": "CCK", "name": "Cocos (Keeling) Islands (the)", "number": "166"}, {"code": "CO", "code3": "COL", "name": "Colombia", "number": "170"}, {"code": "KM", "code3": "COM", "name": "Comoros (the)", "number": "174"}, {"code": "CD", "code3": "COD", "name": "Congo (the Democratic Republic of the)", "number": "180"}, {"code": "CG", "code3": "COG", "name": "Congo (the)", "number": "178"}, {"code": "CK", "code3": "COK", "name": "Cook Islands (the)", "number": "184"}, {"code": "CR", "code3": "CRI", "name": "Costa Rica", "number": "188"}, {"code": "HR", "code3": "HRV", "name": "Croatia", "number": "191"}, {"code": "CU", "code3": "CUB", "name": "Cuba", "number": "192"}, {"code": "CW", "code3": "CUW", "name": "Curaçao", "number": "531"}, {"code": "CY", "code3": "CYP", "name": "Cyprus", "number": "196"}, {"code": "CZ", "code3": "CZE", "name": "Czechia", "number": "203"}, {"code": "CI", "code3": "CIV", "name": "Côte d'Ivoire", "number": "384"}, {"code": "DK", "code3": "DNK", "name": "Denmark", "number": "208"}, {"code": "DJ", "code3": "DJI", "name": "Djibouti", "number": "262"}, {"code": "DM", "code3": "DMA", "name": "Dominica", "number": "212"}, {"code": "DO", "code3": "DOM", "name": "Dominican Republic (the)", "number": "214"}, {"code": "EC", "code3": "ECU", "name": "Ecuador", "number": "218"}, {"code": "EG", "code3": "EGY", "name": "Egypt", "number": "818"}, {"code": "SV", "code3": "SLV", "name": "El Salvador", "number": "222"}, {"code": "GQ", "code3": "GNQ", "name": "Equatorial Guinea", "number": "226"}, {"code": "ER", "code3": "ERI", "name": "Eritrea", "number": "232"}, {"code": "EE", "code3": "EST", "name": "Estonia", "number": "233"}, {"code": "SZ", "code3": "SWZ", "name": "Eswatini", "number": "748"}, {"code": "ET", "code3": "ETH", "name": "Ethiopia", "number": "231"}, {"code": "FK", "code3": "FLK", "name": "Falkland Islands (the) [Malvinas]", "number": "238"}, {"code": "FO", "code3": "FRO", "name": "Faroe Islands (the)", "number": "234"}, {"code": "FJ", "code3": "FJI", "name": "Fiji", "number": "242"}, {"code": "FI", "code3": "FIN", "name": "Finland", "number": "246"}, {"code": "FR", "code3": "FRA", "name": "France", "number": "250"}, {"code": "GF", "code3": "GUF", "name": "French Guiana", "number": "254"}, {"code": "PF", "code3": "PYF", "name": "French Polynesia", "number": "258"}, {"code": "TF", "code3": "ATF", "name": "French Southern Territories (the)", "number": "260"}, {"code": "GA", "code3": "GAB", "name": "Gabon", "number": "266"}, {"code": "GM", "code3": "GMB", "name": "Gambia (the)", "number": "270"}, {"code": "GE", "code3": "GEO", "name": "Georgia", "number": "268"}, {"code": "DE", "code3": "DEU", "name": "Germany", "number": "276"}, {"code": "GH", "code3": "GHA", "name": "Ghana", "number": "288"}, {"code": "GI", "code3": "GIB", "name": "Gibraltar", "number": "292"}, {"code": "GR", "code3": "GRC", "name": "Greece", "number": "300"}, {"code": "GL", "code3": "GRL", "name": "Greenland", "number": "304"}, {"code": "GD", "code3": "GRD", "name": "Grenada", "number": "308"}, {"code": "GP", "code3": "GLP", "name": "Guadeloupe", "number": "312"}, {"code": "GU", "code3": "GUM", "name": "Guam", "number": "316"}, {"code": "GT", "code3": "GTM", "name": "Guatemala", "number": "320"}, {"code": "GG", "code3": "GGY", "name": "Guernsey", "number": "831"}, {"code": "GN", "code3": "GIN", "name": "Guinea", "number": "324"}, {"code": "GW", "code3": "GNB", "name": "Guinea-Bissau", "number": "624"}, {"code": "GY", "code3": "GUY", "name": "Guyana", "number": "328"}, {"code": "HT", "code3": "HTI", "name": "Haiti", "number": "332"}, {"code": "HM", "code3": "HMD", "name": "Heard Island and McDonald Islands", "number": "334"}, {"code": "VA", "code3": "VAT", "name": "Holy See (the)", "number": "336"}, {"code": "HN", "code3": "HND", "name": "Honduras", "number": "340"}, {"code": "HK", "code3": "HKG", "name": "Hong Kong", "number": "344"}, {"code": "HU", "code3": "HUN", "name": "Hungary", "number": "348"}, {"code": "IS", "code3": "ISL", "name": "Iceland", "number": "352"}, {"code": "IN", "code3": "IND", "name": "India", "number": "356"}, {"code": "ID", "code3": "IDN", "name": "Indonesia", "number": "360"}, {"code": "IR", "code3": "IRN", "name": "Iran (Islamic Republic of)", "number": "364"}, {"code": "IQ", "code3": "IRQ", "name": "Iraq", "number": "368"}, {"code": "IE", "code3": "IRL", "name": "Ireland", "number": "372"}, {"code": "IM", "code3": "IMN", "name": "Isle of Man", "number": "833"}, {"code": "IL", "code3": "ISR", "name": "Israel", "number": "376"}, {"code": "IT", "code3": "ITA", "name": "Italy", "number": "380"}, {"code": "JM", "code3": "JAM", "name": "Jamaica", "number": "388"}, {"code": "JP", "code3": "JPN", "name": "Japan", "number": "392"}, {"code": "JE", "code3": "JEY", "name": "Jersey", "number": "832"}, {"code": "JO", "code3": "JOR", "name": "Jordan", "number": "400"}, {"code": "KZ", "code3": "KAZ", "name": "Kazakhstan", "number": "398"}, {"code": "KE", "code3": "KEN", "name": "Kenya", "number": "404"}, {"code": "KI", "code3": "KIR", "name": "Kiribati", "number": "296"}, {"code": "KP", "code3": "PRK", "name": "Korea (the Democratic People's Republic of)", "number": "408"}, {"code": "KR", "code3": "KOR", "name": "Korea (the Republic of)", "number": "410"}, {"code": "KW", "code3": "KWT", "name": "Kuwait", "number": "414"}, {"code": "KG", "code3": "KGZ", "name": "Kyrgyzstan", "number": "417"}, {"code": "LA", "code3": "LAO", "name": "Lao People's Democratic Republic (the)", "number": "418"}, {"code": "LV", "code3": "LVA", "name": "Latvia", "number": "428"}, {"code": "LB", "code3": "LBN", "name": "Lebanon", "number": "422"}, {"code": "LS", "code3": "LSO", "name": "Lesotho", "number": "426"}, {"code": "LR", "code3": "LBR", "name": "Liberia", "number": "430"}, {"code": "LY", "code3": "LBY", "name": "Libya", "number": "434"}, {"code": "LI", "code3": "LIE", "name": "Liechtenstein", "number": "438"}, {"code": "LT", "code3": "LTU", "name": "Lithuania", "number": "440"}, {"code": "LU", "code3": "LUX", "name": "Luxembourg", "number": "442"}, {"code": "MO", "code3": "MAC", "name": "Macao", "number": "446"}, {"code": "MG", "code3": "MDG", "name": "Madagascar", "number": "450"}, {"code": "MW", "code3": "MWI", "name": "Malawi", "number": "454"}, {"code": "MY", "code3": "MYS", "name": "Malaysia", "number": "458"}, {"code": "MV", "code3": "MDV", "name": "Maldives", "number": "462"}, {"code": "ML", "code3": "MLI", "name": "Mali", "number": "466"}, {"code": "MT", "code3": "MLT", "name": "Malta", "number": "470"}, {"code": "MH", "code3": "MHL", "name": "Marshall Islands (the)", "number": "584"}, {"code": "MQ", "code3": "MTQ", "name": "Martinique", "number": "474"}, {"code": "MR", "code3": "MRT", "name": "Mauritania", "number": "478"}, {"code": "MU", "code3": "MUS", "name": "Mauritius", "number": "480"}, {"code": "YT", "code3": "MYT", "name": "Mayotte", "number": "175"}, {"code": "MX", "code3": "MEX", "name": "Mexico", "number": "484"}, {"code": "FM", "code3": "FSM", "name": "Micronesia (Federated States of)", "number": "583"}, {"code": "MD", "code3": "MDA", "name": "Moldova (the Republic of)", "number": "498"}, {"code": "MC", "code3": "MCO", "name": "Monaco", "number": "492"}, {"code": "MN", "code3": "MNG", "name": "Mongolia", "number": "496"}, {"code": "ME", "code3": "MNE", "name": "Montenegro", "number": "499"}, {"code": "MS", "code3": "MSR", "name": "Montserrat", "number": "500"}, {"code": "MA", "code3": "MAR", "name": "Morocco", "number": "504"}, {"code": "MZ", "code3": "MOZ", "name": "Mozambique", "number": "508"}, {"code": "MM", "code3": "MMR", "name": "Myanmar", "number": "104"}, {"code": "NA", "code3": "NAM", "name": "Namibia", "number": "516"}, {"code": "NR", "code3": "NRU", "name": "Nauru", "number": "520"}, {"code": "NP", "code3": "NPL", "name": "Nepal", "number": "524"}, {"code": "NL", "code3": "NLD", "name": "Netherlands (the)", "number": "528"}, {"code": "NC", "code3": "NCL", "name": "New Caledonia", "number": "540"}, {"code": "NZ", "code3": "NZL", "name": "New Zealand", "number": "554"}, {"code": "NI", "code3": "NIC", "name": "Nicaragua", "number": "558"}, {"code": "NE", "code3": "NER", "name": "Niger (the)", "number": "562"}, {"code": "NG", "code3": "NGA", "name": "Nigeria", "number": "566"}, {"code": "NU", "code3": "NIU", "name": "Niue", "number": "570"}, {"code": "NF", "code3": "NFK", "name": "Norfolk Island", "number": "574"}, {"code": "MP", "code3": "MNP", "name": "Northern Mariana Islands (the)", "number": "580"}, {"code": "NO", "code3": "NOR", "name": "Norway", "number": "578"}, {"code": "OM", "code3": "OMN", "name": "Oman", "number": "512"}, {"code": "PK", "code3": "PAK", "name": "Pakistan", "number": "586"}, {"code": "PW", "code3": "PLW", "name": "Palau", "number": "585"}, {"code": "PS", "code3": "PSE", "name": "Palestine, State of", "number": "275"}, {"code": "PA", "code3": "PAN", "name": "Panama", "number": "591"}, {"code": "PG", "code3": "PNG", "name": "Papua New Guinea", "number": "598"}, {"code": "PY", "code3": "PRY", "name": "Paraguay", "number": "600"}, {"code": "PE", "code3": "PER", "name": "Peru", "number": "604"}, {"code": "PH", "code3": "PHL", "name": "Philippines (the)", "number": "608"}, {"code": "PN", "code3": "PCN", "name": "Pitcairn", "number": "612"}, {"code": "PL", "code3": "POL", "name": "Poland", "number": "616"}, {"code": "PT", "code3": "PRT", "name": "Portugal", "number": "620"}, {"code": "PR", "code3": "PRI", "name": "Puerto Rico", "number": "630"}, {"code": "QA", "code3": "QAT", "name": "Qatar", "number": "634"}, {"code": "MK", "code3": "MKD", "name": "Republic of North Macedonia", "number": "807"}, {"code": "RO", "code3": "ROU", "name": "Romania", "number": "642"}, {"code": "RU", "code3": "RUS", "name": "Russian Federation (the)", "number": "643"}, {"code": "RW", "code3": "RWA", "name": "Rwanda", "number": "646"}, {"code": "RE", "code3": "REU", "name": "Réunion", "number": "638"}, {"code": "BL", "code3": "BLM", "name": "Saint Barthélemy", "number": "652"}, {"code": "SH", "code3": "SHN", "name": "Saint Helena, Ascension and Tristan da Cunha", "number": "654"}, {"code": "KN", "code3": "KNA", "name": "Saint Kitts and Nevis", "number": "659"}, {"code": "LC", "code3": "LCA", "name": "Saint Lucia", "number": "662"}, {"code": "MF", "code3": "MAF", "name": "Saint Martin (French part)", "number": "663"}, {"code": "PM", "code3": "SPM", "name": "Saint Pierre and Miquelon", "number": "666"}, {"code": "VC", "code3": "VCT", "name": "Saint Vincent and the Grenadines", "number": "670"}, {"code": "WS", "code3": "WSM", "name": "Samoa", "number": "882"}, {"code": "SM", "code3": "SMR", "name": "San Marino", "number": "674"}, {"code": "ST", "code3": "STP", "name": "Sao Tome and Principe", "number": "678"}, {"code": "SA", "code3": "SAU", "name": "Saudi Arabia", "number": "682"}, {"code": "SN", "code3": "SEN", "name": "Senegal", "number": "686"}, {"code": "RS", "code3": "SRB", "name": "Serbia", "number": "688"}, {"code": "SC", "code3": "SYC", "name": "Seychelles", "number": "690"}, {"code": "SL", "code3": "SLE", "name": "Sierra Leone", "number": "694"}, {"code": "SG", "code3": "SGP", "name": "Singapore", "number": "702"}, {"code": "SX", "code3": "SXM", "name": "Sint Maarten (Dutch part)", "number": "534"}, {"code": "SK", "code3": "SVK", "name": "Slovakia", "number": "703"}, {"code": "SI", "code3": "SVN", "name": "Slovenia", "number": "705"}, {"code": "SB", "code3": "SLB", "name": "Solomon Islands", "number": "090"}, {"code": "SO", "code3": "SOM", "name": "Somalia", "number": "706"}, {"code": "ZA", "code3": "ZAF", "name": "South Africa", "number": "710"}, {"code": "GS", "code3": "SGS", "name": "South Georgia and the South Sandwich Islands", "number": "239"}, {"code": "SS", "code3": "SSD", "name": "South Sudan", "number": "728"}, {"code": "ES", "code3": "ESP", "name": "Spain", "number": "724"}, {"code": "LK", "code3": "LKA", "name": "Sri Lanka", "number": "144"}, {"code": "SD", "code3": "SDN", "name": "Sudan (the)", "number": "729"}, {"code": "SR", "code3": "SUR", "name": "Suriname", "number": "740"}, {"code": "SJ", "code3": "SJM", "name": "Svalbard and Jan Mayen", "number": "744"}, {"code": "SE", "code3": "SWE", "name": "Sweden", "number": "752"}, {"code": "CH", "code3": "CHE", "name": "Switzerland", "number": "756"}, {"code": "SY", "code3": "SYR", "name": "Syrian Arab Republic", "number": "760"}, {"code": "TW", "code3": "TWN", "name": "Taiwan", "number": "158"}, {"code": "TJ", "code3": "TJK", "name": "Tajikistan", "number": "762"}, {"code": "TZ", "code3": "TZA", "name": "Tanzania, United Republic of", "number": "834"}, {"code": "TH", "code3": "THA", "name": "Thailand", "number": "764"}, {"code": "TL", "code3": "TLS", "name": "Timor-Leste", "number": "626"}, {"code": "TG", "code3": "TGO", "name": "Togo", "number": "768"}, {"code": "TK", "code3": "TKL", "name": "Tokelau", "number": "772"}, {"code": "TO", "code3": "TON", "name": "Tonga", "number": "776"}, {"code": "TT", "code3": "TTO", "name": "Trinidad and Tobago", "number": "780"}, {"code": "TN", "code3": "TUN", "name": "Tunisia", "number": "788"}, {"code": "TR", "code3": "TUR", "name": "Turkey", "number": "792"}, {"code": "TM", "code3": "TKM", "name": "Turkmenistan", "number": "795"}, {"code": "TC", "code3": "TCA", "name": "Turks and Caicos Islands (the)", "number": "796"}, {"code": "TV", "code3": "TUV", "name": "Tuvalu", "number": "798"}, {"code": "UG", "code3": "UGA", "name": "Uganda", "number": "800"}, {"code": "UA", "code3": "UKR", "name": "Ukraine", "number": "804"}, {"code": "AE", "code3": "ARE", "name": "United Arab Emirates (the)", "number": "784"}, {"code": "GB", "code3": "GBR", "name": "United Kingdom of Great Britain and Northern Ireland (the)", "number": "826"}, {"code": "UM", "code3": "UMI", "name": "United States Minor Outlying Islands (the)", "number": "581"}, {"code": "US", "code3": "USA", "name": "United States of America (the)", "number": "840"}, {"code": "UY", "code3": "URY", "name": "Uruguay", "number": "858"}, {"code": "UZ", "code3": "UZB", "name": "Uzbekistan", "number": "860"}, {"code": "VU", "code3": "VUT", "name": "Vanuatu", "number": "548"}, {"code": "VE", "code3": "VEN", "name": "Venezuela (Bolivarian Republic of)", "number": "862"}, {"code": "VN", "code3": "VNM", "name": "Viet Nam", "number": "704"}, {"code": "VG", "code3": "VGB", "name": "Virgin Islands (British)", "number": "092"}, {"code": "VI", "code3": "VIR", "name": "Virgin Islands (U.S.)", "number": "850"}, {"code": "WF", "code3": "WLF", "name": "Wallis and Futuna", "number": "876"}, {"code": "EH", "code3": "ESH", "name": "Western Sahara", "number": "732"}, {"code": "YE", "code3": "YEM", "name": "Yemen", "number": "887"}, {"code": "ZM", "code3": "ZMB", "name": "Zambia", "number": "894"}, {"code": "ZW", "code3": "ZWE", "name": "Zimbabwe", "number": "716"}, {"code": "AX", "code3": "ALA", "name": "Åland Islands", "number": "248"} ];

import { useRoute, useRouter } from 'vue-router'
import { useProductsStore } from "@/stores/products"
import { useAuthStore } from '@/stores/auth'
import { storeToRefs } from 'pinia'

import { CheckCircleIcon, TrashIcon } from '@heroicons/vue/solid'
import { RadioGroup, RadioGroupDescription, RadioGroupLabel, RadioGroupOption } from '@headlessui/vue'

import orderAPI from '@/api/ordering'
import billingAPI from '@/api/billing'

const paymentMethods = [
    { id: 'stripe', title: 'Card', description: 'Handled via Stripe' },
    //   { id: 'paypal', title: 'PayPal', description: 'Handled via PayPal' },
]
const selectedPaymentMethod = ref(paymentMethods[0])

const route = useRoute()
let loading = ref(false)

let shippingAddress = ref({
    address: '',
    city: '',
    state: '',
    postalCode: '',
    country: 'Czechia',
})

const productsStore = useProductsStore()
const authStore = useAuthStore()

const { products, categories } = storeToRefs(productsStore)

const product = computed(() => products?.value?.find(x => x.slug === route.params.id))
const category = computed(() => categories?.value?.find(x => x.id === product?.value?.groupId))

const currency = computed(() => Object.keys(product.value?.pricing?.currencyPrices ?? {})[0])
const price = computed(() => product.value?.pricing?.currencyPrices[currency.value])

const productsList = computed(() => [
    product?.value
])

// if multiple products have different currencies, this will fuck up, lol
const subtotal = computed(() => formatCurrency(productsList?.value?.map(product => getProductPrice(product, true)).reduce((prev, curr) => prev + curr), currency.value))

function formatCurrency(value, currency) {
    if (typeof value !== "number") {
        return value;
    }

    var formatter = new Intl.NumberFormat(navigator.language, {
        style: 'currency',
        currency: currency
    })

    return formatter.format(value)
}

function getProductPrice(product, noFormat) {
    const currency = Object.keys(product?.pricing?.currencyPrices ?? {})[0]
    const price = product?.pricing?.currencyPrices[currency]

    return !noFormat ? formatCurrency(price, currency) : price
}

function getProductCategory(product) {
    return categories?.value?.find(x => x.id == product?.groupId)
}

let poller

async function finishPayment() {
    loading.value = true

    let productInfos = []
    productsList.value.forEach(product => {
        productInfos.push({productId: product.id})
    })


    const orderPlaceModel = {
        productInfos: productInfos,
        currency: currency.value,
        paymentMethod: selectedPaymentMethod.value.id,
        shippingAddress: shippingAddress
    }

    const response = await orderAPI.placeOrder(orderPlaceModel)
    const data = await response.text()

    if (response.status == 200) {
        // poll billing api until we get a payment url
        poller = setInterval( () => pollForPayment(data), 500 )
    } else {
        // todo: maybe better handling of errors later
        alert(data)
    }
}

async function pollForPayment(orderId) {
    const response = await billingAPI.getByOrder(orderId)
    const data = await response.json()

    if (data.paymentLink) {
        // we are done here

        window.location.href = data.paymentLink
        clearInterval(poller)
        loading.value = false
    }
}

</script>