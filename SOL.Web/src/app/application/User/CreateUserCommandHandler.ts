import { injectable, inject } from "tsyringe";
import { validate, IsString, Length } from "class-validator";
import ICommandHandlerBase from "@/app/core/commands/ICommandHandlerBase";
import IUserRepository from "@/app/domain/interfaces/repositories/IUserRepository";
import IUser from "@/app/domain/entities/User";
import Result from "@/app/core/commands/Result";
import CreateUserCommand from "@/app/domain/commands/User/CreateUserCommand";
import createUserCommandValidator from "@/app/domain/commands/User/Validators/CreateUserCommandValidator";
/*
 * Command handler resolution:
 * CAS: 1 command 1 action 1 side
 * Add and push can be one 1 command
 * Update and push update can be one 1 command
 * ... other commands are similar
 * Add/Delete/Get/Update work with store.
 * Push/RequestDelete/Fetch/RequestUpdate work with server.
 * Command always return Result.
 * */
@injectable()
export default class CreateUserCommandHandler implements ICommandHandlerBase {
  constructor(
    @inject("UserRepository") private userRepository: IUserRepository // Late @inject("UserRepository") // private createUserCommandValidator: IUserRepository
  ) {}

  async handle(command: CreateUserCommand) {
    // Validation.
    const validationResult = await createUserCommandValidator.Validate(command);
    if (validationResult.length > 0) {
      return new Result(false, validationResult);
    }
    // Create entity.
    const user: IUser = {
      Id: "something id",
      Email: command.Email,
      Password: command.Password,
    };
    // If you add entity to store use Entity argument.
    await this.userRepository.AddAsync(user);
    // Actions with server should be without arguments.
    await this.userRepository.PushUserAsync();
    // Return Result().
    return new Result(true, validationResult);
  }
}
