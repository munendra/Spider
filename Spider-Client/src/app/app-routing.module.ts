import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ApplicationListComponent } from './application/application-list/component/application-list.component';

import { EnvironmentListComponent } from './environment/environment-list/component/environment-list.component';

import { ConfigurationListComponent } from './configuration/configuration-list/component/configuration-list.component';

const routes: Routes = [
  {
    path: '',
    component: ApplicationListComponent
  },
  {
    path: 'application/:applicationId/environments',
    component: EnvironmentListComponent,
    children: [
      {
        path: ':environmentId/configurations',
        component: ConfigurationListComponent


      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
