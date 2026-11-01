<script setup lang="ts">
import { ref, computed, watch, onBeforeUnmount } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { getProducts } from '@/api/product';
import { useCart } from '@/composable/useCart';
import type { Product } from '@/definition/product';
import Breadcumb from '@/components/breadcumb.vue';
import Loading from '@/components/loading.vue';
import Pagination from '@/components/pagination.vue';
import { storeToRefs } from 'pinia';
import { useAuthStore } from '@/store/auth';

const route = useRoute();
const productList = ref<Product[]>([]);
const totalPages = ref(1);
const loading = ref(false);
const error = ref<string | null>(null);
const pageSize = 10;
const page = computed(() => Math.max(1, Number(route.query.page) || 1));
const totalItems = ref(0);
let timer: ReturnType<typeof setTimeout> | null = null;
const showPagination = computed(() => totalPages.value > 1);

const router = useRouter();

const authStore = useAuthStore();
const { isAuthenticated } = storeToRefs(authStore);
const { upsert, error: cartError } = useCart();

async function fetchProducts(pageIndex: number): Promise<void> {
  try {
    const response = await getProducts(pageIndex, pageSize);
    productList.value = response.items;

    if (response.totalPages > 0 && pageIndex > response.totalPages) {
      router.replace({ query: { ...route.query, page: String(response.totalPages) } });
    }

    totalPages.value = response.totalPages;
    totalItems.value = response.total;
  } catch {
    error.value = 'Unable to search products. Please try again.';
  } finally {
    loading.value = false;
  }
}

async function addToCart(product: Product): Promise<void> {
  if (!isAuthenticated.value) {
    router.push({ name: 'login', query: { redirect: '/product' } });
    return;
  }
  await upsert(product, 1, true);
}

watch(
  page,
  (nextPage) => {
    loading.value = true;
    error.value = null;
    if (timer) clearTimeout(timer);
    timer = setTimeout(() => {
      fetchProducts(nextPage);
    }, 1000);
  },
  { immediate: true },
);

onBeforeUnmount(() => {
  if (timer) clearTimeout(timer);
});
</script>

<template>
  <Breadcumb />

  <template v-if="showPagination">
    <div class="border-bottom border-top py-3">
      <div class="d-flex justify-content-between align-items-center">
        <div>
          <span>Total results: {{ totalItems }}</span>
        </div>
        <Pagination :page="page" :totalPages="totalPages" />
      </div>
    </div>
  </template>

  <section class="mx-auto py-4">
    <p v-if="cartError" class="alert alert-warning py-2" role="alert">{{ cartError }}</p>

    <Loading v-if="loading" />

    <p v-else-if="!productList.length && !loading">No items found.</p>

    <div v-else class="row g-3">
      <div v-for="product in productList" :key="product.id" class="col-sm-6 col-lg-4 col-xl-3">
        <article class="card h-h-100">
          <div class="card-body d-flex flex-column gap-2">
            <span>{{ product.name }}</span>
            <span>${{ product.price.toFixed(2) }} </span>
            <button
              class="btn btn-primary mt-auto"
              :disabled="product.stock === 0"
              @click="addToCart(product)"
            >
              Add to cart
            </button>
          </div>
        </article>
      </div>
    </div>
  </section>
</template>
