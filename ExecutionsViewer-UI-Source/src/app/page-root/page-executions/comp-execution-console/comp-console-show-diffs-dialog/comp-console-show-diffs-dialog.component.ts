import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FailuresInExecution} from "../../../../models/failures-in-execution.model";
import {DiffResults} from "ngx-text-diff/lib/ngx-text-diff.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-comp-console-show-diffs-dialog',
  templateUrl: './comp-console-show-diffs-dialog.component.html',
  styleUrls: ['./comp-console-show-diffs-dialog.component.css']
})
export class CompConsoleShowDiffsDialogComponent {

  constructor(public dialogRef: MatDialogRef<CompConsoleShowDiffsDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: FailuresInExecution) {
  }

  onCompareResults($event: DiffResults) {
    // console.log(1);
  }

  onCloseDialog() {
    this.dialogRef.close();
  }


}
