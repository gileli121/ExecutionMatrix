import { Injectable } from '@angular/core';
import {Observable, Subject} from "rxjs";
import {AppEvent} from "../classes/app-event";
import {AppEventType} from "../classes/app-event-type";
import {filter} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class EventQueueServiceService {

  private eventBrocker = new Subject<AppEvent<any>>();

  on(eventType: AppEventType): Observable<AppEvent<any>> {
    return this.eventBrocker.pipe(filter(event => event.type === eventType));
  }

  dispatch<T>(event: AppEvent<T>): void {
    this.eventBrocker.next(event);
  }
}
