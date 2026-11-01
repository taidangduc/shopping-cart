<script setup lang="ts">
import { onMounted } from 'vue';
import { RouterLink } from 'vue-router';
import { useCartStore } from '@/store/cart';
import { useCart } from '@/composable/useCart.ts';
import CartTargetItem from './cart-target-item.vue';
import Breadcumb from '@/components/breadcumb.vue';
import { storeToRefs } from 'pinia';

const store = useCartStore();

const { items, count, calculate } = storeToRefs(store);
const { isLoading, error, clear } = useCart();

onMounted(async () => {
  await store.fetch();
});
</script>

<template>
  <section>
    <Breadcumb />

    <p v-if="isLoading && items.length === 0" class="text-muted">Loading…</p>

    <p v-else-if="error && items.length === 0" class="text-center py-5">
      <span class="text-danger d-block mb-3">{{ error }}</span>
      <button type="button" class="btn btn-outline-secondary" @click="store.fetch()">
        Try again
      </button>
    </p>

    <div v-else-if="items.length === 0" class="text-center py-5">
      <p class="text-muted mb-4">Your cart is empty.</p>
      <RouterLink to="/product" class="btn btn-primary">Continue shopping</RouterLink>
    </div>

    <template v-else>
      <p v-if="error" class="alert alert-warning py-2" role="alert">{{ error }}</p>

      <ul class="cart-items list-unstyled border rounded bg-white p-3 mb-3">
        <li v-for="item in items" :key="item.id" class="border-bottom">
          <CartTargetItem :item="item" />
        </li>
      </ul>

      <div class="d-flex align-items-center justify-content-between">
        <button type="button" class="btn btn-outline-danger" @click="clear">Clear cart</button>
        <div class="d-flex align-items-center gap-4">
          <span class="text-muted">{{ count }} item(s)</span>
          <span class="fw-bold fs-5">${{ calculate.toFixed(2) }}</span>
        </div>
      </div>
    </template>
  </section>
</template>
