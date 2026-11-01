<script setup lang="ts">
import { ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useAuthStore } from '@/store/auth';

const email = ref('');
const password = ref('');
const errorMessage = ref<string | null>(null);
const loading = ref(false);

const route = useRoute();
const router = useRouter();
const authStore = useAuthStore();

async function submit(): Promise<void> {
  if (!email.value.trim() || !password.value) {
    errorMessage.value = 'Email and password are required.';
    return;
  }

  loading.value = true;
  errorMessage.value = null;
  try {
    await authStore.login({ email: email.value, password: password.value });
    const redirect = typeof route.query.redirect === 'string' ? route.query.redirect : '/product';
    router.push(redirect);
  } catch {
    errorMessage.value = 'Invalid email or password.';
  } finally {
    loading.value = false;
  }
}
</script>

<template>
  <div class="mx-auto my-2 p-3" style="max-width: 400px">
    <form @submit.prevent="submit" class="d-flex flex-column gap-3">
      <h3 class="text-center">Login</h3>
      <div>
        <label for="email">Email</label>
        <input id="email" v-model="email" type="email" class="form-control" required />
      </div>
      <div>
        <label for="password">Password</label>
        <input id="password" v-model="password" type="password" class="form-control" required />
      </div>
      <button type="submit" class="btn btn-primary" :disabled="loading">Sign in</button>
    </form>
    <div class="text-center small">
      <div class="text-body">For demo: user@example.com / Password123!</div>
      <div v-if="errorMessage" class="text-danger">
        {{ errorMessage }}
      </div>
    </div>
  </div>
</template>
