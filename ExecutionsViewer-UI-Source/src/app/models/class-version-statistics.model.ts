import {StatisticsInVersion} from "./statistics-in-version.model";

export class ClassVersionStatistics {

  classId:number;
  className:string;
  classDisplayName:string;
  classPackageName:string;
  statistics:StatisticsInVersion[] = [];

  constructor(classVersionStatistics:ClassVersionStatistics) {
    this.classId = classVersionStatistics.classId;
    this.className = classVersionStatistics.className;
    this.classDisplayName = classVersionStatistics.classDisplayName;
    this.classPackageName = classVersionStatistics.classPackageName;

    if (classVersionStatistics.statistics.length > 0) {
      for (let statisticsItem of classVersionStatistics.statistics) {
        this.statistics.push(new StatisticsInVersion(statisticsItem));
      }
    }
  }

  get isHaveOptimizedName() {
    return !!this.classDisplayName;
  }

  get optimizedName() {
    return this.classDisplayName ? this.classDisplayName : this.className;
  }

  get classInfoText():string | null {

    let data = new Array<String>();

    if (this.isHaveOptimizedName)
      data.push('Class: ' + this.className);

    if (this.classPackageName && this.classPackageName !== '')
      data.push('Package: ' + this.classPackageName);

    if (data.length == 0)
      return null;

    return '('+data.join(', ')+')';
  }
}
