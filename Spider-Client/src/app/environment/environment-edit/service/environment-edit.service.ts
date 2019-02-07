import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../../environments/environment';

import { EditEnvironment } from '../class/editEnvironment';
import { Environment } from '../../classes/environment';

import { ApiResponse } from '../../../shared/classes/response/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentEditService {

  environmentApiBaseUrl: string;
  constructor(private http: HttpClient) {
    this.environmentApiBaseUrl = environment.apiBaseUrl + 'api/environment/';
  }

  public editEnvironment(editEnvironment: EditEnvironment): Promise<EditEnvironment> {
    return this.http.put<ApiResponse>(this.environmentApiBaseUrl, editEnvironment).map(response => {
      return response.data;
    }).toPromise();
  }

  getEnvironmentDetail(environmentId): Promise<Environment> {
    return this.http
      .get<ApiResponse>(this.environmentApiBaseUrl + '/environment/' + environmentId)
      .map(res => {
        return res.data;
      }).toPromise();
  }
}
