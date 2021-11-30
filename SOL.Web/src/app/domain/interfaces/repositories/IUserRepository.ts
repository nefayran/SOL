import IUser from "@/app/domain/entities/User";
import IRepository from "@/app/core/repositories/IRepository";

export default interface IUserRepository extends IRepository<IUser> {
  PushUserAsync: () => Promise<boolean>;
}
