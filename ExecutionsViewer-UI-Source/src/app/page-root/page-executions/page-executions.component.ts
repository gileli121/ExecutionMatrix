import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ApiServiceService} from '../../services/api-service.service';
import {ExecutionResult} from "../../models/execution-result";
import {DatePipe} from "@angular/common";
import {ExecutionInTest} from "../../models/execution-in-test.model";
import {TestExecution} from "../../models/test-execution.model";
import {UtilsService} from "../../services/utils.service";
import {GlobalsService} from "../../services/globals.service";
import {TestSummary} from "../../models/test-summary.model";

class FilterOption {
  id: number;
  name?: string;

  constructor(id: number, name?: string) {
    this.id = id;
    this.name = name;
  }
}

interface IFilter {
  get filterName():string;
  get filterTag(): string;
  get isDisabled(): boolean;
  get childFilters(): IFilter[];
  onFilterAdded():void
  onFilterRemoved():void
  initFilterOptions(onLoaded?: () => any): void;
  setSelectedFilter(filterOption?: FilterOption): void;
  unloadOptions(): void;
  get selectedFilter(): FilterOption | undefined;
  get filterOptions(): FilterOption[];
}

class ClassFilter implements IFilter {
  filterName: string = 'Test Class';
  filterTag: string = 'testClassId';
  isDisabled = false;
  childFilters: IFilter[] = [];

  cachedOptions: FilterOption[] = [];
  _selectedFilter: FilterOption | undefined = undefined;

  constructor(public api: ApiServiceService) {
  }

  unloadOptions() {

  }

  get selectedFilter(): FilterOption | undefined {
    return this._selectedFilter;
  }

  setSelectedFilter(filterOption: FilterOption) {
    this._selectedFilter = filterOption;
  }

  get filterOptions(): FilterOption[] {
    return this.cachedOptions;
  }

  initFilterOptions(onLoaded: () => any): void {
    if (this.cachedOptions.length == 0)
      this.api.getTestClasses().subscribe((testClasses) => {
        for (const testClass of testClasses) {
          if (!testClass.id || !testClass.className) continue;
          let filterOption = new FilterOption(testClass.id, testClass.className);
          this.cachedOptions.push(filterOption);
          if (!this.selectedFilter)
            this.setSelectedFilter(filterOption);
        }
        onLoaded();
      });
    else
      onLoaded();

  }

  onFilterAdded(): void {
  }

  onFilterRemoved(): void {
  }


}

class VersionFilter implements IFilter {
  filterName: string = 'Version';
  filterTag: string = 'versionId';
  isDisabled = false;
  childFilters: IFilter[] = [];
  cachedOptions: FilterOption[] = [];
  _selectedFilter: FilterOption | undefined = undefined;

  constructor(public api: ApiServiceService) {
  }

  unloadOptions() {

  }

  get selectedFilter(): FilterOption | undefined {
    return this._selectedFilter;
  }

  setSelectedFilter(filterOption: FilterOption) {
    this._selectedFilter = filterOption;
  }

  get filterOptions(): FilterOption[] {
    return this.cachedOptions;
  }

  initFilterOptions(onLoaded: () => any): void {
    if (this.cachedOptions.length == 0)
      this.api.getVersions().subscribe((versions) => {
        for (const version of versions) {
          if (!version.id || !version.name) continue;
          let filterOption = new FilterOption(version.id, version.name);
          this.cachedOptions.push(filterOption);
          if (!this.selectedFilter || this.selectedFilter.id === filterOption.id)
            this.setSelectedFilter(filterOption);
        }
        onLoaded();
      })
    else
      onLoaded();
  }

  onFilterAdded(): void {
  }

  onFilterRemoved(): void {
  }
}

class MainFeatureFilter implements IFilter {
  filterName: string = 'Main Feature';
  filterTag: string = 'mainFeatureId'
  isDisabled: boolean = false;
  _selectedFilter: FilterOption | undefined;
  cachedOptions: FilterOption[] = [];
  childFilters: IFilter[] = [];
  private featureFilter?:FeatureFilter;

  constructor(public api: ApiServiceService) {
  }

  unloadOptions() {

  }

  setFeatureFilter(featureFilter:FeatureFilter) {
    this.featureFilter = featureFilter;
    this.childFilters.push(featureFilter);
  }


  onFilterAdded() {
    if (!this.featureFilter)
      return;
    this.featureFilter.isDisabled = false;
  }


  onFilterRemoved() {
    if (!this.featureFilter)
      return;
    this.featureFilter.isDisabled = true;
  }

  get selectedFilter(): FilterOption | undefined {
    return this._selectedFilter;
  }

  setSelectedFilter(filterOption: FilterOption) {
    this._selectedFilter = filterOption;
    if (!this.featureFilter)
      return;

    this.featureFilter.setSelectedFilter();
    this.featureFilter.unloadOptions();
    this.featureFilter.initFilterOptions()
  }

  get filterOptions(): FilterOption[] {
    return this.cachedOptions;
  }

