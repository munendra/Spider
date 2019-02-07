import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { Notification } from '../../classes/notification/notification';
import { MessageType } from '../../enums/notification/MessageType';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationSubject = new Subject<Notification>();
  notificationState = this.notificationSubject.asObservable();
  constructor() { }

  public onSuccess(message: any) {
    const notification = new Notification();
    notification.Type = MessageType.Success;
    notification.message = message;
    notification.IsVisible = true;
    this.displayNotification(notification);
  }

  public onError(message: any) {
    const notification = new Notification();
    notification.Type = MessageType.Error;
    notification.message = message;
    notification.IsVisible = true;
    this.displayNotification(notification);
  }

  private displayNotification(notification: Notification) {
    this.notificationSubject.next(<Notification>notification);
    this.resetNotification(notification);
  }

  private resetNotification(notification: Notification) {
    setTimeout(function () {
      notification.IsVisible = false;
    }.bind(this), 5000);
  }
}
