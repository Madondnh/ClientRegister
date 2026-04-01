import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr'; 
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private toast: ToastrService) { } // A UI service

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                let userMessage = 'An unexpected error occurred';

                // Logic to decide message based on Status Code
                if (error.status === 404) {
                    userMessage = 'The requested resource was not found.';
                } else if (error.status === 500) {
                    userMessage = 'Internal server error. Please try again later.';
                } else if (error.status === 0) {
                    userMessage = 'Network error. Check your internet connection.';
                }

                // General error toast
                this.toast.warning(userMessage || 'An error occurred', 'Attention')
                console.error('Logged from Interceptor:', error);

                // Return the error so the component can also handle it if needed
                return throwError(() => new Error(userMessage));
            })
        );
    }
}