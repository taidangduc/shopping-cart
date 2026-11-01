<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router';
import { useAuthStore } from '@/store/auth';
import { useCartStore } from '@/store/cart';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';

const authStore = useAuthStore();
const cartStore = useCartStore();

const { user, isAuthenticated } = storeToRefs(authStore);
const { count } = storeToRefs(cartStore);
const router = useRouter();

const emit = defineEmits<{
  openCart: [];
}>();

function handleLogout(): void {
  authStore.logout();
  cartStore.reset();
  router.push('/login');
}

onMounted(async () => {
  if (!authStore.isAuthenticated) return;

  try {
    await cartStore.fetch();
  } catch {
    cartStore.reset();
  }
});
</script>

<template>
  <header class="border-bottom sticky-top bg-white">
    <div class="container d-flex align-items-center justify-content-between py-2">
      <RouterLink to="/">
        <img src="../assets/logo.png" alt="Logo" height="60px" width="auto" />
      </RouterLink>
      <div class="d-flex align-items-center gap-2">
        <template v-if="isAuthenticated">
          <span class="text-lg">{{ user?.username }}</span
          >/
          <button class="btn p-0 border-0 bg-transparent" @click="emit('openCart')">
            ( {{ count }} )</button
          >/
          <a href="#" class="text-decoration-none pe-auto" @click="handleLogout">sign out</a>
        </template>
        <RouterLink v-else to="/login"> Sign in </RouterLink>
      </div>
    </div>
  </header>
</template>
