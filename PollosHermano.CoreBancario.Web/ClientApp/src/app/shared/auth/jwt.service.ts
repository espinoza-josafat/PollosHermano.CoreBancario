import { Injectable } from "@angular/core";

import jwt_decode from "jwt-decode";
import Utils from "../helpers/Utils";
import { LocalStorageService } from "../storage/local-storage.service";

@Injectable({
  providedIn: "root"
})
export class JwtService {
  constructor(private localStorageService: LocalStorageService) {

  }

  private decodeToken(token: string): string {
    return jwt_decode(token);
  }

  get(): any {
    const token = this.localStorageService.get("token");

    if (Utils.IsValidStringNotEmpty(token)) {
      return this.decodeToken(token);
    }

    return null;
  }

  set(token: string): any {
    if (Utils.IsValidStringNotEmpty(token)) {
      this.localStorageService.set("token", token);
    }
  }

  clear(): any {
    this.localStorageService.remove("token");
  }

  private getExpiryTime(): number {
    const data = this.get();
    return data ? data.exp : null;
  }

  isTokenExpired(): boolean {
    const expiryTime: number = this.getExpiryTime();
    if (Utils.IsValidNumber(expiryTime)) {
      const dCurrent = new Date();
      const current = dCurrent.getTime();
      return ((1000 * expiryTime) - current) < 5000;
    }

    return true;
  }
}
