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

  consoleText:string = '';

  ExecutionResult = ExecutionResult;

  constructor() { }

  ngOnInit(): void {
    this.redrawExecutionOutput();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.redrawExecutionOutput();
  }

  private redrawExecutionOutput() {
    this.consoleText = '';
    if (!this.testExecution) return;
    this.consoleText = this.testExecution.executionOutput;
  }



}
