import { Component, OnInit, Input } from '@angular/core';

import { ActivatedRoute } from '@angular/router';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { EnvironmentListService } from '../../environment-list/service/environment-list.service';
import { NotificationService } from 'src/app/shared/services/notification/notification.service';
import { EnvironmentAddService } from '../service/environment-add.service';
import { AddEnvironment } from '../class/addEnvironment';

@Component({
  selector: 'app-environment-add',
  templateUrl: './environment-add.component.html',
  styleUrls: ['./environment-add.component.scss']
})
export class EnvironmentAddComponent implements OnInit {

  EnvironmentAddForm: FormGroup;
  submitted = false;
  @Input() applicationId;

  constructor(public activeModal: NgbActiveModal,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private environmentAddService: EnvironmentAddService,
    private environmentListService: EnvironmentListService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.buildEnvironmentAddForm();
  }

  buildEnvironmentAddForm() {
    this.EnvironmentAddForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      url: [''],
      isActive: [true]
    });
  }

  get f() {
    return this.EnvironmentAddForm.controls;
  }
  onSubmit() {
    this.submitted = true;

    if (this.EnvironmentAddForm.invalid) {
      return;
    }
    const environment = this.EnvironmentAddForm.value as AddEnvironment;
    environment.applicationId = this.applicationId;
    this.environmentAddService
      .addEnvironment(environment).then(response => {
        this.notificationService.onSuccess('Environment added successfully.');
        this.environmentListService.getEnvironments(this.applicationId);
        this.activeModal.close('call close from Submit');
      });
  }
}
