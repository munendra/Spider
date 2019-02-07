import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ConfigurationAddComponent } from '../../configuration-add/component/configuration-add.component';
import { ConfigurationUpdateComponent } from '../../configuration-update/component/configuration-update.component';

import { NotificationService } from '../../../shared/services/notification/notification.service';

import { ConfigurationDeleteService } from '../../configuration-delete/sesrvice/configuration-delete.service';
import { ConfigurationListService } from '../service/configuration-list.service';
import { ConfigurationUpdateService } from '../../configuration-update/service/configuration-update.service';
import { Configuration } from '../../classes/configuration';


@Component({
  selector: 'app-configuration-list',
  templateUrl: './configuration-list.component.html',
  styleUrls: ['./configuration-list.component.scss']
})
export class ConfigurationListComponent implements OnInit, OnDestroy {

  environmentId: any;
  private subscription: Subscription;

  public configurationList: Configuration[];

  submitted = false;

  constructor(
    private modalService: NgbModal,
    private notificationService: NotificationService,
    private route: ActivatedRoute,
    private configurationListService: ConfigurationListService,
    private configurationUpdateService: ConfigurationUpdateService,
    private configurationDeleteService: ConfigurationDeleteService) {

  }

  ngOnInit() {
    this.subscription = this.route.params.subscribe(params => {
      this.environmentId = params['environmentId'];
      this.getConfiguration(this.environmentId);

    });
  }

  getConfiguration(environmentId) {
    this.configurationListService.getConfigurations(environmentId);
    this.subscription = this.configurationListService.configurationListState.subscribe((data: Configuration[]) => {
      this.configurationList = data['configurations'];
    });
  }

  onDelete(configurationId: any) {
    if (confirm('Are you sure?')) {
      this.configurationDeleteService.deleteEnvironment(configurationId).then(res => {
        this.notificationService.onSuccess('Configuration deleted successfully.');
        this.getConfiguration(this.environmentId);
      });
    }
  }

  onEdit(configuration: Configuration) {
    const modalref = this.modalService.open(ConfigurationUpdateComponent, { centered: true });
    modalref.componentInstance.configuration = configuration;
  }

  onAdd() {
    const modalref = this.modalService.open(ConfigurationAddComponent, { centered: true });
    modalref.componentInstance.environmentId = this.environmentId;
  }

  ngOnDestroy() {
    console.log('ngOnDestroy');
    this.subscription.unsubscribe();
  }
}
