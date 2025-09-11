import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { UserClient, UserRegisterDto } from '../../services/moziHttpClient';

@Component({
  selector: 'app-register',
  imports: [
    LocalSharedModule,
  ],
  templateUrl: './register.html',
  styleUrl: './register.css'
})

export class Register implements OnInit {

  constructor(
    private userClient: UserClient,
  ) { }

  public newUserDto: UserRegisterDto = {
    email: "",
    firstName: "",
    lastName: "",
    password: "",
    phone: ""
  } as UserRegisterDto

  public showForm: boolean = true;
  public showSuccess: boolean = false;
  public showFailed: boolean = false;


  ngOnInit(): void {
  }

  public get RegistrationButtonEnabled(): boolean {
    if (
      this.newUserDto.email.length > 0 &&
      this.newUserDto.firstName.length > 0 &&
      this.newUserDto.lastName.length > 0 &&
      this.newUserDto.password.length > 0
    ) {
      return false;
    }

    return true;
  }

  initiateRegistration() {
    this.userClient.register(this.newUserDto).subscribe(x => {
      if (x.status == 200) {
        this.showForm = false
        this.showSuccess = true
      } else {
        this.showForm = false
        this.showFailed = true
      }
    })
  }

}
