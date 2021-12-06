import { container } from "tsyringe";
import BaseUseCase from "@/app/core/usecases/BaseUseCase";
import ErrorService from "@/app/services/ErrorService";
import ICommandHandlerBase from "@/app/core/commands/ICommandHandlerBase";
import ValidatorErrorService from "@/app/services/ValidatorErrorService";
import Result from "@/app/core/commands/Result";
import IError from "@/app/core/usecases/IError";
import LoginUserCommand from "@/app/domain/commands/User/LoginUserCommand";

// Login form.
export interface ILoginFields {
  Email: string;

  Password: string;
}

// Login Use Case.
export interface ILoginUseCaseResult {
  Success: boolean;
  ValidationErrors: Array<IError>;
  Errors: Array<IError>;
  // ... something else
}

// Login Use Case.
export interface ILoginUseCase {
  ErrorService: ErrorService;
  ValidatorErrorService: ValidatorErrorService;
}

export default class LoginUseCase implements BaseUseCase {
  ErrorService: ErrorService;

  ValidatorErrorService: ValidatorErrorService;

  constructor({ ErrorService, ValidatorErrorService }: ILoginUseCase) {
    this.ErrorService = ErrorService;
    this.ValidatorErrorService = ValidatorErrorService;
  }

  async execute(fields: ILoginFields) {
    const result: ILoginUseCaseResult = {
      Success: false,
      ValidationErrors: [],
      Errors: [],
    };
    let commandResult: Result = { Success: false, Errors: [] };
    try {
      // Convert Use Case object to command object.
      const loginUserCommand: LoginUserCommand = new LoginUserCommand(fields);
      const loginUserCommandHandler: ICommandHandlerBase = container.resolve("LoginUserCommandHandler");
      commandResult = await loginUserCommandHandler.handle(loginUserCommand);
      // Add Validation Errors to result.
      result.ValidationErrors = await this.ValidatorErrorService.handle(commandResult.Errors);
    } catch (errors) {
      // Add errors to result.
      result.Errors = await this.ErrorService.handle(errors);
    }
    // Success false.
    if (result.ValidationErrors.length > 0 || result.Errors.length > 0) {
      result.Success = false;
    }
    // Success true.
    result.Success = commandResult.Success;
    // Return result.
    return new Promise<ILoginUseCaseResult>((resolve, reject) => {
      resolve(result);
    });
  }
}
