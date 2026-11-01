<script setup lang="ts">
const props = defineProps<{
  page: number;
  totalPages: number;
}>();

function nextPage(pageIndex: number) {
  const param = new URLSearchParams(window.location.search);

  param.set('page', String(pageIndex));
  return { query: Object.fromEntries(param.entries()) };
}
</script>

<template>
  <div class="d-flex gap-2">
    <RouterLink v-if="page > 1" :to="nextPage(page - 1)">&lt;</RouterLink>
    <span v-else>&lt;</span>

    <RouterLink :to="nextPage(page)">{{ page }}</RouterLink>
    <span class="text-muted">/</span>
    <RouterLink :to="nextPage(totalPages)">{{ totalPages }}</RouterLink>

    <RouterLink v-if="page < totalPages" :to="nextPage(page + 1)">&gt;</RouterLink>
    <span v-else>&gt;</span>
  </div>
</template>
