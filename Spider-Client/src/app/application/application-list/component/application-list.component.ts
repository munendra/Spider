import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';

import { Application } from '../../classes/Application';
import { NotificationService } from '../../../shared/services/notification/notification.service';

import { ApplicationListService } from '../service/application-list.service';
import { ApplicationAddComponent } from '../../application-add/component/application-add.component';
import { ApplicationDeleteService } from '../../application-delete/service/application-delete.service';
import { ApplicationEditComponent } from '../../application-edit/component/application-edit.component';

@Component({
  selector: 'app-application-list',
  templateUrl: './application-list.component.html',
  styleUrls: ['./application-list.component.scss']
})

export class ApplicationListComponent implements OnInit, OnDestroy {

  error: any;
  applicationList: Application[];
  private subscription: Subscription;


  constructor(private applicationListService: ApplicationListService,
    private modalService: NgbModal,
    private applicationDeleteService: ApplicationDeleteService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.getApplicationList();
  }

  getApplicationList() {

    this.applicationListService.getApplicationList();
    this.subscription = this.applicationListService.applicationListState.subscribe((state: Application[]) => {
      this.applicationList = state;
    });
  }

  addApplicationPopup() {
    this.modalService.open(ApplicationAddComponent, { centered: true });
  }

  edit(applicationId) {
    const modalref = this.modalService.open(ApplicationEditComponent, { centered: true });
    modalref.componentInstance.applicationId = applicationId;
  }

  delete(applicationId) {
    console.log(applicationId);
    if (confirm('Are you sure?')) {
      this.applicationDeleteService.deleteApplication(applicationId).then(res => {
        this.notificationService.onSuccess('Application deleted successfully.');
        this.applicationListService.getApplicationList();
      });
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
