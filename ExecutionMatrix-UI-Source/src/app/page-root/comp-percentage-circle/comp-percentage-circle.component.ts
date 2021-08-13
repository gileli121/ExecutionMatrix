import {Component, OnInit, Input, OnChanges, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-comp-percentage-circle',
  templateUrl: './comp-percentage-circle.component.html',
  styleUrls: ['./comp-percentage-circle.component.scss']
})
export class CompPercentageCircleComponent implements OnInit, OnChanges {

  @Input()
  percent:number = 0;

  _precent:number = 0;
  strokeColor:string = 'rgb(255,255,255)';


  ngOnInit(): void {
    this.setProgress();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.setProgress()
  }

  setProgress(){
    this._precent = this.percent;
    this.strokeColor = this.greenYellowRed(this.percent);
  }

  greenYellowRed(percentage :number) {
    percentage  = Math.round(percentage);

    let r = (percentage  > 50 ? 1 - 2 * (percentage  - 50) / 100.0 : 1.0) * 255;
    let g = (percentage  > 50 ? 1.0 : 2 * percentage  / 100.0) * 255;
    let b = 0.0;

    return 'rgb('+r+','+g+','+b+')';
  }


}
