import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { NotificationService } from '../../../shared/services/notification/notification.service';


import { ApplicationDetail } from '../../classes/application-detail';
import { ApplicationEditService } from '../../application-edit/service/application-edit.service';
import { ApplicationListService } from '../../application-list/service/application-list.service';


@Component({
  selector: 'app-application-edit',
  templateUrl: './application-edit.component.html',
  styleUrls: ['./application-edit.component.scss']
})
export class ApplicationEditComponent implements OnInit {

  editApplicationForm: FormGroup;
  submitted = false;
  @Input() applicationId;


  constructor(public activeModal: NgbActiveModal,
    private applicationEditService: ApplicationEditService,
    private formBuilder: FormBuilder, private applicationListService: ApplicationListService,
    private notificationService: NotificationService) {

  }

  ngOnInit() {
    this.buildFormEditApplication();
    this.getApplicationDetail();
  }

  buildFormEditApplication() {

    this.editApplicationForm = this.formBuilder.group({
      id: [this.applicationId],
      name: ['', Validators.required],
      description: ['', Validators.required],
    });

  }

  setValueInEditFormControls(applicationDetail: ApplicationDetail) {
    this.editApplicationForm.controls['name'].patchValue(applicationDetail.application.name);
    this.editApplicationForm.controls['description'].patchValue(applicationDetail.application.description);
  }

  get f() {
    return this.editApplicationForm.controls;
  }




  getApplicationDetail() {
    this.applicationEditService.getApplicationDetails(this.applicationId).then(result => {
      this.buildFormEditApplication();
      this.setValueInEditFormControls(result);

    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.editApplicationForm.invalid) {
      return;
    }
    this.updateApplication(this.editApplicationForm.value);
  }

  updateApplication(applicationDetail: ApplicationDetail) {
    this.applicationEditService.updateApplication(applicationDetail).then(result => {
      this.notificationService.onSuccess('Application updated successfully.');
      this.applicationListService.getApplicationList();
      this.activeModal.close('call close from application Submit');
    }).catch();
  }

}
