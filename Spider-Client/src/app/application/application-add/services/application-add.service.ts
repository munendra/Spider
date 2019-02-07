import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../../environments/environment';

import { AddApplication } from '../classes/add-application';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class ApplicationAddService {

  applicationApiBaseUrl: string;
  constructor(private http: HttpClient) {
    this.applicationApiBaseUrl = environment.apiBaseUrl + 'api/application/';
  }

  public addApplication(appApplication: AddApplication): Promise<AddApplication> {
    return this.http.post<ApiResponse>(this.applicationApiBaseUrl, appApplication).map(response => {
      return response.data;
    }).toPromise();
  }
}
