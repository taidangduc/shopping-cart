<script setup lang="ts">
import { RouterLink } from 'vue-router';
</script>

<template>
  <section class="text-center py-5">
    <h1 class="display-4 mb-3">404</h1>
    <p class="text-muted mb-4">The page you are looking for does not exist.</p>
    <RouterLink to="/" class="btn btn-primary">Back to home</RouterLink>
  </section>
</template>
