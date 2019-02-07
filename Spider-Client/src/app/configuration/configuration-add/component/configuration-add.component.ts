import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { NotificationService } from '../../../shared/services/notification/notification.service';

import { Configuration } from '../../classes/configuration';

import { ConfigurationListService } from '../../configuration-list/service/configuration-list.service';

import { ConfigurationAddService } from '../service/configuration-add.service';

@Component({
  selector: 'app-configuration-add',
  templateUrl: './configuration-add.component.html',
  styleUrls: ['./configuration-add.component.scss']
})

export class ConfigurationAddComponent implements OnInit {

  @Input() environmentId;
  addConfigurationForm: FormGroup;
  submitted = false;


  constructor(
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private notificationService: NotificationService,
    private configurationAddService: ConfigurationAddService,
    private configurationListService: ConfigurationListService

  ) {
  }

  ngOnInit() {
    this.buildFormAddConfiguration();
  }

  buildFormAddConfiguration() {
    this.addConfigurationForm = this.formBuilder.group({
      key: ['', Validators.required],
      value: ['', Validators.required],
      isActive: [false, Validators.required],
      description: ['', Validators.required]

    });
  }

  get f() {
    return this.addConfigurationForm.controls;
  }

  onSubmit() {

    this.submitted = true;
    const configuration = this.addConfigurationForm.value as Configuration;
    configuration.environmentId = this.environmentId;
    if (this.addConfigurationForm.invalid) {
      return;
    }
    this.configurationAddService
      .addConfiguration(configuration).then(response => {
        this.notificationService.onSuccess('configuration added successfully.');
        this.configurationListService.getConfigurations(configuration.environmentId);
        this.activeModal.close('call close from application Submit');
      });
  }
}
