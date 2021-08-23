import {Component, OnInit} from '@angular/core';
import {GlobalsService} from "../../services/globals.service";
import {MainFeatureSummary} from "../../models/main-feature-summary.model";
import {AppEventType} from "../../classes/app-event-type";
import {EventQueueServiceService} from "../../services/event-queue-service.service";
import {ActivatedRoute, Router} from "@angular/router";
import {ApiServiceService} from "../../services/api-service.service";

@Component({
  selector: 'app-page-main-features',
  templateUrl: './page-main-features.component.html',
  styleUrls: ['./page-main-features.component.css']
})
export class PageMainFeaturesComponent implements OnInit {

  constructor(public globals:GlobalsService) {
  }
  ngOnInit(): void {
  }

}
