import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import { TestExecution } from '../../../models/test-execution.model';
import { ApiServiceService } from '../../../services/api-service.service';
import { ExecutionResult } from '../../../models/execution-result';
import { UtilsService } from '../../../services/utils.service';

@Component({
  selector: 'app-comp-show-test-execution',
  templateUrl: './comp-show-test-execution.component.html',
  styleUrls: [
    './comp-show-test-execution.component.css',
    '../page-executions.component.css',
  ],
})
export class CompShowTestExecutionComponent implements OnInit, OnChanges {
  @Input()
  testExecutionId?: number;

  @Output()
  selectedTestExecResEvent = new EventEmitter<TestExecution>();

  selectedTestExecRes?:TestExecution;

  testOldExecutionId?: number;

  testExecution: TestExecution[] = [];
  ExecutionResult = ExecutionResult;

  constructor(private api: ApiServiceService,
              public utils: UtilsService) {}

  ngOnInit(): void {
    this.loadSelectedExecution();
  }

  private loadSelectedExecution() {
    if (this.testExecutionId && this.testExecutionId !== this.testOldExecutionId) {
      this.testOldExecutionId = this.testExecutionId;
      this.api.getExecution(this.testExecutionId).subscribe((testExecution) => {
        this.testExecution.push(testExecution);
        this.selectedTestExecRes = testExecution;
        this.selectedTestExecResEvent.emit(testExecution);
      });
    }
  }

  onTestToggleExpand(testExecutionItem: TestExecution) {
    testExecutionItem.isExpanded = !testExecutionItem.isExpanded;
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.testExecution = [];
    this.loadSelectedExecution();
  }

  onSubExecutionResultSelected(item: TestExecution) {
    this.selectedTestExecRes = item;
    this.selectedTestExecResEvent.emit(item);
  }
}
