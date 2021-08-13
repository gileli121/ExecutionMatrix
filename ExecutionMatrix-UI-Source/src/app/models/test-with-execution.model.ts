import { FeatureInTest } from './feature-in-test.model';
import { ExecutionInTest } from './execution-in-test.model';
import {TestClass} from "./test-class.model";

export class TestWithExecution {
  public testId: number;
  public testMethodName: string;
  public testDisplayName: string;
  public testClass?: TestClass;
  public features: FeatureInTest[];
  public executions: ExecutionInTest[];
  public isExpanded: boolean = false;

  constructor(testWithExecution: TestWithExecution) {
    this.testId = testWithExecution.testId;
    this.testMethodName = testWithExecution.testMethodName;
    this.testDisplayName = testWithExecution.testDisplayName;
    this.features = testWithExecution.features;
    this.executions = testWithExecution.executions;
    if (testWithExecution.testClass)
      this.testClass = new TestClass(testWithExecution.testClass);
  }

  get testNameView(): string {
    if (this.testDisplayName) return this.testDisplayName;

    return this.testMethodName;
  }
}
