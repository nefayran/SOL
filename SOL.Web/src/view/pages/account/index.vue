<template>
  <user-container />
</template>

<script lang="ts">
import { defineComponent } from "vue";
import UserContainer from "@/ui/containers/user/UserContainer.vue";

export default defineComponent({
  name: "AccountPage",
  components: {
    UserContainer,
  },
});
</script>
