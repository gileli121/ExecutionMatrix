import { Component, OnInit } from '@angular/core';
import {MainFeatureSummary} from "../../../models/main-feature-summary.model";
import {ApiServiceService} from "../../../services/api-service.service";
import {GlobalsService} from "../../../services/globals.service";
import {ActivatedRoute, Router} from "@angular/router";
import {EventQueueServiceService} from "../../../services/event-queue-service.service";
import {AppEventType} from "../../../classes/app-event-type";

@Component({
  selector: 'app-comp-main-features-per-version',
  templateUrl: './comp-main-features-per-version.component.html',
  styleUrls: ['./comp-main-features-per-version.component.css']
})
export class CompMainFeaturesPerVersionComponent implements OnInit {

  mainFeatureSummaries: MainFeatureSummary[] = [];

  columnsToDisplay = ['mainFeatureName', 'coverage', 'passRatio', 'totalTests','actions'];


  constructor(
    private api: ApiServiceService,
    public globals: GlobalsService,
    private route: ActivatedRoute,
    private eventQ: EventQueueServiceService,
    private router: Router) {
  }


  ngOnInit(): void {
    this.route.queryParams.subscribe(() =>
    {
      if (!this.globals.selectedVersionId) return;
      this.loadTableData();
    });
    this.eventQ.on(AppEventType.VersionsLoadedEvent).subscribe(() => {
      if (!this.globals.selectedVersionId) return;
      this.loadTableData()
    });

  }


  onRowClicked(featureSummary: MainFeatureSummary) {
    this.router.navigate(['/executions'], {
      queryParams: {
        mainFeatureId: featureSummary.id,
        versionId: this.globals.selectedVersionId ? this.globals.selectedVersionId : null
      }
    });
  }

  private loadTableData() {
    this.api.getMainFeaturesSummary(this.globals.selectedVersionId)
      .subscribe(mainFeatureSummaries => {
        this.mainFeatureSummaries = mainFeatureSummaries;
      })
  }
}
