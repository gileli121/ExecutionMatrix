import { Injectable } from '@angular/core';
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
      case ExecutionResult.skipped:
        return 'Skipped';
      case ExecutionResult.unexecuted:
        return 'Un Executed';
    }
  }

}
