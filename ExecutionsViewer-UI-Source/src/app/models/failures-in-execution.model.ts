export class FailuresInExecution {

  public failureMessage:string;
  public expected?:string;
  public actual?:string;


  constructor(failure:FailuresInExecution) {
    this.failureMessage = failure.failureMessage;
    this.expected = failure.expected;
    this.actual = failure.actual;
  }

  get haveExActFields():boolean {
    return this.expected != undefined && this.actual != undefined;
  }

}
