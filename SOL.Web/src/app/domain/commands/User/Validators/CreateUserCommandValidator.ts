import { validate } from "class-validator";
import CreateUserCommand from "@/app/domain/commands/User/CreateUserCommand";

export default class CreateUserCommandValidator {
  constructor() {}

  static Validate(createUserCommand: CreateUserCommand): Promise<any> {
    return validate(createUserCommand).then((errors) => {
      // Errors is an array of validation errors.
      if (errors.length > 0) {
        return errors;
      }
      return [];
    });
  }
}