  initFilterOptions(onLoaded: () => any): void {
    if (this.cachedOptions.length == 0)
      this.api.getMainFeatures().subscribe((mainFeatures) => {
        for (const mainFeature of mainFeatures) {
          if (!mainFeature.id || !mainFeature.featureName) continue;
          let filterOption = new FilterOption(mainFeature.id, mainFeature.featureName);
          this.cachedOptions.push(filterOption);
          if (!this.selectedFilter)
            this.setSelectedFilter(filterOption);
        }
        onLoaded();
      });
    else
      onLoaded();

  }

}

class FeatureFilter implements IFilter {
  filterName: string = 'Feature';
  filterTag: string = 'featureId';
  isDisabled = true;
  childFilters: IFilter[] = [];
  cachedOptions: FilterOption[] = [];
  _selectedFilter: FilterOption | undefined = undefined;
  private mainFeaturesFilter?: MainFeatureFilter;

  constructor(public api: ApiServiceService) {
  }

  setMainFeatureFilter(mainFeatureFilter:MainFeatureFilter) {
    this.mainFeaturesFilter = mainFeatureFilter;
  }

  onFilterAdded() {

  }


  onFilterRemoved() {

  }

  get selectedFilter(): FilterOption | undefined {
    return this._selectedFilter;
  }

  setSelectedFilter(filterOption?: FilterOption) {
    this._selectedFilter = filterOption;
  }

  get filterOptions(): FilterOption[] {
    return this.cachedOptions;
  }

  unloadOptions() {
    this.cachedOptions = [];
  }

  initFilterOptions(onLoaded?: () => any): void {

    if (!this.mainFeaturesFilter || !this.mainFeaturesFilter.selectedFilter) {
      this.cachedOptions = [];
      return;
    }

    if (this.cachedOptions.length == 0)
      this.api.getFeatures(this.mainFeaturesFilter.selectedFilter.id)
        .subscribe((features) => {
          for (const feature of features) {
            if (!feature.id || !feature.featureName) continue;
            let filterOption = new FilterOption(feature.id, feature.featureName);
            this.cachedOptions.push(filterOption);
            if (!this.selectedFilter)
              this.setSelectedFilter(filterOption);
          }
          if (onLoaded)
            onLoaded();
        })
    else if (onLoaded)
      onLoaded();
  }


}


@Component({
  selector: 'app-page-executions',
  templateUrl: './page-executions.component.html',
  styleUrls: ['./page-executions.component.css'],
  providers: [DatePipe]
})
export class PageExecutionsComponent implements OnInit {


  availableFilters: IFilter[] = [];

  usedFilters: IFilter[] = [];

  testsWithExecutions: TestSummary[] = []

  ExecutionResult = ExecutionResult;

  selectedTest?: TestSummary;
  selectedExecutionId?: number;

  isTestListInit = false;

  coveragePercent = 0;
  passRatioPercent = 0;

  @ViewChild('pageContainer', {static: true}) pageContainer: ElementRef | undefined;

  selectedTestExecutionResult?: TestExecution;

