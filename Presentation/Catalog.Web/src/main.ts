import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { bootstrapApplication } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import { routes } from './app/app-routing.module';  
import { ErrorInterceptor } from './app/core/interceptors/error.interceptor';
import { provideAnimations } from '@angular/platform-browser/animations'; // <-
import { provideToastr } from 'ngx-toastr';


bootstrapApplication(AppComponent, {
  providers: 
  [
    provideHttpClient(withInterceptorsFromDi()), provideRouter(routes),
   { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true } ,
   // Required for toastr animations
    provideAnimations(), 
    
    // Toastr Configuration
    provideToastr({
      timeOut: 4000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
  ],
}).catch(err => console.error(err));


