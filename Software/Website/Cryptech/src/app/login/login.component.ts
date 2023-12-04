import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  /**
   * @property {User} user - The User created from user-input.
   */
  user: User;

  /**
   * @property {boolean} credentialsInvalid - A flag to indicate if the input credentials are invalid.
   */
  credentialsInvalid: boolean = false;

  /**
   * @property {FormGroup} form - Reactive form for accepting user-input.
   */
  form: FormGroup = new FormGroup({
    employeeID: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  /**
   * @constructor
   * @param {AuthenticationService} authenticationService - Service used for authenticating users.
   * @param {Router} router - Router used for navigation.
   */
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) {}

  /**
   * Lifecycle hook for when component is initialized.
   */
  ngOnInit() {
    this.user = new User();
  }

  /**
   * Method to login when form is submitted.
   */
  login() {
    this.user = this.form.value;
    this.form.markAsPristine();
    this.credentialsInvalid = false;

    if (this.form.invalid) return;

    this.authenticationService.login(this.user).subscribe(
      (user) => {
        if (user.errors.length == 0) {
          this.authenticationService.globals.user = user;
          this.authenticationService.globals.user.password = this.user.password;
          this.router.navigateByUrl('/');
        }

        this.user = user;
      },
      (err) => {
        if (err.status == 401) {
          this.credentialsInvalid = true;
        }
      }
    );
  }

  /**
   * Clears flag.
   */
  clearCredentials() {
    this.credentialsInvalid = false;
  }

  /**
   * Making form control available in template.
   */
  get employeeID() {
    return this.form.get('employeeID');
  }

  /**
   * Making form control available in template.
   */
  get password() {
    return this.form.get('password');
  }
}
