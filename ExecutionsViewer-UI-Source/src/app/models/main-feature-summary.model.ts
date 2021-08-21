export class MainFeatureSummary {

  id: number;
  featureName: string;
  totalTests: number;
  totalTestsCollections:number;
  totalExecutedTests: number;
  totalPassedTests: number;

  constructor(mainFeatureSummary: MainFeatureSummary) {
    this.id = mainFeatureSummary.id;
    this.featureName = mainFeatureSummary.featureName;
    this.totalTests = mainFeatureSummary.totalTests;
    this.totalTestsCollections = mainFeatureSummary.totalTestsCollections;
    this.totalExecutedTests = mainFeatureSummary.totalExecutedTests;
    this.totalPassedTests = mainFeatureSummary.totalPassedTests;
  }

  get passRatio():number {
    return this.totalPassedTests/this.totalTests;
  }

  get coverage():number {
    return this.totalExecutedTests/this.totalTests;
  }
}
