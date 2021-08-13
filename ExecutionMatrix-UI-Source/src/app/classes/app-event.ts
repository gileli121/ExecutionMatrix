import {AppEventType} from "./app-event-type";

export class AppEvent<T> {



  constructor(
    public type: AppEventType,
    public payload?: T,
  ) {}
}
