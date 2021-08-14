import {Injectable} from '@angular/core';
import {ExecutionResult} from "../models/execution-result";

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor() { }


  getExecutionResultString(executionResult:ExecutionResult) {
    switch (executionResult) {
      case ExecutionResult.passed:
        return 'Passed';
      case ExecutionResult.failed:
        return 'Failed';
      case ExecutionResult.fatal:
        return 'Fatal';
      case ExecutionResult.skipped:
        return 'Skipped/Disabled';
      case ExecutionResult.unexecuted:
        return 'Un Executed';
    }
  }

}
