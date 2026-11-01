import { defineStore } from 'pinia';
import { computed, ref } from 'vue';
import * as cartApi from '@/api/cart';
import type { CartProduct } from '@/definition/cart';

export const useCartStore = defineStore('cart', () => {
  const items = ref<CartProduct[]>([]);
  const isFetchError = ref(false);
  const isOpen = ref(false);

  const count = computed(() => items.value.reduce((sum, item) => sum + item.quantity, 0));

  const calculate = computed(() =>
    items.value.reduce((sum, item) => sum + item.price * item.quantity, 0),
  );

  async function fetch(): Promise<void> {
    isFetchError.value = false;

    try {
      const cart = await cartApi.getCart();
      items.value = cart.items;
    } catch {
      items.value = [];
      isFetchError.value = true;
    }
  }

  function reset(): void {
    items.value = [];
    isOpen.value = false;
    isFetchError.value = false;
  }

  return {
    items,
    isFetchError,
    count,
    calculate,
    fetch,
    reset,
  };
});
