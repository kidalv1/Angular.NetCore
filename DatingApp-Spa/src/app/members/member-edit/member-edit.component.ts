import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { error } from 'console';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild("editForm", { static: true }) editForm: NgForm;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event : any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }
  user: User;
  constructor(private route: ActivatedRoute, 
    private alertify: AlertifyService, 
    private userSevice : UserService,
    private authService : AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    })
  }

  updateUser() {
    console.log(this.authService.decodedToken)
    this.userSevice.updateUser(this.authService.decodedToken.nameid ,this.user).subscribe(next =>{
      this.alertify.succes("Profile udate succesufully")
      this.editForm.reset(this.user);
    }, error =>{
      this.alertify.error(error);
    })

  }

}
