import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {TestExecution} from "../../../models/test-execution.model";
import {ExecutionResult} from "../../../models/execution-result";
import {ExecutionInTest} from "../../../models/execution-in-test.model";
import {FailuresInExecution} from "../../../models/failures-in-execution.model";
import {MatDialog} from "@angular/material/dialog";
import {CompConsoleShowDiffsDialogComponent} from "./comp-console-show-diffs-dialog/comp-console-show-diffs-dialog.component";
import {ToastrService} from "ngx-toastr";
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-comp-execution-console',
  templateUrl: './comp-execution-console.component.html',
  styleUrls: ['./comp-execution-console.component.css']
})
export class CompExecutionConsoleComponent implements OnInit, OnChanges {

  @Input()
  testExecution?: TestExecution;

  testExecutions: TestExecution[] = [];

  ExecutionResult = ExecutionResult;

  constructor(
    private dialog: MatDialog,
    private toastr: ToastrService,
    private clipboard: Clipboard
  ) {
  }

  ngOnInit(): void {
    this.redrawExecutionOutput();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.redrawExecutionOutput();
  }


  private redrawExecutionOutput() {
    this.testExecutions = [];
    if (!this.testExecution) return;
    this.testExecutions = this.getAllDescendantsExecutionsWithOutput(this.testExecution);
  }

  private getAllDescendantsExecutionsWithOutput(testExecution: TestExecution): TestExecution[] {
    if (!testExecution)
      return [];

    let output = [];
    if (testExecution.executionOutput)
      output.push(testExecution);

    if (testExecution.childExecutions?.length > 0) {
      for (let childExecution of testExecution.childExecutions) {
        if (childExecution.childExecutions?.length > 0) {
          let childChildExecutions = this.getAllDescendantsExecutionsWithOutput(childExecution);
          if (childChildExecutions.length > 0)
            output.push(...childChildExecutions);
        } else if (childExecution.executionOutput) {
          output.push(childExecution);
        }
      }
    }

    return output;
  }


  openFailuresInDialog(failure: FailuresInExecution) {
    const dialogRef = this.dialog.open(CompConsoleShowDiffsDialogComponent,{
      data:failure,
      height: '80%',
      width: '80%'
    });

    dialogRef.afterClosed().subscribe((confirmed: boolean) => {
      if (confirmed) {

      }
    });
  }

  onCopyClipBoardClick(valueToPut: string | undefined) {
    if (!valueToPut) return;
    this.clipboard.copy(valueToPut);

    this.toastr.info('Copied to clipboard');

  }
}
