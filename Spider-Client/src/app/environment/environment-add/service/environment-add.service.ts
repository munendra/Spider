import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../../environments/environment';

import { AddEnvironment } from '../class/addEnvironment';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentAddService {

  applicationApiBaseUrl: string;
  constructor(private http: HttpClient) {
    this.applicationApiBaseUrl = environment.apiBaseUrl + 'api/environment/';
  }

  public addEnvironment(addEnvironment: AddEnvironment): Promise<AddEnvironment> {

    return this.http.post<ApiResponse>(this.applicationApiBaseUrl, addEnvironment).map(response => {
      return response.data;
    }).toPromise();
  }
}
