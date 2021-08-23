export class StatisticsInVersion {

  versionId:number;
  versionName:string;
  totalTests:number;
  totalExecutedTests:number;
  totalPassedTests:number;

  constructor(statisticsInVersion:StatisticsInVersion) {
    this.versionId = statisticsInVersion.versionId;
    this.versionName = statisticsInVersion.versionName;
    this.totalTests = statisticsInVersion.totalTests;
    this.totalExecutedTests = statisticsInVersion.totalExecutedTests;
    this.totalPassedTests = statisticsInVersion.totalPassedTests;
  }

  get passRatio() {
    return (this.totalPassedTests / this.totalExecutedTests) * 100;
  }

  get coverage() {
    return (this.totalExecutedTests / this.totalTests) * 100;
  }

}
