export class TestClass {

  public id?: number;
  public packageName?: string;
  public className?: string;
  public displayName?: string;

  constructor(testClass:TestClass
  ) {
    this.id = testClass.id;
    this.packageName = testClass.packageName;
    this.className = testClass.className;
    this.displayName = testClass.displayName;
  }
}
