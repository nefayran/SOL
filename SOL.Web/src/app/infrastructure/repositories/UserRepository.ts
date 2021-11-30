import { injectable } from "tsyringe";
import IUserRepository from "@/app/domain/interfaces/repositories/IUserRepository";
import store from "@/app/store";
import { ApiClient } from "@/app/core/api/ApiClient";
import { UserAPI } from "@/app/infrastructure/api/User/User";
import IUser from "@/app/domain/entities/User";
import PushUserRequest from "@/app/infrastructure/requests/User/PushUserRequest";

@injectable()
export default class UserRepository implements IUserRepository {
  async AddAsync(user: IUser): Promise<any> {
    try {
      return await store.dispatch("AddAsync", user);
    } catch (error) {
      return false;
    }
  }

  PushUserAsync(): Promise<boolean> {
    // Get User from store;
    const user: IUser = this.Get();
    const request: PushUserRequest = new PushUserRequest({
      Email: user.Email,
      Password: user.Password,
      // Now password should be equals.
      PasswordConfirmation: user.Password,
    });
    return ApiClient.shared.request(new UserAPI.PushUser(request));
  }

  DeleteAsync(entity: IUser): Promise<any> {
    return Promise.resolve(undefined);
  }

  Get(): IUser {
    console.log(store.getters);
    return store.getters.getUser;
  }

  UpdateAsync(entity: IUser): Promise<any> {
    return Promise.resolve(undefined);
  }
}
