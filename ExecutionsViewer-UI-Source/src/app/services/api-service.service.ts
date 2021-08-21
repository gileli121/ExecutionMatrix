import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {TestExecution} from '../models/test-execution.model';
import {TestClass} from '../models/test-class.model';
import {Version} from '../models/version.model';
import {map} from 'rxjs/operators';
import {FeatureInTest} from "../models/feature-in-test.model";
import {FeatureSummary} from "../models/feature-summary.model";
import {TestClassSummary} from "../models/test-class-summary.model";
import {TestSummary} from "../models/test-summary.model";
import {MainFeature} from "../models/main-feature.model";
import {MainFeatureSummary} from "../models/main-feature-summary.model";

@Injectable({
  providedIn: 'root',
})
export class ApiServiceService {
  constructor(private http: HttpClient) {
  }

  getTestsSummary(
    versionId?: number,
    testClassId?: number,
    mainFeatureId?: number,
    featureId?: number
  ): Observable<TestSummary[]> {
    let parms: any = {};

    if (versionId) parms.versionId = versionId;

    if (testClassId) parms.testClassId = testClassId;

    if (mainFeatureId) parms.mainFeatureId = mainFeatureId;

    if (featureId) parms.featureId = featureId;

    return this.http
      .get<TestSummary[]>(
        `${environment.webApi}/Test/GetTestsSummary`,
        {
          params: parms,
        }
      )
      .pipe(
        map((result) => {
          let modifiedResult = new Array<TestSummary>();
          for (let resultItem of result)
            modifiedResult.push(new TestSummary(resultItem));
          return modifiedResult;
        })
      );
  }

  getTestClass(classId: number): Observable<TestClass> {
    return this.http.get<TestClass>(
      `${environment.webApi}/TestClass/GetTestClass`,
      {
        params: {classId: classId},
      }
    );
  }

  getTestClasses(): Observable<TestClass[]> {
    return this.http.get<TestClass[]>(
      `${environment.webApi}/TestClass/GetTestClasses`
    );
  }

  getVersions(): Observable<Version[]> {
    return this.http.get<Version[]>(
      `${environment.webApi}/Version/GetVersions`
    );
  }

  getExecution(executionId: number): Observable<TestExecution> {
    return this.http
      .get<TestExecution>(`${environment.webApi}/Execution/GetExecution`, {
        params: {executionId: executionId},
      })
      .pipe(
        map((result) => {
          return new TestExecution(result);
        })
      );
  }


  getFeatures(mainFeatureId: number): Observable<FeatureInTest[]> {
    return this.http
      .get<FeatureInTest[]>(
        `${environment.webApi}/Feature/GetFeatures`, {
          params: {mainFeatureId: mainFeatureId}
        }
      )
      .pipe(
        map((result) => {
          let modifiedResult = new Array<FeatureInTest>();
          for (let resultItem of result)
            modifiedResult.push(new FeatureInTest(resultItem));
          return modifiedResult;
        })
      );
  }


  getMainFeatures(): Observable<MainFeature[]> {
    return this.http
      .get<MainFeature[]>(
        `${environment.webApi}/MainFeature/GetMainFeatures`
      )
      .pipe(
        map((result) => {
          let modifiedResult = new Array<MainFeature>();
          for (let resultItem of result)
            modifiedResult.push(new MainFeature(resultItem));
          return modifiedResult;
        })
      );
  }


  getFeaturesSummary(versionId: number, mainFeatureId: number): Observable<FeatureSummary[]> {

    let parms: any = {};
    parms.versionId = versionId;

    if (mainFeatureId > 0)
      parms.mainFeatureId = mainFeatureId;

    return this.http
      .get<FeatureSummary[]>(
        `${environment.webApi}/Feature/GetFeaturesSummary`, {
          params: parms
        })
      .pipe(
        map((result) => {
          let modifiedResult = new Array<FeatureSummary>();
          for (let resultItem of result)
            modifiedResult.push(new FeatureSummary(resultItem));
          return modifiedResult;
        })
      );
  }

  getMainFeaturesSummary(versionId: number): Observable<MainFeatureSummary[]> {

    return this.http
      .get<MainFeatureSummary[]>(
        `${environment.webApi}/MainFeature/GetMainFeaturesSummary`, {
          params: {
            versionId: versionId
          }
        })
      .pipe(
        map((result) => {
          let modifiedResult = new Array<MainFeatureSummary>();
          for (let resultItem of result)
            modifiedResult.push(new MainFeatureSummary(resultItem));
          return modifiedResult;
        })
      );
  }

  getTestClassesSummary(versionId?: number, mainFeatureId?:number): Observable<TestClassSummary[]> {
    let parms: any = {};
    if (versionId)
      parms.versionId = versionId

    if (mainFeatureId)
      parms.mainFeatureId = mainFeatureId;

    return this.http
      .get<TestClassSummary[]>(
        `${environment.webApi}/TestClass/GetTestClassesSummary`, {
          params: parms
        })
      .pipe(
        map((result) => {
          let modifiedResult = new Array<TestClassSummary>();
          for (let resultItem of result)
            modifiedResult.push(new TestClassSummary(resultItem));
          return modifiedResult;
        })
      );
  }

}
