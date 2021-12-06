<template>
  <v-container fluid class="h-100">
    <v-row>
      <v-container>
        <v-row>
          <v-col cols="4" offset="4">
            <h1>Registration</h1>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="4" offset="4">
            <v-form>
              <v-text-field v-model="RegistrationFields.Email" label="Email address" suffix="@gmail.com">
                <template #append>
                  <v-icon> mdi-mail </v-icon>
                </template>
              </v-text-field>
              <v-text-field v-model="RegistrationFields.Password" type="password" label="Password" autocomplete="on">
                <template #append>
                  <v-icon> mdi-key </v-icon>
                </template>
              </v-text-field>
              <v-text-field
                v-model="RegistrationFields.PasswordConfirmation"
                type="password"
                label="PasswordConfirmation"
                autocomplete="on"
              >
                <template #append>
                  <v-icon> mdi-key </v-icon>
                </template>
              </v-text-field>
              <v-btn @click="Submit">Submit</v-btn>
            </v-form>
          </v-col>
          <v-col v-if="RegistrationResult.Success" cols="4" offset="4">
            <v-chip color="green" text-color="white">
              {{ RegistrationResult.SuccessMessage }}
            </v-chip>
          </v-col>
        </v-row>
      </v-container>
    </v-row>
    <v-row>
      <v-col>
        <error-list :errors="RegistrationResult.ValidationErrors" />
        <error-list :errors="RegistrationResult.Errors" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import RegistrationUseCase, {
  IRegistrationFields,
  IRegistrationUseCase,
  IRegistrationUseCaseResult,
} from "@/app/usescases/User/RegistrationUseCase";
import ErrorService from "@/app/services/ErrorService";
import ErrorList from "@/view/components/ErrorList/ErrorList.vue";
import ValidatorErrorService from "@/app/services/ValidatorErrorService";
import router from "@/app/router";

const RegistrationResult: any = ref(<IRegistrationUseCaseResult>{
  Success: false,
  Errors: [],
  ValidationErrors: [],
});

const RegistrationFields: any = ref(<IRegistrationFields>{
  Email: "",
  Password: "",
  PasswordConfirmation: "",
});

// Submit create user command.
const Submit = async () => {
  const params: IRegistrationUseCase = {
    ErrorService: new ErrorService({
      context: "Registration",
    }),
    ValidatorErrorService: new ValidatorErrorService({
      context: "Registration",
    }),
  };
  RegistrationResult.value = await new RegistrationUseCase(params).execute(RegistrationFields.value);
  if (RegistrationResult.value.Success) {
    await router.push("/login");
  }
};
</script>
