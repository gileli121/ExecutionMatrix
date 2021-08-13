import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {ApiServiceService} from "../../services/api-service.service";
import {FeatureSummary} from "../../models/feature-summary.model";
import {GlobalsService} from "../../services/globals.service";
import {ActivatedRoute, NavigationEnd, Router} from "@angular/router";
import {EventQueueServiceService} from "../../services/event-queue-service.service";
import {AppEventType} from "../../classes/app-event-type";

@Component({
  selector: 'app-page-features',
  templateUrl: './page-features.component.html',
  styleUrls: ['./page-features.component.css']
})
export class PageFeaturesComponent implements OnInit {

  featureSummaries: FeatureSummary[] = [];

  columnsToDisplay = ['featureName', 'coverage', 'passRatio', 'totalTests'];

  constructor(private api: ApiServiceService,
              private route: ActivatedRoute,
              private router: Router,
              public globals: GlobalsService,
              private eventQ: EventQueueServiceService) {
  }

  ngOnInit(): void {

    const that = this;
    this.route.queryParams.subscribe(() => that.loadTableData());

    this.loadTableData();

    this.eventQ.on(AppEventType.VersionsLoadedEvent).subscribe(event => this.loadTableData());

  }


  private loadTableData() {
    if (this.globals.selectedVersionId) {
      this.api.getFeaturesSummary(this.globals.selectedVersionId).subscribe(featureSummaries => {
        this.featureSummaries = featureSummaries;
      })
    }
  }

  onRowClicked(featureSummary: FeatureSummary) {
    this.router.navigate(['/executions'], {
      queryParams: {
        featureId: featureSummary.id,
        versionId: this.globals.selectedVersionId ? this.globals.selectedVersionId : null
      }
    });
  }

}
