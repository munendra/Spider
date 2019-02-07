import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularFontAwesomeModule } from 'angular-font-awesome';


import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';


import { LoaderInterceptor } from './shared/interceptor/loaderInterceptor';
import { NotificationInterceptor } from './shared/interceptor/notificationInterceptor';

import { LoaderComponent } from './shared/component/loader/loader.component';
import { NotificationComponent } from './shared/component/notification/notification.component';

import { ClickStopPropagationDirective } from './shared/directive/click-stop-propagation.directive';
import { ConfirmDialogDirective } from './shared/directive/confirmation-dialog/confirm-dialog.directive';


import { ApplicationListComponent } from './application/application-list/component/application-list.component';
import { ApplicationAddComponent } from './application/application-add/component/application-add.component';
import { ApplicationEditComponent } from './application/application-edit/component/application-edit.component';

import { EnvironmentListComponent } from './environment/environment-list/component/environment-list.component';
import { EnvironmentAddComponent } from './environment/environment-add/component/environment-add.component';
import { EnvironmentEditComponent } from './environment/environment-edit/component/environment-edit.component';
import { ConfigurationListComponent } from './configuration/configuration-list/component/configuration-list.component';

import { ConfigurationAddComponent } from './configuration/configuration-add/component/configuration-add.component';
import { ConfigurationUpdateComponent } from './configuration/configuration-update/component/configuration-update.component';

@NgModule({
  declarations: [
    AppComponent,

    ClickStopPropagationDirective,
    NotificationComponent,
    LoaderComponent,
    ConfirmDialogDirective,

    ApplicationListComponent,
    ApplicationAddComponent,
    ApplicationEditComponent,


    EnvironmentListComponent,
    EnvironmentAddComponent,
    EnvironmentEditComponent,

    ConfigurationAddComponent,
    ConfigurationListComponent,
    ConfigurationUpdateComponent

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AngularFontAwesomeModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,

  ],
  entryComponents: [
    ApplicationAddComponent, ApplicationEditComponent,
    EnvironmentAddComponent, EnvironmentEditComponent,
    ConfigurationAddComponent, ConfigurationUpdateComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: NotificationInterceptor,
      multi: true
    }
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
