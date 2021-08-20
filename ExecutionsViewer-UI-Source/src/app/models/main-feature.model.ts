export class MainFeature {

  public id: number;
  public featureName: string;

  constructor(feature: MainFeature) {
    this.id = feature.id;
    this.featureName = feature.featureName;
  }
}
