<template>
  <input v-model="RegistrationCommand.Email" />
  <input v-model="RegistrationCommand.Password" />
  <input v-model="RegistrationCommand.PasswordConfirmation" />
  <button @click="Submit">Submit</button>
  <div v-if="RegistrationCommandResult">
    <div v-for="error in RegistrationCommandResult.Errors" :key="error">
      {{ error }}
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import "reflect-metadata";
import { container } from "tsyringe";
import ICommandHandlerBase from "@/core/commands/ICommandHandlerBase";
import CreateUserCommand from "@/domain/commands/User/CreateUserCommand";

export default defineComponent({
  name: "DetailContainer",
  setup() {
    const RegistrationCommand: CreateUserCommand = ref(
      new CreateUserCommand({
        Email: "",
        Password: "",
        PasswordConfirmation: "",
      })
    );
    const RegistrationCommandResult: any = ref();
    // Submit create user command.
    const Submit = async () => {
      const createUserCommandHandler: ICommandHandlerBase = container.resolve(
        "CreateUserCommandHandler"
      );
      RegistrationCommandResult.value = await createUserCommandHandler.handle(
        RegistrationCommand.value
      );
    };
    return {
      RegistrationCommand,
      RegistrationCommandResult,
      Submit,
    };
  },
});
</script>
