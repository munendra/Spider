import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import 'rxjs/add/operator/map';
import { Subject } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';

import { Configuration } from '../../classes/configuration';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationListService {

  private configurationApiBaseUrl: string;
  private configurationListSubject = new Subject<Configuration[]>();
  configurationListState = this.configurationListSubject;

  updateConfigurationForm: FormGroup;
  submitted = false;

  constructor(
    private http: HttpClient) {
    this.configurationApiBaseUrl = environment.apiBaseUrl + 'api/Configuration';
  }


  getConfigurations(environmentId: any) {
    this.http.get<ApiResponse>(environment.apiBaseUrl + '/api/environment/' + environmentId + '/configurations').subscribe(response => {
      this.configurationListState.next(<Configuration[]>response.data);
    });
  }



}
