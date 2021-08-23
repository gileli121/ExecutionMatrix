import {Component, OnInit} from '@angular/core';
import {GlobalsService} from "../../services/globals.service";


@Component({
  selector: 'app-page-features',
  templateUrl: './page-features.component.html',
  styleUrls: ['./page-features.component.css']
})
export class PageFeaturesComponent implements OnInit {

  constructor(public globals:GlobalsService) {
  }

  ngOnInit(): void {
  }


}
