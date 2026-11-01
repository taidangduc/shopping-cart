<script setup lang="ts">
import { useCart } from '@/composable/useCart';
import type { CartProduct } from '@/definition/cart';

defineProps<{ item: CartProduct; editable?: boolean }>();

const { upsert } = useCart();
</script>

<template>
  <div class="d-flex align-items-center gap-3 py-2">
    <div class="flex-grow-1">
      <div>{{ item.name }}</div>
      <small>${{ item.price.toFixed(2) }} × {{ item.quantity }}</small>
    </div>

    <label class="visually-hidden">Quantity</label>
    <input
      type="number"
      min="0"
      max="99"
      class="form-control"
      style="width: 60px"
      :value="item.quantity"
      @change="upsert(item, Number(($event.target as HTMLInputElement).value))"
    />

    <button
      class="btn p-0 border-0 bg-transparent text-danger"
      @click="upsert({ ...item, quantity: 0 })"
    >
      delete
    </button>
  </div>
</template>
