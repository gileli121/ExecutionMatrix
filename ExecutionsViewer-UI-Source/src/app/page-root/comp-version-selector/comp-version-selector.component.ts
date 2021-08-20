import {Component, Input, OnInit} from '@angular/core';
import { GlobalsService } from '../../services/globals.service';
import { Version } from '../../models/version.model';
import { ActivatedRoute, Router } from '@angular/router';
import {AppEventType} from "../../classes/app-event-type";
import {EventQueueServiceService} from "../../services/event-queue-service.service";
import {MainFeature} from "../../models/main-feature.model";

@Component({
  selector: 'app-comp-version-selector',
  templateUrl: './comp-version-selector.component.html',
  styleUrls: ['./comp-version-selector.component.css'],
})
export class CompVersionSelectorComponent implements OnInit {

  @Input()
  showMainFeatureSelector = false;

  selectedVersionId = this.globals.selectedVersionId;

  constructor(
    public globals: GlobalsService
  ) {}


  ngOnInit(): void {
  }



  onSelectedVersionChanged(version?:Version) {
    this.globals.setSelectedVersion(version,true);
  }

  onSelectedMainFeatureChanged(mainFeature?:MainFeature) {

  }
}
