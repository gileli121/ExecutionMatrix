import {Component, OnInit, Input, OnChanges, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-comp-percentage-circle',
  templateUrl: './comp-percentage-circle.component.html',
  styleUrls: ['./comp-percentage-circle.component.scss']
})
export class CompPercentageCircleComponent implements OnInit, OnChanges {

  @Input()
  percent:number = 0;

  @Input()
  backgroundGradient:boolean = false;

  @Input()
  backgroundOpacity:number = 0.2;

  @Input()
  showSubtitle:boolean = false;

  @Input()
  subtitle:string = "progress";

  @Input()
  subtitleFontSize:string = '7px';

  _precent:number = this.percent;
  _showSubtitle:boolean = this.showSubtitle;
  _subtitleFontSize = this.subtitleFontSize;
  _backgroundGradient = this.backgroundGradient;
  _backgroundOpacity = this.backgroundOpacity;
  _subtitle = this.subtitle;

  strokeColor:string = 'rgb(255,255,255)';


  ngOnInit(): void {
    this.renderElement();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.renderElement()
  }

  renderElement(){

    this._precent = this.percent;
    this._showSubtitle = this.showSubtitle;
    this._subtitleFontSize = this.subtitleFontSize;
    this._backgroundGradient = this.backgroundGradient;
    this._backgroundOpacity = this.backgroundOpacity;
    this._subtitle = this.subtitle;

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
