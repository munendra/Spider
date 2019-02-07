import { Component, OnInit, Input } from '@angular/core';

import { ActivatedRoute } from '@angular/router';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnvironmentListService } from '../../environment-list/service/environment-list.service';
import { NotificationService } from 'src/app/shared/services/notification/notification.service';
import { EnvironmentEditService } from '../service/environment-edit.service';
import { EditEnvironment } from '../class/editEnvironment';
import { Environment } from '../../classes/environment';

@Component({
  selector: 'app-environment-edit',
  templateUrl: './environment-edit.component.html',
  styleUrls: ['./environment-edit.component.scss']
})

export class EnvironmentEditComponent implements OnInit {

  EnvironmentEditForm: FormGroup;
  submitted = false;
  @Input() applicationId;
  @Input() environmentId;

  constructor(public activeModal: NgbActiveModal,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private environmentEditService: EnvironmentEditService,
    private environmentListService: EnvironmentListService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.buildEnvironmentEditForm();
    this.getEnvironmentDetail(this.environmentId);
  }

  buildEnvironmentEditForm() {
    this.EnvironmentEditForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      url: [''],
      isActive: [true]
    });
  }

  get f() {
    return this.EnvironmentEditForm.controls;
  }
  onSubmit() {
    this.submitted = true;

    if (this.EnvironmentEditForm.invalid) {
      return;
    }
    const environment = this.EnvironmentEditForm.value as EditEnvironment;
    environment.applicationId = this.applicationId;
    environment.id = this.environmentId;

    this.environmentEditService
      .editEnvironment(environment).then(response => {
        this.notificationService.onSuccess('Environment updated successfully.');
        this.environmentListService.getEnvironments(this.applicationId);
        this.activeModal.close('call close from Submit');
      });
  }

  getEnvironmentDetail(environmentId) {
    this.environmentEditService.getEnvironmentDetail(environmentId).then(result => {
      this.buildEnvironmentEditForm();
      this.setValueInEditFormControls(result);

    });
  }

  setValueInEditFormControls(environment: Environment) {
    this.EnvironmentEditForm.controls['name'].patchValue(environment.name);
    this.EnvironmentEditForm.controls['description'].patchValue(environment.description);
    this.EnvironmentEditForm.controls['isActive'].patchValue(environment.isActive);
    this.EnvironmentEditForm.controls['url'].patchValue(environment.url);
  }
}
