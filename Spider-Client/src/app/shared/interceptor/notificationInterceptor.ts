import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { NotificationService } from '../services/notification/notification.service';
import { Notification } from '../classes/notification/notification';
import { ApiResponse } from '../classes/response/apiResponse';
import { MessageType } from '../enums/notification/MessageType';

@Injectable({
    providedIn: 'root'
})
export class NotificationInterceptor implements HttpInterceptor {



    constructor(private notificationService: NotificationService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(tap(event => {

            if (event instanceof HttpResponse && event.status === 200) {

            }
        },
            error => {
                if (error.status !== 200) {
                    const httpError = error as HttpErrorResponse;
                    const apiResponse = httpError.error as ApiResponse;
                    if (apiResponse.hasOwnProperty('errors')) {
                        this.onError(apiResponse.errors[0].message);
                    } else {
                        this.onError('Unknown error');

                    }
                }
            }));
    }

    onError(errorMessage) {
        this.notificationService.onError(errorMessage);
    }
}

