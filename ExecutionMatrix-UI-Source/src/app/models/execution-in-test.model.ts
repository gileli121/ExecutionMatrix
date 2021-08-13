import {Version} from "./version.model";
import {ExecutionResult} from "./execution-result";

export class ExecutionInTest {
  constructor(
    public id:number,
    public executionResult:ExecutionResult,
    public executionDate:Date,
    public version:Version,
    public childExecutions:ExecutionInTest[]
  ) {
  }
}
