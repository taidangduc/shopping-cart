<script setup lang="ts">
import { onMounted, watch } from 'vue';
import { RouterView } from 'vue-router';
import Header from '@/components/header.vue';
import { useCartStore } from '@/store/cart';
import { useAuthStore } from './store/auth';
import Drawer from '@/components/drawer.vue';
import MiniCart from '@/views/cart/mini-cart.vue';
import { storeToRefs } from 'pinia';
import { ref } from 'vue';

const authStore = useAuthStore();
const cartStore = useCartStore();

const { isAuthenticated } = storeToRefs(authStore);
const drawer = ref(false);

onMounted(async () => {
  await authStore.fetch();
  if (isAuthenticated.value) {
    await cartStore.fetch();
  }
});

watch(isAuthenticated, async (loggedIn) => {
  if (loggedIn) {
    await cartStore.fetch();
  } else {
    cartStore.reset();
  }
});
</script>

<template>
  <Header @openCart="drawer = true" />
  <Drawer :open="drawer" title="Your cart" @close="drawer = false">
    <MiniCart @navigate="drawer = false" />
  </Drawer>
  <main class="container">
    <RouterView />
  </main>
</template>
