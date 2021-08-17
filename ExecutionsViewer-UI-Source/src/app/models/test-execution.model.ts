import {FailuresInExecution} from "./failures-in-execution.model";

export class TestExecution {
  public testMethodName: string;
  public testDisplayName: string;
  public executionOutput: string;
  public executionResult: number;
  public executionDate: Date;
  public childExecutions: TestExecution[] = [];
  public failures: FailuresInExecution[] = [];
  public isExpanded: boolean = true;

  constructor(testExecution: TestExecution) {
    this.testMethodName = testExecution.testMethodName;
    this.testDisplayName = testExecution.testDisplayName;
    this.executionOutput = testExecution.executionOutput;
    this.executionResult = testExecution.executionResult;
    this.executionDate = testExecution.executionDate;
    testExecution.childExecutions?.forEach((execution) => {
      this.childExecutions.push(new TestExecution(execution));
    });

    testExecution.failures?.forEach(failure => {
      this.failures.push(new FailuresInExecution(failure));
    })

  }

  get testNameView(): string {
    if (this.testDisplayName) return this.testDisplayName;
    return this.testMethodName;
  }
}
