import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../../environments/environment';
import { ApplicationDetail } from '../../classes/application-detail';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';

@Injectable({
  providedIn: 'root'
})

export class ApplicationEditService {
  private applicationApiBaseUrl: string;

  constructor(private http: HttpClient) {
    this.applicationApiBaseUrl = environment.apiBaseUrl + 'api/application';
  }

  getApplicationDetails(applicationId): Promise<ApplicationDetail> {
    return this.http
      .get<ApiResponse>(this.applicationApiBaseUrl + '/' + applicationId)
      .map(res => {
        return res.data;
      }).toPromise();
  }

  public updateApplication(appApplication: ApplicationDetail): Promise<ApplicationDetail> {
    return this.http.put<ApiResponse>(this.applicationApiBaseUrl, appApplication).map(response => {
      return response.data;
    }).toPromise();
  }

}
