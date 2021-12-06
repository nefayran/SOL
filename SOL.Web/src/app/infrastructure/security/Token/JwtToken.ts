import IToken from "@/app/infrastructure/security/Token/IToken";

export default class JwtToken implements IToken {
  Date: Date;

  Id: number;

  Token: string;

  constructor(token: string) {
    this.Token = token;
    this.Date = new Date();
  }
}
