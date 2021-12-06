import "reflect-metadata";
import { container } from "tsyringe";
import UserRepository from "@/app/infrastructure/repositories/UserRepository";
import CreateUserCommandHandler from "@/app/application/User/CreateUserCommandHandler";
import CreateUserCommandValidator from "@/app/domain/commands/User/Validators/CreateUserCommandValidator";
import LoginUserCommandHandler from "@/app/application/User/LoginUserCommandHandler";
import LoginUserCommandValidator from "@/app/domain/commands/User/Validators/LoginUserCommandValidator";
import TokenRepository from "@/app/infrastructure/repositories/TokenRepository";
import CheckTokenCommandHandler from "@/app/application/Token/CheckTokenCommandHandler";
import CheckTokenCommandValidator from "@/app/domain/commands/Token/Validators/CheckTokenCommandValidator";

export const setup = () => {
  // Commands.
  container.register("CreateUserCommandHandler", {
    useClass: CreateUserCommandHandler,
  });
  container.register("LoginUserCommandHandler", {
    useClass: LoginUserCommandHandler,
  });
  container.register("CheckTokenCommandHandler", {
    useClass: CheckTokenCommandHandler,
  });

  // Repositories.
  container.register("UserRepository", {
    useClass: UserRepository,
  });
  container.register("TokenRepository", {
    useClass: TokenRepository,
  });

  // Validators.
  container.register("CreateUserCommandValidator", {
    useClass: CreateUserCommandValidator,
  });
  container.register("LoginUserCommandValidator", {
    useClass: LoginUserCommandValidator,
  });
  container.register("CheckTokenCommandValidator", {
    useClass: CheckTokenCommandValidator,
  });
};
