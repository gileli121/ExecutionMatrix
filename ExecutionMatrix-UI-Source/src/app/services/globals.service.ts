import {Injectable} from '@angular/core';
import {Version} from "../models/version.model";
import {ApiServiceService} from "./api-service.service";
import {ActivatedRoute} from "@angular/router";
import {EventQueueServiceService} from "./event-queue-service.service";
import {AppEvent} from "../classes/app-event";
import {AppEventType} from "../classes/app-event-type";

@Injectable({
  providedIn: 'root',
})
export class GlobalsService {

  selectedVersionId: number = 0;
  selectedVersion?: Version;
  versions: Version[] | undefined;
  rootElement?: HTMLDivElement;


  constructor(private api: ApiServiceService,
              private route: ActivatedRoute,
              private eventQ: EventQueueServiceService) {
  }

  init() {

    this.route.queryParamMap.subscribe(params => {
      const selectedVersionStr = params.get('versionId');
      if (selectedVersionStr)
        this.selectedVersionId = parseInt(selectedVersionStr);
    });

    if (this.versions == undefined) {
      this.api.getVersions().subscribe((versions) => {
        this.versions = versions;
        if (versions.length > 0)
          this.onSelectedVersionIdChanged(versions[0].id);

        this.eventQ.dispatch(new AppEvent(AppEventType.VersionsLoadedEvent, event));
      });
    }
  }

  get rootElementWidth() {
    return this.rootElement ? this.rootElement.offsetWidth : 0;
  }

  onSelectedVersionIdChanged(versionId?: number) {
    if (versionId && this.versions) {
      for (let version of this.versions) {
        if (version.id == versionId) {
          this.selectedVersion = version;
          this.selectedVersionId = version.id;
          break;
        }
      }

    } else {
      this.selectedVersion = undefined;
      this.selectedVersionId = 0;
    }
  }

}
