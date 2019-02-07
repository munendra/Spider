import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { Subject } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';


import { Environment } from '../../classes/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentListService {

  private environmentApiBaseUrl: string;
  private applicationListSubject = new Subject<Environment[]>();
  applicationListState = this.applicationListSubject;

  constructor(private http: HttpClient) {
    this.environmentApiBaseUrl = environment.apiBaseUrl + 'api/environment';
  }

  getEnvironments(applicationId: any) {
    this.http.get<ApiResponse>(this.environmentApiBaseUrl + '/' + applicationId).subscribe(response => {
      this.applicationListState.next(<Environment[]>response.data);
    });
  }

  public deleteEnvironment(environmentId): Promise<any> {
    return this.http.delete<ApiResponse>(this.environmentApiBaseUrl + '/' + environmentId)
      .map(response => {
      }).toPromise();
  }

}
