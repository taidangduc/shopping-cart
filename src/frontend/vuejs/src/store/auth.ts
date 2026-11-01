import { defineStore } from 'pinia';
import { computed, ref } from 'vue';
import * as userApi from '@/api/user';
import type { TokenRequest, User } from '@/definition/user';

export const useAuthStore = defineStore('auth', () => {
  const token = ref<string | null>(localStorage.getItem(import.meta.env.VITE_SECRET_KEY));
  const user = ref<User | null>(null);

  const isAuthenticated = computed(() => token.value !== null);

  async function login(request: TokenRequest): Promise<void> {
    const response = await userApi.login(request);
    token.value = response.access_token;
    localStorage.setItem(import.meta.env.VITE_SECRET_KEY, response.access_token);
    await fetch();
  }

  function logout(): void {
    token.value = null;
    user.value = null;
    localStorage.removeItem(import.meta.env.VITE_SECRET_KEY);
  }

  async function fetch(): Promise<void> {
    if (token.value === null) {
      user.value = null;
      return;
    }

    try {
      user.value = await userApi.getProfile();
    } catch {
      logout();
    }
  }

  return { token, user, isAuthenticated, login, logout, fetch };
});
