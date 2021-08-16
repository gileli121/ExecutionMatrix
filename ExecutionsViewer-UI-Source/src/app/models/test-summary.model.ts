import {TestClass} from "./test-class.model";
import {FeatureInTest} from "./feature-in-test.model";
import {ExecutionInTest} from "./execution-in-test.model";

export class TestSummary {
  public testId: number;
  public testMethodName: string;
  public testDisplayName: string;
  public testClass?: TestClass;
  public features: FeatureInTest[];
  public executions: ExecutionInTest[];
  public isExpanded: boolean = false;
  public lastExecution?:ExecutionInTest;

  constructor(testWithExecution: TestSummary) {
    this.testId = testWithExecution.testId;
    this.testMethodName = testWithExecution.testMethodName;
    this.testDisplayName = testWithExecution.testDisplayName;
    this.features = testWithExecution.features;
    this.executions = testWithExecution.executions;
    if (this.executions?.length > 0)
      this.lastExecution = this.executions[0];
    if (testWithExecution.testClass)
      this.testClass = new TestClass(testWithExecution.testClass);
  }

  get testNameView(): string {
    if (this.testDisplayName) return this.testDisplayName;

    return this.testMethodName;
  }

  get haveExecutions():boolean {
    return this.executions && this.executions.length > 0;
  }
}
