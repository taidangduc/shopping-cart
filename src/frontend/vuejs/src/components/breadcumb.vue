<script setup lang="ts">
import { RouterLink, useRoute } from 'vue-router';

var route = useRoute();
var breadcrumb = route.path.split('/').filter(Boolean);
</script>

<template>
  <nav class="mt-3">
    <ol class="breadcrumb">
      <li class="breadcrumb-item">
        <RouterLink to="/">Home</RouterLink>
      </li>
      <li class="breadcrumb-item" v-for="(crumb, index) in breadcrumb" :key="index">
        {{ crumb }}
      </li>
    </ol>
  </nav>
</template>
