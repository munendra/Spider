import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';


import { environment } from '../../../../environments/environment';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';


import { Configuration } from '../../classes/configuration';

@Injectable({
  providedIn: 'root'
})

export class ConfigurationUpdateService {

  private configurationApiBaseUrl: string;

  constructor(private http: HttpClient) {
    this.configurationApiBaseUrl = environment.apiBaseUrl + 'api/configuration';
  }

  public updateConfiguration(configuration: Configuration): Promise<Configuration> {
    return this.http.put<ApiResponse>(this.configurationApiBaseUrl, configuration).map(response => {
      return response.data;
    }).toPromise();
  }

}
