import { Injectable, Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Notification } from '../../classes/notification/notification';
import { NotificationService } from '../../services/notification/notification.service';
import { MessageType } from '../../enums/notification/MessageType';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
@Injectable({ providedIn: 'root' })
export class NotificationComponent implements OnInit, OnDestroy {
  notification: Notification;
  private subscription: Subscription;
  messageType: typeof MessageType = MessageType;
  constructor(private notificationService: NotificationService) {

  }

  ngOnInit() {
    this.subscription = this.notificationService.notificationState
      .subscribe((state: Notification) => {
        this.notification = state;
      });
  }


  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
