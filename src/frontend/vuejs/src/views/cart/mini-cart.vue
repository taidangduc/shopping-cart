<script setup lang="ts">
import { useCartStore } from '@/store/cart';
import { storeToRefs } from 'pinia';
import { useCart } from '@/composable/useCart';

const { upsert, error: cartError } = useCart();
const cartStore = useCartStore();
const { items, count, calculate } = storeToRefs(cartStore);

const emit = defineEmits<{
  navigate: [];
}>();
</script>
<template>
  <div class="d-flex flex-column h-100">
    <div class="flex-grow-1 overflow-auto p-3">
      <p v-if="items.length === 0" class="text-muted">Your cart is empty.</p>

      <div
        v-for="item in items"
        :key="item.id"
        class="d-flex justify-content-between align-items-start gap-2 mb-3"
      >
        <div>
          <div>{{ item.name }}</div>
          <small class="text-muted">${{ item.price.toFixed(2) }} x {{ item.quantity }}</small>
        </div>

        <button
          class="btn p-0 border-0 bg-transparent text-danger"
          @click="upsert({ ...item, quantity: 0 })"
        >
          delete
        </button>
      </div>
    </div>

    <footer class="p-3 border-top bg-white">
      <div class="d-flex justify-content-between mb-3">
        <span>{{ count }} item(s)</span>
        <span class="fw-bold fs-5">${{ calculate.toFixed(2) }}</span>
      </div>

      <RouterLink to="/cart" class="btn btn-primary w-100" @click="emit('navigate')">
        View Cart
      </RouterLink>
    </footer>
  </div>
</template>
