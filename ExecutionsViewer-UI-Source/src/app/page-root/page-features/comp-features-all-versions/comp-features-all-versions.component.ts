import {Component, OnInit} from '@angular/core';
import {ApiServiceService} from "../../../services/api-service.service";
import {GlobalsService} from "../../../services/globals.service";
import {ActivatedRoute, Router} from "@angular/router";
import {FeatureVersionStatistics} from "../../../models/feature-version-statistics.model";
import {StatisticsInVersion} from "../../../models/statistics-in-version.model";

@Component({
  selector: 'app-comp-features-all-versions',
  templateUrl: './comp-features-all-versions.component.html',
  styleUrls: ['./comp-features-all-versions.component.css']
})
export class CompFeaturesAllVersionsComponent implements OnInit {

  pageSize = 10;
  pageNumber = 1;

  featureVersionStatistics:FeatureVersionStatistics[] = [];
  versionsR1:StatisticsInVersion[] = [];

  constructor(private api: ApiServiceService,
              private route: ActivatedRoute,
              private router: Router,
              public globals: GlobalsService) {
  }

  ngOnInit(): void {

    this.route.queryParams.subscribe(() => {
      if (!this.globals.selectedVersionId)
        this.loadTableData();
    });
  }


  private loadTableData() {
    this.api.getFeatureVersionStatistics(this.globals.selectedMainFeatureId, this.pageNumber, this.pageSize)
      .subscribe(versionStatistics => {
        this.featureVersionStatistics = versionStatistics;
        if (versionStatistics.length > 0)
          this.versionsR1 = versionStatistics[0].statistics;
      });
  }

  onRowClicked(feature: FeatureVersionStatistics) {
    this.router.navigate(['/executions'], {
      queryParams: {
        mainFeatureId: this.globals.selectedMainFeatureId ? this.globals.selectedMainFeatureId : null,
        featureId: feature.featureId
      }
    });
  }
}
