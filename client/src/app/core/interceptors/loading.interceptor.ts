import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyServcice: BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if(request.url.includes('emailExis') || 
        request.method === 'POST' && request.url.includes('orders') ||
        request.method === 'DELETE'
      )      {

       return next.handle(request);
    }
   
    this.busyServcice.busy();
    return next.handle(request).pipe(
      delay(1000),
      finalize(()=> this.busyServcice.idle())
    )
  }
}
