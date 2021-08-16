import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFound } from './page-root/page-not-found/page-not-found.component';
import {PageTestsComponent} from "./page-root/page-tests/page-tests.component";
import {PageExecutionsComponent} from "./page-root/page-executions/page-executions.component";
import {PageFeaturesComponent} from "./page-root/page-features/page-features.component";


const routes: Routes = [
  { path: '', redirectTo: '/features', pathMatch: 'full' },
  { path: 'features', component: PageFeaturesComponent },
  { path: 'tests', component: PageTestsComponent },
  { path: 'executions', component: PageExecutionsComponent },
  { path: 'page-not-found', component: PageNotFound },
  { path: '**', redirectTo: '/page-not-found' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
