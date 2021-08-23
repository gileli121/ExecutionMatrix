import {Component, OnInit} from '@angular/core';
import {GlobalsService} from "../../services/globals.service";

@Component({
  selector: 'app-page-tests',
  templateUrl: './page-tests.component.html',
  styleUrls: ['./page-tests.component.css'],
})
export class PageTestsComponent implements OnInit {

  constructor(public globals: GlobalsService) {
  }

  ngOnInit(): void {
  }

}
