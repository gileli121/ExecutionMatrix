import {StatisticsInVersion} from "./statistics-in-version.model";

export class MainFeatureVersionStatistics {

  mainFeatureId: number;
  mainFeatureName: string;
  statistics: StatisticsInVersion[] = [];

  constructor(mainFeatureVerStatistics: MainFeatureVersionStatistics) {
    this.mainFeatureId = mainFeatureVerStatistics.mainFeatureId;
    this.mainFeatureName = mainFeatureVerStatistics.mainFeatureName;
    if (mainFeatureVerStatistics.statistics.length > 0) {
      for (let statisticsInVersion of mainFeatureVerStatistics.statistics) {
        this.statistics.push(new StatisticsInVersion(statisticsInVersion));
      }
    }
  }
}
