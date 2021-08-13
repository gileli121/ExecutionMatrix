import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-comp-page-message',
  templateUrl: './comp-page-message.component.html',
  styleUrls: ['./comp-page-message.component.css']
})
export class CompPageMessageComponent implements OnInit {

  @Input()
  pageMessage:string = '';

  constructor() { }

  ngOnInit(): void {
  }

}
