import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "app/shared/auth/auth.service";
//import { NgxSpinnerService } from "ngx-spinner";
import { JwtService } from "../../../shared/auth/jwt.service";
import Utils from "../../../shared/helpers/Utils";
import { GenericResponse } from "../../../shared/models/common/GenericResponse";

@Component({
  selector: "app-login-page",
  templateUrl: "./login-page.component.html",
  styleUrls: ["./login-page.component.scss"]
})
export class LoginPageComponent implements OnInit {

  loginFormSubmitted = false;
  isLoginFailed = false;

  loginForm = new FormGroup({
    username: new FormControl("admin", [Validators.required]),
    password: new FormControl("admin", [Validators.required]),
    rememberMe: new FormControl(true)
  });

  constructor(private router: Router,
    private authService: AuthService,
    //private spinner: NgxSpinnerService,
    private route: ActivatedRoute,
    private jwtService: JwtService) {

  }

  ngOnInit() {
    this.jwtService.clear();
  }

  get lf() {
    return this.loginForm.controls;
  }

  // On submit button click
  onSubmit() {
    this.loginFormSubmitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    //this.spinner.show(undefined, {
    //  type: "ball-triangle-path",
    //  size: "medium",
    //  bdColor: "rgba(0, 0, 0, 0.8)",
    //  color: "#fff",
    //  fullScreen: true
    //});

    this.authService.signinUser(this.loginForm.value.username, this.loginForm.value.password)
    .subscribe((response: GenericResponse<any>) => {
      if (response &&
          response.status === 1 &&
          Utils.IsValidJsonObject(response.data)) {

        const jwt = response.data;

        this.jwtService.set(jwt.token);

        this.router.navigate(["/page"]);
      }
    },
    (error: any) => {
      this.isLoginFailed = true;
      console.log("error: " + error);
    },
    () => {
      //this.spinner.hide();
    });
  }
}
