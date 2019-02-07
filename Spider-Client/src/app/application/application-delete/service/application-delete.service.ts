import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';

@Injectable({
  providedIn: 'root'
})


export class ApplicationDeleteService {
  applicationApiBaseUrl: string;
  constructor(private http: HttpClient) {
    this.applicationApiBaseUrl = environment.apiBaseUrl + 'api/application/';
  }




  public deleteApplication(applicationId): Promise<any> {
    return this.http.delete<ApiResponse>(this.applicationApiBaseUrl + '/' + applicationId)
      .map(response => {
      }).toPromise();
  }

}
