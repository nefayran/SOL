import IBaseService from "@/app/core/services/IBaseService";
import IError from "@/app/core/usecases/IError";

export interface IErrorService {
  context?: any;
}

export default class ErrorService implements IBaseService {
  context: any;

  constructor({ context = "Application" }: IErrorService) {
    this.context = context;
  }

  async handle(error: any): Promise<Array<IError>> {
    console.log(`Errors context: ${this.context}`);
    console.log(error);
    const errors: Array<IError> = [];
    if (error.status === 400) {
      error.raw.response.data.forEach((e: any) => {
        const err: IError = {
          Id: "something id",
          Type: 0,
          Message: e,
        };
        errors.push(err);
      });
    }
    if (error.status === 415) {
      errors.push("Not a supported data type");
    }
    if (error.status === 409) {
      errors.push("Conflict");
    }
    return new Promise((resolve, reject) => {
      resolve(errors);
    });
  }
}