  showUnExecutedTests = true;


  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public api: ApiServiceService,
    public datePipe: DatePipe,
    public utils: UtilsService,
    public globals: GlobalsService
  ) {
  }

  ngOnInit(): void {

    let mainFeatureFilter = new MainFeatureFilter(this.api);
    let featureFilter = new FeatureFilter(this.api);

    featureFilter.setMainFeatureFilter(mainFeatureFilter);
    mainFeatureFilter.setFeatureFilter(featureFilter);

    this.availableFilters = [
      mainFeatureFilter,
      featureFilter,
      new ClassFilter(this.api),
      new VersionFilter(this.api),
    ];

    const that = this;

    function init() {
      const testExecutionId = that.route.snapshot.queryParamMap.get('testExecution');

      if (!that.isTestListInit) {
        that.initTestsList(true);
        that.isTestListInit = true;
      }
      that.unLoadTestExecution();

      if (testExecutionId) {
        that.loadTestExecution(parseInt(testExecutionId));
      }
    }

    // this.router.events.subscribe((event) => {
    //   if (event instanceof NavigationEnd)
    //     init();
    // });

    init();


  }


  private initTestsList(_loadFiltersFromQuery = true) {

    const that = this;


    function loadFilterFromQuery(filterTag: string, initTestsList = true) : IFilter | null {
      const filterValue = that.route.snapshot.queryParamMap.get(filterTag);
      if (filterValue) {
        let foundFilter = that.getAvailableFilterByTag(filterTag);
        if (foundFilter) {
          foundFilter.setSelectedFilter(new FilterOption(parseInt(filterValue)));
          that.onAddFilterEvent(foundFilter,initTestsList);
          return foundFilter;
        }
      }
      return null;
    }

    if (_loadFiltersFromQuery) {
      loadFilterFromQuery('testClassId');
      loadFilterFromQuery('versionId');
      loadFilterFromQuery('mainFeatureId');
      loadFilterFromQuery('featureId',false);
    }

    let versionId, testClassId, mainFeatureId, featureId;
    let testClassFilter = this.getUsedFilterByTag('testClassId');
    if (testClassFilter) testClassId = testClassFilter.selectedFilter?.id;
    let mainFeatureFilter = this.getUsedFilterByTag('mainFeatureId');
    if (mainFeatureFilter) mainFeatureId = mainFeatureFilter.selectedFilter?.id;
    let featureFilter = this.getUsedFilterByTag('featureId')
    if (featureFilter) featureId = featureFilter.selectedFilter?.id;
    let versionFilter = this.getUsedFilterByTag('versionId');
    if (versionFilter) versionId = versionFilter.selectedFilter?.id;

    this.api.getTestsSummary(versionId, testClassId, mainFeatureId, featureId).subscribe(testsWithExecutions => {

      if (this.selectedExecutionId) {
        for (let ex of testsWithExecutions) {
          if (!ex.executions) continue;
          for (let exc of ex.executions)
            if (this.selectedExecutionId === exc.id) {
              ex.isExpanded = true;
              this.loadTestExecution(this.selectedExecutionId, ex);
              break;
            }
          if (ex.isExpanded)
            break;
        }

      }
      this.testsWithExecutions = testsWithExecutions;

      let executedTests = 0;
      let passedTests = 0;

      if (testsWithExecutions?.length > 0) {
        for (let test of testsWithExecutions) {
          if (!test.lastExecution)
            continue;

          executedTests++;
          if (test.lastExecution.executionResult === ExecutionResult.passed)
            passedTests++;

        }

        this.coveragePercent = (executedTests / testsWithExecutions.length) * 100;
        this.passRatioPercent = (passedTests / executedTests) * 100;

      }


    });
  }


  onAddFilterEvent(filter: IFilter,initTestsList = true) {
    const index = this.availableFilters.indexOf(filter);
    if (index == -1) return;
    this.availableFilters.splice(index, 1);
    this.usedFilters.push(filter);
    if (initTestsList) {
      filter.initFilterOptions(() => {
        this.updateFiltersInQueryUrl();
        this.initTestsList(false);
      });
    }

    filter.onFilterAdded();
  }

  onRemoveFilterEvent(filter: IFilter, updateTestsList = true) {
    const index = this.usedFilters.indexOf(filter);
    if (index == -1) return;
    this.usedFilters.splice(index, 1);
    this.availableFilters.push(filter);
    filter.onFilterRemoved();
    if (filter.childFilters.length > 0) {
      for (let childFilter of filter.childFilters)
        this.onRemoveFilterEvent(childFilter, false);
    }
    if (updateTestsList) {
      this.updateFiltersInQueryUrl();
      this.initTestsList(false);
    }

  }

  private updateFiltersInQueryUrl() {

    for (let filter of this.usedFilters) {
      if (!filter.filterTag || !filter.selectedFilter?.id)
        continue

      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {[filter.filterTag]: filter.selectedFilter?.id},
        queryParamsHandling: 'merge',
      });
    }

    for (let filter of this.availableFilters) {
      const filterVal = this.route.snapshot.queryParamMap.get(filter.filterTag);
      if (filterVal && filterVal !== '') {
        this.router.navigate([], {
          // relativeTo: this.route,
          queryParams: {[filter.filterTag]: null},
          queryParamsHandling: 'merge',
        });
      }
    }
  }

  getAvailableFilterByTag(filterTag: string): IFilter | null {
    for (let filter of this.availableFilters) {
      if (filterTag === filter.filterTag) return filter;
    }
    return null;
  }

  getUsedFilterByTag(filterTag: string): IFilter | null {
    for (let filter of this.usedFilters) {
      if (filterTag === filter.filterTag) return filter;
    }
    return null;
  }

  onTestToggleExpand(testWithExecution: TestSummary) {
    testWithExecution.isExpanded = !testWithExecution.isExpanded;
  }


  loadTestExecution(testExecutionId: number, selectedTest?: TestSummary) {
    this.selectedExecutionId = testExecutionId;
    if (selectedTest)
      this.selectedTest = selectedTest;
  }

  private unLoadTestExecution() {
    this.selectedExecutionId = undefined;
    this.selectedTest = undefined;
  }


  onSelectedExecutionResult($event: TestExecution) {
    this.selectedTestExecutionResult = $event;
  }

  onSelectFilterValue(filter: IFilter, filterOption: FilterOption) {

    filter.setSelectedFilter(filterOption);

    this.testsWithExecutions = [];
    this.updateFiltersInQueryUrl();
    this.initTestsList(false);
  }

  getExecutionResultLineString(execution: ExecutionInTest) {
    return `${this.utils.getExecutionResultString(execution.executionResult)} (Version: ${execution.version.name},
    Time: ${this.datePipe.transform(execution.executionDate, 'M/d/yy hh:mm a')})`
  }

  // getLastExecutionResult(test:TestWithExecution):ExecutionResult | null {
  //   if (test.executions?.length > 0)
  //     return test.executions[0].executionResult;
  //   return null;
  // }
}
