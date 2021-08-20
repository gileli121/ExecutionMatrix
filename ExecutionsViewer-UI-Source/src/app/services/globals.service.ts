import {Injectable} from '@angular/core';
import {Version} from "../models/version.model";
import {ApiServiceService} from "./api-service.service";
import {ActivatedRoute, Router} from "@angular/router";
import {EventQueueServiceService} from "./event-queue-service.service";
import {AppEvent} from "../classes/app-event";
import {AppEventType} from "../classes/app-event-type";

@Injectable({
  providedIn: 'root',
})
export class GlobalsService {

  private _selectedVersionId: number = 0;
  private _selectedVersion?: Version;
  private _versions: Version[] | undefined;

  get selectedVersionId() {
    return this._selectedVersionId;
  }

  get selectedVersion() {
    return this._selectedVersion;
  }

  get versions() {
    return this._versions;
  }

  constructor(private api: ApiServiceService,
              private route: ActivatedRoute,
              private router: Router,
              private eventQ: EventQueueServiceService) {
  }

  init() {

    this.route.queryParamMap.subscribe(params => {
      const selectedVersionStr = params.get('versionId');
      if (selectedVersionStr) {
        this.setSelectedVersionById(parseInt(selectedVersionStr));
      }
    });

    if (this._versions == undefined) {
      this.loadVersions(true);
    }
  }

  loadVersions(broadcastLoadedEvent:boolean,setSelectedVersion = true) {
    this.api.getVersions().subscribe((versions) => {
      this._versions = versions;
      if (setSelectedVersion && versions.length > 0 && !this._selectedVersion)
        this.setSelectedVersionById(this._selectedVersionId ? this._selectedVersionId : versions[0].id);

      if (broadcastLoadedEvent)
        this.eventQ.dispatch(new AppEvent(AppEventType.VersionsLoadedEvent, event));
    });
  }

  setSelectedVersion(version?:Version, updateQueryParms = false) {
    if (version) {
      this._selectedVersion = version;
      this._selectedVersionId = version.id;
    } else {
      this._selectedVersion = undefined;
      this._selectedVersionId = 0;
    }

    if (updateQueryParms) {
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: { versionId: this._selectedVersionId ? this._selectedVersionId : null },
        queryParamsHandling: "merge"
      });
    }

    this.eventQ.dispatch(new AppEvent(AppEventType.SelectedVersionChanged));
  }

  setSelectedVersionById(versionId?: number, updateQueryParms = false) {
    if (versionId && this._versions) {
      for (let version of this._versions) {
        if (version.id == versionId) {
          this.setSelectedVersion(version,updateQueryParms);
          return;
        }
      }
    }
    this.setSelectedVersion(undefined,updateQueryParms);
  }

}
