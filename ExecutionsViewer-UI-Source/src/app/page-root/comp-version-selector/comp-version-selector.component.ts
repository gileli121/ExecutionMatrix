import { Component, OnInit } from '@angular/core';
import { GlobalsService } from '../../services/globals.service';
import { Version } from '../../models/version.model';
import { ActivatedRoute, Router } from '@angular/router';
import {AppEventType} from "../../classes/app-event-type";
import {EventQueueServiceService} from "../../services/event-queue-service.service";

@Component({
  selector: 'app-comp-version-selector',
  templateUrl: './comp-version-selector.component.html',
  styleUrls: ['./comp-version-selector.component.css'],
})
export class CompVersionSelectorComponent implements OnInit {

  constructor(
    public globals: GlobalsService,
    private router: Router,
    private route: ActivatedRoute,
    private eventQ: EventQueueServiceService
  ) {}

  private updateQueryParms() {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { versionId: this.globals.selectedVersionId ? this.globals.selectedVersionId : null },
      queryParamsHandling: "merge"
    });
  }

  ngOnInit(): void {

    if (!this.globals.selectedVersionId) {
      this.updateQueryParms();
    }

    this.eventQ.on(AppEventType.VersionsLoadedEvent).subscribe(event => this.updateQueryParms());

  }



  onSelectedVersionChanged(version?:Version) {
    if (version)
      this.globals.onSelectedVersionIdChanged(version.id);
    else
      this.globals.onSelectedVersionIdChanged();

    this.updateQueryParms();
  }
}
