export class FeatureSummary {

  id: number;
  featureName: string;
  totalTests: number;
  totalExecutedTests: number;
  totalPassedTests: number;

  constructor(featureSummary: FeatureSummary) {
    this.id = featureSummary.id;
    this.featureName = featureSummary.featureName;
    this.totalTests = featureSummary.totalTests;
    this.totalExecutedTests = featureSummary.totalExecutedTests;
    this.totalPassedTests = featureSummary.totalPassedTests;
  }

  get passRatio():number {
    return this.totalPassedTests/this.totalTests;
  }

  get coverage():number {
    return this.totalExecutedTests/this.totalTests;
  }

}
