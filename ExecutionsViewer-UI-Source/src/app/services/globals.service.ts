import {Injectable} from '@angular/core';
import {Version} from "../models/version.model";
import {ApiServiceService} from "./api-service.service";
import {ActivatedRoute, Router} from "@angular/router";
import {EventQueueServiceService} from "./event-queue-service.service";
import {AppEvent} from "../classes/app-event";
import {AppEventType} from "../classes/app-event-type";
import {MainFeature} from "../models/main-feature.model";

@Injectable({
  providedIn: 'root',
})
export class GlobalsService {

  private _selectedVersionId: number = 0;
  private _selectedVersion?: Version;
  private _versions: Version[] | undefined;

  private _selectedMainFeatureId: number = 0;
  private _selectedMainFeature?: MainFeature;
  private _mainFeatures: MainFeature[] | undefined;

  get selectedVersionId() {
    return this._selectedVersionId;
  }

  get selectedVersion() {
    return this._selectedVersion;
  }

  get versions() {
    return this._versions;
  }

  get selectedMainFeatureId() {
    return this._selectedMainFeatureId;
  }

  get selectedMainFeature() {
    return this._selectedMainFeature;
  }

  get mainFeatures() {
    return this._mainFeatures;
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
        // this.setSelectedVersionById(parseInt(selectedVersionStr));
        this._selectedVersionId = parseInt(selectedVersionStr);
      }

      const selectedMainFeatureIdStr = params.get("mainFeatureId");
      if (selectedMainFeatureIdStr) {
        // this.setSelectedMainFeatureById(parseInt(selectedMainFeatureIdStr));
        this._selectedMainFeatureId = parseInt(selectedMainFeatureIdStr);
      }

      if (this._versions == undefined) {
        this.loadVersions(true);
      }

      if (this._mainFeatures == undefined)
        this.loadMainFeatures(true);

    });


  }

  loadVersions(broadcastLoadedEvent:boolean,setSelectedVersion = true) {
    this.api.getVersions().subscribe((versions) => {
      this._versions = versions;
      if (setSelectedVersion && versions.length > 0)
        this.setSelectedVersionById(this._selectedVersionId ? this._selectedVersionId : versions[0].id);

      if (broadcastLoadedEvent)
        this.eventQ.dispatch(new AppEvent(AppEventType.VersionsLoadedEvent, event));
    });
  }

  loadMainFeatures(broadcastLoadedEvent:boolean,setSelectedMainFeature = true) {
    this.api.getMainFeatures().subscribe((mainFeatures) => {
      this._mainFeatures = mainFeatures;
      if (setSelectedMainFeature && mainFeatures.length > 0)
        this.setSelectedMainFeatureById(this._selectedMainFeatureId ? this._selectedMainFeatureId : mainFeatures[0].id);

      if (broadcastLoadedEvent)
        this.eventQ.dispatch(new AppEvent(AppEventType.MainFeaturesLoadedEvent, event));
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








  setSelectedMainFeature(mainFeature?:MainFeature, updateQueryParms = false) {
    if (mainFeature) {
      this._selectedMainFeature = mainFeature;
      this._selectedMainFeatureId = mainFeature.id;
    } else {
      this._selectedMainFeature = undefined;
      this._selectedMainFeatureId = 0;
    }

    if (updateQueryParms) {
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: { mainFeatureId: this._selectedMainFeatureId ? this._selectedMainFeatureId : null },
        queryParamsHandling: "merge"
      });
    }

    // this.eventQ.dispatch(new AppEvent(AppEventType.SelectedVersionChanged));
  }

  setSelectedMainFeatureById(mainFeatureId?: number, updateQueryParms = false) {
    if (mainFeatureId && this._mainFeatures) {
      for (let mainFeature of this._mainFeatures) {
        if (mainFeature.id == mainFeatureId) {
          this.setSelectedMainFeature(mainFeature,updateQueryParms);
          return;
        }
      }
    }
    this.setSelectedMainFeature(undefined,updateQueryParms);
  }







}
