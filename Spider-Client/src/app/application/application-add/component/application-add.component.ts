import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { NotificationService } from '../../../shared/services/notification/notification.service';

import { ApplicationListService } from '../../application-list/service/application-list.service';
import { ApplicationAddService } from '../services/application-add.service';

import { AddApplication } from '../classes/add-application';


@Component({
  selector: 'app-application-add',
  templateUrl: './application-add.component.html',
  styleUrls: ['./application-add.component.scss']
})

export class ApplicationAddComponent implements OnInit {

  addApplicationForm: FormGroup;
  submitted = false;


  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder,
    private applicationAddService: ApplicationAddService,
    private applicationListService: ApplicationListService,
    private notificationService: NotificationService) {
  }

  ngOnInit() {
    this.buildFormAddApplication();
  }

  buildFormAddApplication() {
    this.addApplicationForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });

  }

  get f() {
    return this.addApplicationForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    if (this.addApplicationForm.invalid) {
      return;
    }
    this.applicationAddService
      .addApplication(this.addApplicationForm.value as AddApplication).then(response => {
        this.notificationService.onSuccess('Application added successfully.');
        this.applicationListService.getApplicationList();
        this.activeModal.close('call close from application Submit');
      });
  }
}
