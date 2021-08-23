import { Component, OnInit } from '@angular/core';
import {MainFeatureVersionStatistics} from "../../../models/main-feature-version-statistics.model";
import {StatisticsInVersion} from "../../../models/statistics-in-version.model";
import {ApiServiceService} from "../../../services/api-service.service";
import {ActivatedRoute, Router} from "@angular/router";
import {GlobalsService} from "../../../services/globals.service";
import {ClassVersionStatistics} from "../../../models/class-version-statistics.model";

@Component({
  selector: 'app-comp-tests-all-versions',
  templateUrl: './comp-tests-all-versions.component.html',
  styleUrls: ['./comp-tests-all-versions.component.css']
})
export class CompTestsAllVersionsComponent implements OnInit {

  pageSize = 10;
  pageNumber = 1;

  testClassesStatistics: ClassVersionStatistics[] = [];
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
    this.api.getTestClassStatistics(this.pageNumber, this.pageSize)
      .subscribe(testClassesStatistics => {
        this.testClassesStatistics = testClassesStatistics;
        if (testClassesStatistics.length > 0)
          this.versionsR1 = testClassesStatistics[0].statistics;
      });
  }

  onRowClicked(testClassStatistics: ClassVersionStatistics) {
    this.router.navigate(['/executions'], {
      queryParams: {
        mainFeatureId: this.globals.selectedMainFeatureId ? this.globals.selectedMainFeatureId : null,
        testClassId: testClassStatistics.classId
      }
    });
  }

}
