import "reflect-metadata";
import { container } from "tsyringe";
import UserRepository from "@/app/infrastructure/repositories/UserRepository";
import CreateUserCommandHandler from "@/app/application/User/CreateUserCommandHandler";

export const setup = () => {
  container.register("CreateUserCommandHandler", {
    useClass: CreateUserCommandHandler,
  });

  container.register("UserRepository", {
    useClass: UserRepository,
  });
};
