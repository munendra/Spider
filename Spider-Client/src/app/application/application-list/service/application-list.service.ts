import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { Subject } from 'rxjs';

import { environment } from '../../../../environments/environment';
import { Application } from '../../classes/Application';
import { ApiResponse } from '../../../shared/classes/response/apiResponse';

@Injectable({
  providedIn: 'root'
})

export class ApplicationListService {

  private applicationListSubject = new Subject<Application[]>();
  applicationListState = this.applicationListSubject;

  applicationApiBaseUrl: string;
  constructor(private http: HttpClient) {
    this.applicationApiBaseUrl = environment.apiBaseUrl + 'api/application';
  }

  public getApplicationList() {
    this.http.get<ApiResponse>(this.applicationApiBaseUrl + '/all').subscribe(response => {
      this.applicationListState.next(<Application[]>response.data.applications);
    });
  }





}
