import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {TestExecution} from "../../../models/test-execution.model";
import {ExecutionResult} from "../../../models/execution-result";
import {ExecutionInTest} from "../../../models/execution-in-test.model";


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

  constructor() {
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


}
