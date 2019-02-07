import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';


import { environment } from '../../../../environments/environment';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';


import { Configuration } from '../../classes/configuration';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationDeleteService {

  private configurationApiBaseUrl: string;


  constructor(private http: HttpClient) {
    this.configurationApiBaseUrl = environment.apiBaseUrl + 'api/configuration';
  }

  public deleteEnvironment(configurationId): Promise<any> {
    return this.http.delete<ApiResponse>(this.configurationApiBaseUrl + '/' + configurationId)
      .map(response => {
      }).toPromise();
  }

}
