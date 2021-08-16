export class TestClassSummary {

  id: number;
  packageName:string;
  className: string;
  displayName: string;
  totalTests: number;
  totalExecutedTests: number;
  totalPassedTests: number;

  constructor(classSummary: TestClassSummary) {
    this.id = classSummary.id;
    this.packageName = classSummary.packageName;
    this.className = classSummary.className;
    this.displayName = classSummary.displayName;
    this.totalTests = classSummary.totalTests;
    this.totalExecutedTests = classSummary.totalExecutedTests;
    this.totalPassedTests = classSummary.totalPassedTests;
  }

  get coverage():number {
    return this.totalTests > 0 ? this.totalExecutedTests/this.totalTests : 0;
  }

  get passRatio():number {
    return this.totalExecutedTests > 0 ? this.totalPassedTests / this.totalExecutedTests : 0;
  }

  get optimizedName():string {
    return this.displayName ? this.displayName : this.className;
  }

  get isHaveOptimizedName():boolean {
    return !!this.displayName;
  }

  get classInfoText():string | null {

    let data = new Array<String>();

    if (this.isHaveOptimizedName)
      data.push('Class: ' + this.className);

    if (this.packageName && this.packageName !== '')
      data.push('Package: ' + this.packageName);

    if (data.length == 0)
      return null;

    return '('+data.join(', ')+')';
  }
}
