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

  mainFeatureSummaries: MainFeatureSummary[] = [];

  columnsToDisplay = ['mainFeatureName', 'coverage', 'passRatio', 'totalTests','actions'];


  constructor(
    private api: ApiServiceService,
    public globals: GlobalsService,
    private route: ActivatedRoute,
    private eventQ: EventQueueServiceService,
    private router: Router,) {
  }


  ngOnInit(): void {
    this.route.queryParams.subscribe(() => this.loadTableData());
    this.eventQ.on(AppEventType.VersionsLoadedEvent).subscribe(() => this.loadTableData());

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
