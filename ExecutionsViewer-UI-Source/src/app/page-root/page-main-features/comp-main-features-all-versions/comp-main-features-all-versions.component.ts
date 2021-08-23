import {Component, OnInit} from '@angular/core';
import {MainFeatureVersionStatistics} from "../../../models/main-feature-version-statistics.model";
import {ApiServiceService} from "../../../services/api-service.service";
import {ActivatedRoute, Router} from "@angular/router";
import {GlobalsService} from "../../../services/globals.service";
import {StatisticsInVersion} from "../../../models/statistics-in-version.model";

@Component({
  selector: 'app-comp-main-features-all-versions',
  templateUrl: './comp-main-features-all-versions.component.html',
  styleUrls: ['./comp-main-features-all-versions.component.css']
})
export class CompMainFeaturesAllVersionsComponent implements OnInit {

  pageSize = 10;
  pageNumber = 1;

  mainFeatureStatistics: MainFeatureVersionStatistics[] = [];
  versionsR1: StatisticsInVersion[] = [];

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
    this.api.getMainFeatureVersionStatistics(this.pageNumber, this.pageSize)
      .subscribe(mainFeatureStatistics => {
        this.mainFeatureStatistics = mainFeatureStatistics;
        if (mainFeatureStatistics.length > 0)
          this.versionsR1 = mainFeatureStatistics[0].statistics;
      });
  }

  onRowClicked(versionStatistics: MainFeatureVersionStatistics) {
    this.globals.setSelectedMainFeatureById(versionStatistics.mainFeatureId);
    this.router.navigate(['/features'], {
      queryParams: {
        mainFeatureId: versionStatistics.mainFeatureId
      }
    });
  }
}
