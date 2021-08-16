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

@Injectable({
  providedIn: 'root',
})
export class ApiServiceService {
  constructor(private http: HttpClient) {
  }

  getTestsSummary(
    versionId?: number,
    testClassId?: number,
    requirementId?: number
  ): Observable<TestSummary[]> {
    let parms: any = {};

    if (versionId) parms.versionId = versionId;

    if (testClassId) parms.testClassId = testClassId;

    if (requirementId) parms.requirementId = requirementId;

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


  getFeatures(): Observable<FeatureInTest[]> {
    return this.http
      .get<FeatureInTest[]>(
        `${environment.webApi}/Feature/GetFeatures`
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


  getFeaturesSummary(versionId: number): Observable<FeatureSummary[]> {
    return this.http
      .get<FeatureSummary[]>(
        `${environment.webApi}/Feature/GetFeaturesSummary`, {
          params: {versionId: versionId}
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

  getTestClassesSummary(versionId?:number): Observable<TestClassSummary[]> {
    let options = {params: {}};
    if (versionId)
      options.params = {versionId: versionId}
    return this.http
      .get<TestClassSummary[]>(
        `${environment.webApi}/TestClass/GetTestClassesSummary`,options)
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
