import { AxiosResponse } from "axios";
import { ApiRequest } from "@/app/core/api/ApiRequest";
import { HttpMethod } from "@/app/core/api/HttpMethod";
import PushUserRequest from "@/app/infrastructure/requests/User/PushUserRequest";
/*
 * API Resolution:
 * Push + Entity- POST
 * Fetch + Entity- GET
 * RequestUpdate + Entity - UPDATE
 * RequestDelete + Entity - DELETE
 */
export namespace UserAPI {
  export class PushUser implements ApiRequest<boolean> {
    response: boolean;

    path: "user/create";

    method = HttpMethod.POST;

    parse = (data: AxiosResponse) => data.data;

    constructor(public params: PushUserRequest) {
      this.path = "user/create";
      this.response = false;
    }
  }
}
