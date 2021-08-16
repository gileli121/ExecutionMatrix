import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-comp-navbar',
  templateUrl: './comp-navbar.component.html',
  styleUrls: ['./comp-navbar.component.css']
})
export class CompNavbar implements OnInit {

  isCollapsed = true;
  router: Router;

  constructor(router: Router) {
    this.router = router;
  }

  ngOnInit(): void {
  }

}
