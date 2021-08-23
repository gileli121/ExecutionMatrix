import {StatisticsInVersion} from "./statistics-in-version.model";

export class FeatureVersionStatistics {

  featureId:number;
  featureName:string;
  statistics:StatisticsInVersion[] = [];


  constructor(featureVersionStatistics:FeatureVersionStatistics) {
    this.featureId = featureVersionStatistics.featureId;
    this.featureName = featureVersionStatistics.featureName;
    for (let statisticsItem of featureVersionStatistics.statistics)
      this.statistics.push(new StatisticsInVersion(statisticsItem));

  }

}
