<template>
  <h1>Welcome to the SOL app.</h1>
  <router-link
      to="/registration"
      custom
      v-slot="{ href, route, navigate, isExactActive }"
  >
    <a :href="href" @click="navigate">
      Registration page
    </a>
  </router-link>
  <router-link
      to="/login"
      custom
      v-slot="{ href, route, navigate, isExactActive }"
  >
    <a :href="href" @click="navigate">
      Login page
    </a>
  </router-link>
  <router-link
      to="/account"
      custom
      v-slot="{ href, route, navigate, isExactActive }"
  >
    <a :href="href" @click="navigate">
      Account page
    </a>
  </router-link>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "HomePage",
});
</script>
