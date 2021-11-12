import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { IdentityService } from "../services/identity.service";
import { JwtService } from "./jwt.service";

@Injectable()
export class AuthService {
  constructor(public jwtService: JwtService, public identityService: IdentityService, public router: Router) {

  }

  signupUser(email: string, password: string) {
    //your code for signing up the new user
  }

  signinUser(email: string, password: string) {
    return this.identityService.login({
      username: email,
      password: password
    });
  }

  logout() {
    this.router.navigate(["/pages/login"]);
  }

  isAuthenticated() {
    return !this.jwtService.isTokenExpired();
  }
}
