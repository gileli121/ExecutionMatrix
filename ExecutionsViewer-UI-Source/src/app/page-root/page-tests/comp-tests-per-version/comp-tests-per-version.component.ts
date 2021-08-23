import { Component, OnInit } from '@angular/core';
import {TestClassSummary} from "../../../models/test-class-summary.model";
import {ApiServiceService} from "../../../services/api-service.service";
import {GlobalsService} from "../../../services/globals.service";
import {ActivatedRoute, Router} from "@angular/router";
import {EventQueueServiceService} from "../../../services/event-queue-service.service";
import {AppEventType} from "../../../classes/app-event-type";

@Component({
  selector: 'app-comp-tests-per-version',
  templateUrl: './comp-tests-per-version.component.html',
  styleUrls: ['./comp-tests-per-version.component.css']
})
export class CompTestsPerVersionComponent implements OnInit {

  classesSummaries: TestClassSummary[] = [];

  columnsToDisplay = ['className', 'coverage', 'passRatio', 'totalTests'];

  constructor(
    private api: ApiServiceService,
    public globals: GlobalsService,
    private router: Router,
    private route: ActivatedRoute,
    private eventQ: EventQueueServiceService
  ) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(() => this.loadTableData());
    this.eventQ.on(AppEventType.VersionsLoadedEvent).subscribe(() => this.loadTableData());
  }


  private loadTableData() {
    this.api.getTestClassesSummary(this.globals.selectedVersionId,
      this.globals.selectedMainFeatureId).subscribe(classesSummaries => {
      this.classesSummaries = classesSummaries;
    })
  }

  onRowClicked(testClass: TestClassSummary) {

    this.router.navigate(['/executions'], {
      queryParams: {
        versionId: this.globals.selectedVersionId ? this.globals.selectedVersionId : null,
        mainFeatureId: this.globals.selectedMainFeatureId ? this.globals.selectedMainFeatureId : null,
        testClassId: testClass.id,
      }
    });
  }
}
