import { useCartStore } from '@/store/cart';
import * as api from '@/api/cart';
import { ref } from 'vue';
import type { Product } from '@/definition/product';
import type { CartProduct } from '@/definition/cart';

export function useCart() {
  const isLoading = ref(false);
  const error = ref<string | null>(null);

  const store = useCartStore();

  const upsert = async (product: Product | CartProduct, quantity: number, isPartial: boolean) => {
    isLoading.value = true;
    error.value = null;

    try {
      const copyCart = [...store.items];
      const index = copyCart.findIndex((item) => item.id === product.id);

      if (index === -1) {
        if (quantity <= 0) return;
        copyCart.push({ ...product, quantity });
      } else {
        if (quantity > 0) {
          copyCart[index] = {
            ...copyCart[index],
            quantity: isPartial ? copyCart[index].quantity + quantity : quantity,
          };
        } else {
          copyCart.splice(index, 1);
        }
      }
      var response = await api.updateCart(copyCart);

      if (response) {
        store.items = response.items;
      } else {
        store.reset();
      }
    } catch (err) {
      await store.fetch();
      error.value = 'Upsert error';
    } finally {
      isLoading.value = false;
    }
  };

  const clear = async () => {
    isLoading.value = true;
    error.value = null;

    try {
      await api.deleteCart();
      store.reset();
    } catch {
      error.value = 'Clear error';
    } finally {
      isLoading.value = false;
    }
  };

  return {
    upsert,
    clear,
    isLoading,
    error,
  };
}
