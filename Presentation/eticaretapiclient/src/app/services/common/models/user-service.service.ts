import {Injectable} from '@angular/core';
import {User} from "@app/entities/user";
import {HttpClientService} from "@app/services/common/http-client.service";
import {Create_user} from "@app/contracts/users/create_user";
import {login_User} from "@app/contracts/users/login_user";
import {HttpHeaders} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
      private httpClientService : HttpClientService
  )
  {  }

     create (user : User) {
         return this.httpClientService.post<User, Create_user> (
            {
                action : 'users',
                controller : 'CreateUser',
                isAdminPage : false
            },
            user );
     }

         login (loginData : login_User) {
             const headers = new HttpHeaders({
                 'isAdmin': 'false',
             });

            return this.httpClientService.post<login_User,login_User > (
                {
                    controller : 'users',
                    action : 'LoginUser',
                    headers : headers
                },
                loginData );
         }
}
