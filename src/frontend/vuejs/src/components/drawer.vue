<script setup lang="ts">
import { defineProps } from 'vue';

const props = defineProps<{
  open: boolean;
  title: string;
}>();

const emit = defineEmits<{
  close: [];
}>();
</script>
<template>
  <Teleport to="body">
    <div v-if="open" class="drawer-container">
      <div class="drawer-overlay" @click="emit('close')"></div>

      <aside class="drawer-panel bg-white d-flex flex-column">
        <header class="d-flex align-items-center justify-content-between p-3 border-bottom">
          <h5 class="mb-0">{{ title }}</h5>
          <button
            type="button"
            class="btn-close"
            aria-label="Close"
            @click="emit('close')"
          ></button>
        </header>

        <div class="drawer-content flex-grow-1">
          <slot />
        </div>
      </aside>
    </div>
  </Teleport>
</template>
<style>
.drawer-container {
  position: fixed;
  inset: 76px 0 0;
  z-index: 1010;
}

.drawer-overlay {
  position: absolute;
  inset: 0;
  background: rgb(0 0 0 / 35%);
}

.drawer-panel {
  position: absolute;
  inset: 0 0 0 auto;
  width: min(400px, 100%);
}

.drawer-content {
  min-height: 0;
  overflow: hidden;
}
</style>
