import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ApiServiceService} from "./services/api-service.service";
import {GlobalsService} from "./services/globals.service";
import {EventQueueServiceService} from "./services/event-queue-service.service";
import {AppEvent} from "./classes/app-event";
import {AppEventType} from "./classes/app-event-type";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'TestMatrix-UI-Source';
  @ViewChild('rootContainer', { static: true }) rootContainer: ElementRef | undefined;

  constructor(private api:ApiServiceService,
              private globals:GlobalsService) {
  }

  ngOnInit(): void {

    this.globals.init();



    if (this.rootContainer)
      this.globals.rootElement = this.rootContainer.nativeElement;
  }

}
