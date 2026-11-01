<script setup lang="ts">
import { RouterLink } from 'vue-router';
</script>

<template>
  <section class="text-center py-5">
    <h1 class="display-4 mb-3">Welcome to Shopping Cart</h1>
    <p class="text-muted mb-4">Browse products and manage your cart in one place.</p>
    <RouterLink to="/product" class="btn btn-primary">Shop now</RouterLink>
  </section>
</template>
