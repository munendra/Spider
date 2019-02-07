import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../../../environments/environment';


import { Environment } from '../../classes/environment';


import { ApplicationEditService } from '../../../application/application-edit/service/application-edit.service';
import { EnvironmentListLookUp } from '../class/environmentListLookUp';
import { EnvironmentListService } from '../service/environment-list.service';
import { EnvironmentAddComponent } from '../../environment-add/component/environment-add.component';

import { EnvironmentEditComponent } from '../../environment-edit/component/environment-edit.component';

import { NotificationService } from 'src/app/shared/services/notification/notification.service';


@Component({
  selector: 'app-environment-list',
  templateUrl: './environment-list.component.html',
  styleUrls: ['./environment-list.component.scss']
})
export class EnvironmentListComponent implements OnInit, OnDestroy {

  private applicationId: any;
  private subscription: Subscription;
  filter = true;
  searchfilter: string;
  filterdEnvironments: Environment[];
  environmentApiBaseUrl: any;
  environmentListLookUp: EnvironmentListLookUp;

  constructor(
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private applicationEditService: ApplicationEditService,
    private environmentListService: EnvironmentListService,
    private notificationService: NotificationService
  ) {
    this.environmentApiBaseUrl = environment.apiBaseUrl;
    this.environmentListLookUp = new EnvironmentListLookUp();
  }

  ngOnInit() {
    this.applicationId = this.route.snapshot.paramMap.get('applicationId');
    this.getApplicationDetails();

  }

  getApplicationDetails() {
    this.applicationEditService.getApplicationDetails(this.applicationId).then(result => {
      this.environmentListLookUp.totalEnvironmets = result.totalEnvironment;
      this.environmentListLookUp.application = result.application;
      this.getEnvironments();
    });
  }

  getEnvironments() {
    this.environmentListService.getEnvironments(this.applicationId);
    this.subscription = this.environmentListService.applicationListState.subscribe((state: Environment[]) => {
      this.environmentListLookUp.environments = state['environments'];
      this.environmentListLookUp.totalEnvironmets = this.environmentListLookUp.environments.length;
      this.filterdEnvironments = this.environmentListLookUp.environments.filter(env => env.isActive === this.filter);
    });
  }

  onFilterChange(eve: any) {
    this.filter = !this.filter;
    this.filterdEnvironments = this.environmentListLookUp.environments.filter(env => env.isActive === this.filter);
  }

  onSearchChange(searchText: string) {
    this.filterdEnvironments = this.environmentListLookUp.environments
      .filter(env => env.isActive === this.filter && env.name.toLowerCase().match(searchText.toLowerCase()));
  }

  onEnvironmentAdd() {
    const modalref = this.modalService.open(EnvironmentAddComponent, { centered: true });
    modalref.componentInstance.applicationId = this.applicationId;
  }

  onEnvironmentEdit(environmentId) {
    const modalref = this.modalService.open(EnvironmentEditComponent, { centered: true });
    modalref.componentInstance.applicationId = this.applicationId;
    modalref.componentInstance.environmentId = environmentId;
  }

  delete(environmentId) {
    if (confirm('Are you sure?')) {
      this.environmentListService.deleteEnvironment(environmentId).then(res => {
        this.notificationService.onSuccess('Environment deleted successfully.');
        this.environmentListService.getEnvironments(this.applicationId);
      });
    }
  }

  doCopy(val: any) {
    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = this.environmentApiBaseUrl + 'api/environment/' + val + '/configurations';
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
