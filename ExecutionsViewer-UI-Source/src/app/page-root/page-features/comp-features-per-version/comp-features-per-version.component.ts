import { Component, OnInit } from '@angular/core';
import {FeatureSummary} from "../../../models/feature-summary.model";
import {ApiServiceService} from "../../../services/api-service.service";
import {ActivatedRoute, Router} from "@angular/router";
import {GlobalsService} from "../../../services/globals.service";
import {EventQueueServiceService} from "../../../services/event-queue-service.service";
import {AppEventType} from "../../../classes/app-event-type";

@Component({
  selector: 'app-comp-features-per-version',
  templateUrl: './comp-features-per-version.component.html',
  styleUrls: ['./comp-features-per-version.component.css']
})
export class CompFeaturesPerVersionComponent implements OnInit {

  featureSummaries: FeatureSummary[] = [];

  columnsToDisplay = ['featureName', 'coverage', 'passRatio', 'totalTests'];



  constructor(private api: ApiServiceService,
              private route: ActivatedRoute,
              private router: Router,
              public globals: GlobalsService,
              private eventQ: EventQueueServiceService) {
  }

  ngOnInit(): void {

    this.route.queryParams.subscribe(() => this.loadTableData());
    this.eventQ.on(AppEventType.VersionsLoadedEvent).subscribe(() => this.loadTableData());

  }


  private loadTableData() {
    if (this.globals.selectedVersionId) {
      this.api.getFeaturesSummary(this.globals.selectedVersionId, this.globals.selectedMainFeatureId).subscribe(featureSummaries => {
        this.featureSummaries = featureSummaries;
      })
    }
  }

  onRowClicked(featureSummary: FeatureSummary) {
    this.router.navigate(['/executions'], {
      queryParams: {
        featureId: featureSummary.id,
        mainFeatureId: this.globals.selectedMainFeatureId,
        versionId: this.globals.selectedVersionId ? this.globals.selectedVersionId : null
      }
    });
  }

}
