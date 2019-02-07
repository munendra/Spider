import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Configuration } from '../../classes/configuration';
import { environment } from '../../../../environments/environment';

import { ApiResponse } from '../../../shared/classes/response/apiResponse';

@Injectable({
  providedIn: 'root'
})

export class ConfigurationAddService {

  applicationApiBaseUrl: string;

  constructor(private http: HttpClient) {
    this.applicationApiBaseUrl = environment.apiBaseUrl + 'api/configuration/';
  }


  public addConfiguration(appApplication: Configuration): Promise<Configuration> {
    return this.http.post<ApiResponse>(this.applicationApiBaseUrl, appApplication).map(response => {
      return response.data;
    }).toPromise();
  }
}
