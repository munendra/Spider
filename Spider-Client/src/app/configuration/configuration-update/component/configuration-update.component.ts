import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { NotificationService } from '../../../shared/services/notification/notification.service';

import { Configuration } from '../../classes/configuration';

import { ConfigurationListService } from '../../configuration-list/service/configuration-list.service';

import { ConfigurationUpdateService } from '../service/configuration-update.service';


@Component({
  selector: 'app-configuration-update',
  templateUrl: './configuration-update.component.html',
  styleUrls: ['./configuration-update.component.scss']
})

export class ConfigurationUpdateComponent implements OnInit {

  @Input() configuration;
  updateConfigurationForm: FormGroup;
  submitted = false;


  constructor(
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private notificationService: NotificationService,
    private configurationUpdateService: ConfigurationUpdateService,
    private configurationListService: ConfigurationListService

  ) {
  }

  ngOnInit() {
    console.log('edit Open', this.configuration);
    this.buildFormUpdateConfiguration(this.configuration as Configuration);
  }

  buildFormUpdateConfiguration(configuration: Configuration) {
    this.updateConfigurationForm = this.formBuilder.group({
      id: [configuration.id],
      key: [configuration.key, Validators.required],
      value: [configuration.value, Validators.required],
      isActive: [configuration.isActive, Validators.required],
      description: [configuration.description, Validators.required]
    });
    this.updateConfigurationForm.controls['isActive'].patchValue(configuration.isActive);
  }

  get f() {
    return this.updateConfigurationForm.controls;
  }

  onSubmit() {

    this.submitted = true;
    const configuration = this.updateConfigurationForm.value as Configuration;
    configuration.environmentId = this.configuration.environmentId;
    if (this.updateConfigurationForm.invalid) {
      return;
    }
    this.configurationUpdateService
      .updateConfiguration(configuration).then(response => {
        this.notificationService.onSuccess('configuration updated successfully.');
        this.configurationListService.getConfigurations(configuration.environmentId);
        this.activeModal.close('call close from application Submit.');
      });
  }
}
