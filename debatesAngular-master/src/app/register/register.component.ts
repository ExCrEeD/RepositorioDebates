import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { UserService } from 'src/app/Services/user.service';
import { Router } from '@angular/router';
import { User } from 'src/app/Model/user';
import { NavPageService } from '../Services/nav-page.service';
import { Menu } from '../Model/menu';
import { ActivatedRoute } from '@angular/router';
import {SESSION_STORAGE, WebStorageService} from 'angular-webstorage-service';
import { Inject,Injectable } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public registerForm: FormGroup;
  public ajax: Boolean = false;
  public tituloForm: string;
  public switchForm: Boolean = false;
  private usuario: User;
  public actualizarOk: Boolean = false;

  constructor(
    protected tools: NavPageService,
    private formBuilder: FormBuilder,
    private usrService: UserService,
    private router: Router,
    private menuUser: Menu,
    private currentUser: User,
    private rutaActiva: ActivatedRoute,
    @Inject(SESSION_STORAGE) private storage: WebStorageService
  ) { }

  ngOnInit() {
    let ruta = this.rutaActiva.url['value'][0] === undefined ? '' : this.rutaActiva.url['value'][0].path;
    this.switchForm = ruta == 'Home';
    this.tituloForm = this.switchForm ? 'Actualiza tus datos' : 'Regístrate';
    if (this.switchForm) {
      this.registerForm = this.formBuilder.group({
        Name: ['', [Validators.required, Validators.minLength(3)]],
        SecondName: ['', [Validators.required, Validators.minLength(3)]],
        Email: ['', [Validators.required, Validators.minLength(6), Validators.email]]
      });
    }
    else {
      this.registerForm = this.formBuilder.group({
        Name: ['', [Validators.required, Validators.minLength(3)]],
        SecondName: ['', [Validators.required, Validators.minLength(3)]],
        Email: ['', [Validators.required, Validators.minLength(6), Validators.email]],
        Password: ['', [Validators.required, Validators.minLength(8)]],
        passwordRepeat: ['', [Validators.required, Validators.minLength(8)]]
      });
    }
    this.fillFormToEdit();
  }

  public validatePasword(control: AbstractControl) {
    const password = this.registerForm.get('passwordUser').value;
    let error = null
    if (password != control.value) {
      error = { notEqual: true }
    }
    return error;
  }

  async registerValidate() {
    this.ajax = true;
    this.registerForm.value.Rol = 'Student';
    delete this.registerForm.value.passwordRepeat;
    let user = this.registerForm.value;
    let response = <User>await this.usrService.add(user).toPromise();
    this.menuUser = <Menu>await this.usrService.getRolMenuRest(response.Rol).toPromise();
    this.usrService.setLoginInfo(response, this.menuUser);
    this.router.navigate(['/Home']);
  }

  public fillFormToEdit() {
    if(this.switchForm){
      this.usuario =  <User>this.usrService.getDataUser();
      this.registerForm.patchValue({
        Name: this.usuario.Name,
        SecondName: this.usuario.SecondName,
        Email: this.usuario.Email
      });
    }
  }

  async updateValidate() {
    this.ajax = true;
    this.registerForm.value.Id = this.usuario.Id;
    let response = <User>await this.usrService.updateUser(this.registerForm.value).toPromise();
    if (response) {
      this.actualizarOk = true;
      this.storage.set('local_storage_user',  response);
    }
    this.ajax = false;
  }

}
